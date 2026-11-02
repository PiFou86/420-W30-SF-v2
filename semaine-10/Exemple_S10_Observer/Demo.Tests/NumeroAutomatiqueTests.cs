using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Commandes.Presentation.DotNet;
using Xunit;

namespace Commandes.Tests;

public sealed class NumeroAutomatiqueTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreationAutomatique_EviteLesNumerosPersistesEtFonctionneApresRelecture(bool json)
    {
        string chemin = Path.Combine(Path.GetTempPath(), $"s10-auto-{Guid.NewGuid()}.json");
        try
        {
            IDepotCommandes depot;
            if (json)
            {
                depot = new DepotCommandesJson(chemin);
            }
            else
            {
                depot = new DepotCommandesMemoire();
            }

            AjouterCommande(depot, 1);
            AjouterCommande(depot, 3);
            byte[]? avant = null;
            if (json)
            {
                avant = File.ReadAllBytes(chemin);
            }

            ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
            Result<CommandeDto> premiere = service.CreerAutomatiquement();
            Assert.True(premiere.EstSucces);
            Assert.Equal(2, premiere.Valeur.Numero);
            Assert.Null(depot.Obtenir(2));
            if (json)
            {
                Assert.Equal(avant, File.ReadAllBytes(chemin));
            }

            service.AjouterProduit(1, 1);
            Assert.True(service.Enregistrer().EstSucces);
            Assert.Equal(4, service.CreerAutomatiquement().Valeur.Numero);
            service.AjouterProduit(1, 1);
            Assert.True(service.Enregistrer().EstSucces);

            IDepotCommandes relu = depot;
            if (json)
            {
                relu = new DepotCommandesJson(chemin);
            }

            ServiceCommandes nouveauService = new ServiceCommandes(relu, new CatalogueProduitsMemoire());
            Assert.Equal(5, nouveauService.CreerAutomatiquement().Valeur.Numero);
            Assert.NotNull(relu.Obtenir(1));
            Assert.NotNull(relu.Obtenir(2));
            Assert.NotNull(relu.Obtenir(3));
            Assert.NotNull(relu.Obtenir(4));
        }
        finally
        {
            if (File.Exists(chemin))
            {
                File.Delete(chemin);
            }
        }
    }

    [Fact]
    public void BrouillonsSuccessifs_OntDesNumerosDistinctsSansEcriture()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        Assert.Equal(1, service.CreerAutomatiquement().Valeur.Numero);
        service.AjouterProduit(1, 2);
        Assert.Equal(2, service.CreerAutomatiquement().Valeur.Numero);
        Assert.Empty(service.Consulter().Lignes);
        Assert.Null(depot.Obtenir(1));
        Assert.Null(depot.Obtenir(2));
    }

    [Fact]
    public void NouvelleAutomatique_NotifieUneFoisEtConserveLeBrouillonSiLaLectureEchoue()
    {
        DepotLectureInterrompue depot = new DepotLectureInterrompue();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        using ModeleCommande modele = new ModeleCommande(service, new ServiceFacturation(service), new JournalSilencieux());
        VueEspion vue = new VueEspion();
        using ControleurCommande controleur = new ControleurCommande(modele, vue, new JournalSilencieux());
        controleur.Nouvelle();
        Assert.Equal(2, vue.Etats.Count); // État initial puis création automatique.
        controleur.Ajouter(1, 2);
        EtatCommandeDto avant = vue.Etats.Last();
        depot.EstEnPanne = true;

        controleur.Nouvelle();

        Assert.Equal(3, vue.Etats.Count);
        Assert.Same(avant, vue.Etats.Last());
        Assert.Equal(13m, service.Consulter().Total);
        Assert.Single(vue.Incidents);
        Assert.DoesNotContain("privé", vue.Incidents.Single());
    }

    private static void AjouterCommande(IDepotCommandes depot, int numero)
    {
        Commande commande = new Commande(numero);
        commande.Ajouter(new Produit(1, "Soupe", 6.50m), 1);
        commande.Confirmer();
        depot.Ajouter(commande);
    }

    private sealed class DepotLectureInterrompue : IDepotCommandes
    {
        public bool EstEnPanne { get; set; }
        public Commande? Obtenir(int numero)
        {
            if (EstEnPanne)
            {
                throw new IOException("Détail privé.");
            }

            return null;
        }

        public void Ajouter(Commande commande)
        {
            throw new InvalidOperationException("Écriture non prévue dans ce test.");
        }
    }

    private sealed class VueEspion : IVueCommande
    {
        public List<EtatCommandeDto> Etats { get; } = new List<EtatCommandeDto>();
        public List<string> Incidents { get; } = new List<string>();
        public void OnNext(EtatCommandeDto etat)
        {
            Etats.Add(etat);
        }
        public void PresenterRefus(string message)
        {
            throw new InvalidOperationException(message);
        }
        public void PresenterIncident(string message)
        {
            Incidents.Add(message);
        }
        public void OnCompleted()
        {
        }
        public void OnError(Exception erreur)
        {
        }
    }

    private sealed class JournalSilencieux : IJournalIncidents
    {
        public void Consigner(string operation, Exception erreur)
        {
        }
    }
}
