using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Commandes.Presentation.DotNet;
using Xunit;

namespace Commandes.Tests;

public sealed class MvcObserverTests
{
    private static (ModeleCommande Modele, ServiceCommandes Commandes, JournalEspion Journal) Creer(IDepotCommandes? depot = null)
    {
        ServiceCommandes commandes = new ServiceCommandes(depot ?? new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        JournalEspion journal = new JournalEspion();
        return (new ModeleCommande(commandes, new ServiceFacturation(commandes), journal), commandes, journal);
    }

    [Fact]
    public void Modele_ConstructeurEtAbonnementRefusentNull()
    {
        var contexte = Creer();
        ServiceFacturation facturation = new ServiceFacturation(contexte.Commandes);
        Assert.Throws<ArgumentNullException>(() => new ModeleCommande(null!, facturation, contexte.Journal));
        Assert.Throws<ArgumentNullException>(() => new ModeleCommande(contexte.Commandes, null!, contexte.Journal));
        Assert.Throws<ArgumentNullException>(() => new ModeleCommande(contexte.Commandes, facturation, null!));
        Assert.Throws<ArgumentNullException>(() => contexte.Modele.Subscribe(null!));
        Assert.Equal(3, contexte.Modele.ConsulterProduits().Count);
    }

    [Fact]
    public void Subscribe_SansNotificationInitialeEtDoublonRefuse()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
        Assert.Empty(vue.Etats);
        Assert.Throws<InvalidOperationException>(() => contexte.Modele.Subscribe(vue));
        contexte.Modele.Ajouter(1, 2);
        Assert.Single(vue.Etats);
    }

