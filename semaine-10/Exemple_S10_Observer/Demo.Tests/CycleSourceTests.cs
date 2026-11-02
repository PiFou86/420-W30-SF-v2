using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.DotNet;
using Xunit;

namespace Commandes.Tests;

public sealed class CycleSourceTests
{
    private static ModeleCommande Creer()
    {
        ServiceCommandes commandes = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        return new ModeleCommande(commandes, new ServiceFacturation(commandes), new JournalSilencieux());
    }

    [Fact]
    public void Contrats_SontLesInterfacesSystem()
    {
        using ModeleCommande modele = Creer();
        IObservable<EtatCommandeDto> source = modele;
        IObserver<EtatCommandeDto> vue = new Espion();
        using IDisposable abonnement = source.Subscribe(vue);
        modele.Ajouter(1, 1);
        Assert.Equal(1, ((Espion)vue).Valeurs);
    }

    [Fact]
    public void FinNormale_NotifieUneFoisEtPermetDisposeDesAbonnements()
    {
        ModeleCommande modele = Creer();
        Espion premier = new Espion();
        Espion second = new Espion();
        IDisposable a = modele.Subscribe(premier);
        using IDisposable b = modele.Subscribe(second);
        modele.Dispose();
        modele.Dispose();
        a.Dispose();
        a.Dispose();
        Assert.Equal(1, premier.Fins);
        Assert.Equal(1, second.Fins);
        Assert.Equal(0, premier.Erreurs);
    }

    [Fact]
    public void RetirerUneVue_NeTerminePasLaSource()
    {
        using ModeleCommande modele = Creer();
        Espion premier = new Espion();
        Espion second = new Espion();
        IDisposable a = modele.Subscribe(premier);
        using IDisposable b = modele.Subscribe(second);
        a.Dispose();
        modele.Ajouter(1, 1);
        Assert.Equal(0, premier.Fins);
        Assert.Equal(0, second.Fins);
        Assert.Equal(0, premier.Valeurs);
        Assert.Equal(1, second.Valeurs);
    }

    [Fact]
    public void FinEnErreur_TermineLaSourceSansOnCompleted()
    {
        using ModeleCommande modele = Creer();
        Espion vue = new Espion();
        using IDisposable abonnement = modele.Subscribe(vue);
        IOException erreur = new IOException("détail privé");
        modele.SignalerErreurFatale(erreur);
        modele.SignalerErreurFatale(erreur);
        Assert.Equal(1, vue.Erreurs);
        Assert.Same(erreur, vue.DerniereErreur);
        Assert.Equal(0, vue.Fins);
        Assert.Throws<ObjectDisposedException>(() => modele.Ajouter(1, 1));
    }

    [Fact]
    public void SourceTerminee_RefuseNouveauxAbonnementsEtActions()
    {
        ModeleCommande modele = Creer();
        modele.Dispose();
        Assert.Throws<ObjectDisposedException>(() => modele.Subscribe(new Espion()));
        Assert.Throws<ObjectDisposedException>(() => modele.Nouvelle(2));
        Assert.Throws<ObjectDisposedException>(() => modele.Enregistrer());
        Assert.Throws<ArgumentNullException>(() => modele.SignalerErreurFatale(null!));
    }

    [Fact]
    public void FinDefaillante_NempechePasLesAutresObservateurs()
    {
        ModeleCommande modele = Creer();
        using IDisposable a = modele.Subscribe(new FinDefaillante());
        Espion valide = new Espion();
        using IDisposable b = modele.Subscribe(valide);
        modele.Dispose();
        Assert.Equal(1, valide.Fins);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void FinDuFlux_DesabonnementPendantLeRappelNePrivePasLesAutres(bool enErreur)
    {
        using ModeleCommande modele = Creer();
        Espion premier = new Espion();
        Espion second = new Espion();
        using IDisposable abonnement = modele.Subscribe(premier);
        using IDisposable autreAbonnement = modele.Subscribe(second);
        premier.TerminerAbonnement = abonnement.Dispose;

        if (enErreur)
        {
            modele.SignalerErreurFatale(new IOException("Incident simulé."));
            Assert.Equal(1, premier.Erreurs);
            Assert.Equal(1, second.Erreurs);
            Assert.Equal(0, second.Fins);
        }
        else
        {
            modele.Dispose();
            Assert.Equal(1, premier.Fins);
            Assert.Equal(1, second.Fins);
            Assert.Equal(0, second.Erreurs);
        }

        abonnement.Dispose();
        modele.Dispose();
        Assert.Equal(1, second.Fins + second.Erreurs);
    }

    private sealed class Espion : IObserver<EtatCommandeDto>
    {
        public int Valeurs { get; private set; }
        public int Fins { get; private set; }
        public int Erreurs { get; private set; }
        public Exception? DerniereErreur { get; private set; }
        public Action? TerminerAbonnement { get; set; }

        public void OnNext(EtatCommandeDto etat)
        {
            Valeurs++;
        }

        public void OnCompleted()
        {
            Fins++;
            TerminerAbonnement?.Invoke();
        }

        public void OnError(Exception erreur)
        {
            Erreurs++;
            DerniereErreur = erreur;
            TerminerAbonnement?.Invoke();
        }
    }

    private sealed class FinDefaillante : IObserver<EtatCommandeDto>
    {
        public void OnNext(EtatCommandeDto etat)
        {
        }

        public void OnError(Exception erreur)
        {
        }

        public void OnCompleted()
        {
            throw new InvalidOperationException("Erreur d'affichage simulée.");
        }
    }

    private sealed class JournalSilencieux : IJournalIncidents
    {
        public void Consigner(string operation, Exception erreur)
        {
        }
    }
}