    [Fact]
    public void Modele_DeuxObservateursRecoiventLeMemeInstantaneUneFois()
    {
        var contexte = Creer();
        VueEspion premiere = new VueEspion();
        VueEspion seconde = new VueEspion();
        using IDisposable a = contexte.Modele.Subscribe(premiere);
        using IDisposable b = contexte.Modele.Subscribe(seconde);
        contexte.Modele.Nouvelle(101);
        contexte.Modele.Ajouter(1, 2);
        contexte.Modele.Ajouter(2, 1);
        EtatCommandeDto provisoire = premiere.Etats.Last();
        contexte.Modele.Enregistrer();
        Assert.Equal(4, premiere.Etats.Count);
        Assert.Equal(4, seconde.Etats.Count);
        Assert.Same(premiere.Etats.Last(), seconde.Etats.Last());
        Assert.Equal(22m, provisoire.Commande.Total);
        Assert.Equal(24.20m, provisoire.Facture.Total);
        Assert.True(provisoire.Facture.EstProvisoire);
        Assert.False(premiere.Etats.Last().Facture.EstProvisoire);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(0, 1)]
    [InlineData(99, 1)]
    public void Modele_AjoutRefuseNePubliePas(int numero, int quantite)
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
        Assert.False(contexte.Modele.Ajouter(numero, quantite).EstSucces);
        Assert.Empty(vue.Etats);
        Assert.Equal(0m, contexte.Modele.Consulter().Facture.Total);
    }

    [Fact]
    public void Modele_NouvelleInvalideVideEtDeuxiemeSauvegardeNePublientPas()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
        Assert.False(contexte.Modele.Nouvelle(0).EstSucces);
        Assert.False(contexte.Modele.Enregistrer().EstSucces);
        Assert.Empty(vue.Etats);
        contexte.Modele.Ajouter(1, 1);
        contexte.Modele.Enregistrer();
        Assert.False(contexte.Modele.Enregistrer().EstSucces);
        Assert.Equal(2, vue.Etats.Count);
    }

    [Fact]
    public void Dispose_TermineLesNotificationsEtPermetReouverture()
    {
        var contexte = Creer();
        VueEspion ancienne = new VueEspion();
        IDisposable abonnement = contexte.Modele.Subscribe(ancienne);
        contexte.Modele.Ajouter(1, 1);
        abonnement.Dispose();
        abonnement.Dispose();
        VueEspion nouvelle = new VueEspion();
        using IDisposable suivant = contexte.Modele.Subscribe(nouvelle);
        contexte.Modele.Ajouter(2, 1);
        Assert.Single(ancienne.Etats);
        Assert.Single(nouvelle.Etats);
        Assert.Equal(17.05m, nouvelle.Etats.Single().Facture.Total);
        using IDisposable retour = contexte.Modele.Subscribe(ancienne);
    }

    [Fact]
    public void Notification_DesabonnementPendantParcoursRespecteInstantane()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        IDisposable? abonnement = null;
        vue.Reaction = _ => abonnement!.Dispose();
        abonnement = contexte.Modele.Subscribe(vue);
        VueEspion autre = new VueEspion();
        using IDisposable second = contexte.Modele.Subscribe(autre);
        contexte.Modele.Ajouter(1, 1);
        contexte.Modele.Ajouter(2, 1);
        Assert.Single(vue.Etats);
        Assert.Equal(2, autre.Etats.Count);
    }

    [Fact]
    public void Notification_AbonnementPendantParcoursCommenceAuChangementSuivant()
    {
        var contexte = Creer();
        VueEspion arrivee = new VueEspion();
        IDisposable? tardif = null;
        VueEspion premiere = new VueEspion() { Reaction = _ => tardif ??= contexte.Modele.Subscribe(arrivee) };
        using IDisposable initial = contexte.Modele.Subscribe(premiere);
        contexte.Modele.Ajouter(1, 1);
        Assert.Empty(arrivee.Etats);
        contexte.Modele.Ajouter(2, 1);
        Assert.Single(arrivee.Etats);
        tardif!.Dispose();
    }

    [Fact]
    public void Notification_VueDefaillanteEstJournaliseeSansAnnulerSauvegarde()
    {
        var contexte = Creer();
        VueEspion fautive = new VueEspion() { Reaction = _ => throw new IOException("detail prive") };
        VueEspion valide = new VueEspion();
        using IDisposable a = contexte.Modele.Subscribe(fautive);
        using IDisposable b = contexte.Modele.Subscribe(valide);
        contexte.Modele.Ajouter(1, 1);
        Assert.True(contexte.Modele.Enregistrer().EstSucces);
        Assert.True(contexte.Modele.Consulter().Commande.EstEnregistree);
        Assert.Equal(2, valide.Etats.Count);
        Assert.Equal(2, contexte.Journal.Operations.Count);
        Assert.All(contexte.Journal.Operations, operation => Assert.Equal("actualiser vue", operation));
    }

    [Fact]
    public void Notification_ReentreeRefuseeNeCreePasDeBoucle()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion() { Reaction = _ => contexte.Modele.Nouvelle(99) };
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
        contexte.Modele.Ajouter(1, 1);
        Assert.Equal(1, contexte.Modele.Consulter().Commande.Numero);
        Assert.Single(contexte.Journal.Operations);
        Assert.True(contexte.Modele.Ajouter(2, 1).EstSucces);
    }

    [Fact]
    public void Modele_IncidentAuDepotConserveEtatEtNeNotifiePas()
    {
        DepotPanne depot = new DepotPanne(new IOException("chemin prive"));
        var contexte = Creer(depot);
        VueEspion vue = new VueEspion();
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
        contexte.Modele.Ajouter(1, 1);
        Assert.Throws<IOException>(() => contexte.Modele.Enregistrer());
        Assert.Single(vue.Etats);
        Assert.False(contexte.Modele.Consulter().Commande.EstEnregistree);
        Assert.Equal(7.15m, contexte.Modele.Consulter().Facture.Total);
    }

    [Fact]
    public void Controleur_ConstructeurRefuseNullEtRetireAbonnementSiInitialisationEchoue()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        Assert.Throws<ArgumentNullException>(() => new ControleurCommande(null!, vue, contexte.Journal));
        Assert.Throws<ArgumentNullException>(() => new ControleurCommande(contexte.Modele, null!, contexte.Journal));
        Assert.Throws<ArgumentNullException>(() => new ControleurCommande(contexte.Modele, vue, null!));
        vue.Reaction = _ => throw new InvalidOperationException();
        Assert.Throws<InvalidOperationException>(() => new ControleurCommande(contexte.Modele, vue, contexte.Journal));
        using IDisposable abonnement = contexte.Modele.Subscribe(vue);
    }

    [Fact]
    public void Controleur_SuccesAfficheUneSeuleFoisEtRefusNActualisePas()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        using ControleurCommande controleur = new ControleurCommande(contexte.Modele, vue, contexte.Journal);
        Assert.Single(vue.Etats);
        controleur.Nouvelle(101);
        controleur.Ajouter(1, 2);
        controleur.Ajouter(2, 1);
        controleur.Ajouter(1, 0);
        controleur.Enregistrer();
        Assert.Equal(5, vue.Etats.Count);
        Assert.Single(vue.Refus);
        Assert.Empty(vue.Incidents);
        Assert.Equal(24.20m, vue.Etats.Last().Facture.Total);
    }

    [Theory]
    [InlineData("io")]
    [InlineData("json")]
    [InlineData("donnees")]
    [InlineData("acces")]
    public void Controleur_IncidentDistinctDuRefusEtSansDetailPrive(string type)
    {
        Exception erreur;
        switch (type)
        {
            case "io":
                erreur = new IOException("detail prive");
                break;
            case "json":
                erreur = new JsonException("detail prive");
                break;
            case "donnees":
                erreur = new InvalidDataException("detail prive");
                break;
            default:
                erreur = new UnauthorizedAccessException("detail prive");
                break;
        }
        var contexte = Creer(new DepotPanne(erreur));
        VueEspion vue = new VueEspion();
        using ControleurCommande controleur = new ControleurCommande(contexte.Modele, vue, contexte.Journal);
        controleur.Ajouter(1, 1);
        controleur.Enregistrer();
        Assert.Equal(2, vue.Etats.Count);
        Assert.Empty(vue.Refus);
        Assert.Single(vue.Incidents);
        Assert.DoesNotContain("detail prive", vue.Incidents.Single());
        Assert.Equal("enregistrer commande", contexte.Journal.Operations.Single());
        Assert.False(contexte.Modele.Consulter().Commande.EstEnregistree);
    }

    [Fact]
    public void Controleur_DisposeDeuxFoisDetacheEtRefuseActionsUlterieures()
    {
        var contexte = Creer();
        VueEspion vue = new VueEspion();
        ControleurCommande controleur = new ControleurCommande(contexte.Modele, vue, contexte.Journal);
        controleur.Dispose();
        controleur.Dispose();
        contexte.Modele.Ajouter(1, 1);
        Assert.Single(vue.Etats);
        Assert.Throws<ObjectDisposedException>(() => controleur.Ajouter(1, 1));
        Assert.Throws<ObjectDisposedException>(() => controleur.Nouvelle(2));
        Assert.Throws<ObjectDisposedException>(controleur.Enregistrer);
    }

    private sealed class VueEspion : IVueCommande
    {
        private readonly List<EtatCommandeDto> m_etats = new List<EtatCommandeDto>();
        private readonly List<string> m_refus = new List<string>();
        private readonly List<string> m_incidents = new List<string>();
        public IReadOnlyCollection<EtatCommandeDto> Etats
        {
            get
            {
                return m_etats;
            }
        }
        public IReadOnlyCollection<string> Refus
        {
            get
            {
                return m_refus;
            }
        }
        public IReadOnlyCollection<string> Incidents
        {
            get
            {
                return m_incidents;
            }
        }
        public Action<EtatCommandeDto>? Reaction { get; set; }

        public void OnNext(EtatCommandeDto etat)
        {
            m_etats.Add(etat);
            Reaction?.Invoke(etat);
        }

        public void OnError(Exception erreur)
        {
        }
        public void OnCompleted()
        {
        }

        public void PresenterRefus(string message)
        {
            m_refus.Add(message);
        }

        public void PresenterIncident(string message)
        {
            m_incidents.Add(message);
        }
    }

    private sealed class JournalEspion : IJournalIncidents
    {
        private readonly List<string> m_operations = new List<string>();
        public IReadOnlyCollection<string> Operations
        {
            get
            {
                return m_operations;
            }
        }
        public void Consigner(string operation, Exception exception)
        {
            m_operations.Add(operation);
        }
    }

    private sealed class DepotPanne : IDepotCommandes
    {
        private readonly Exception m_erreur;
        public DepotPanne(Exception erreur)
        {
            m_erreur = erreur;
        }

        public Commande? Obtenir(int numero)
        {
            return null;
        }

        public void Ajouter(Commande commande)
        {
            throw m_erreur;
        }
    }
}
