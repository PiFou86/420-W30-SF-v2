using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class ServiceCommandesTests
{
    [Fact]
    public void Constructeur_RefuseDependancesNulles()
    {
        Assert.Throws<ArgumentNullException>(() => new ServiceCommandes(null!, new CatalogueProduitsMemoire()));
        Assert.Throws<ArgumentNullException>(() => new ServiceCommandes(new DepotCommandesMemoire(), null!));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Creer_NumeroInvalideConserveCommande(int numero)
    {
        ServiceCommandes service = new(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.AjouterProduit(1, 1);
        Assert.False(service.Creer(numero).EstSucces);
        Assert.Single(service.Consulter().Lignes);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(99, 1)]
    public void Ajouter_RefusNeModifieNiCommandeNiDepot(int produit, int quantite)
    {
        DepotObservateur depot = new();
        ServiceCommandes service = new(depot, new CatalogueProduitsMemoire());
        Assert.False(service.AjouterProduit(produit, quantite).EstSucces);
        Assert.Empty(service.Consulter().Lignes);
        Assert.Equal(0, depot.NombreAjouts);
    }

    [Fact]
    public void Enregistrer_VideNeConsulteNiAjoute()
    {
        DepotObservateur depot = new();
        Assert.False(new ServiceCommandes(depot, new CatalogueProduitsMemoire()).Enregistrer().EstSucces);
        Assert.Equal(0, depot.NombreLectures);
        Assert.Equal(0, depot.NombreAjouts);
    }

    [Fact]
    public void Enregistrer_SuccesUnAjoutPuisSecondAppelRefuse()
    {
        DepotObservateur depot = new();
        ServiceCommandes service = new(depot, new CatalogueProduitsMemoire());
        service.Creer(101);
        service.AjouterProduit(1, 2);
        service.AjouterProduit(2, 1);
        Result<CommandeDto> resultat = service.Enregistrer();
        Assert.True(resultat.EstSucces);
        Assert.Equal(22m, resultat.Valeur.Total);
        Assert.True(resultat.Valeur.EstEnregistree);
        Assert.True(depot.Derniere!.EstConfirmee);
        Assert.False(service.Enregistrer().EstSucces);
        Assert.False(service.AjouterProduit(1, 1).EstSucces);
        Assert.Equal(1, depot.NombreAjouts);
    }

    [Fact]
    public void Enregistrer_DoublonNePersistPas()
    {
        DepotObservateur depot = new() { Existante = new Commande(1) };
        ServiceCommandes service = new(depot, new CatalogueProduitsMemoire());
        service.AjouterProduit(1, 1);
        Assert.False(service.Enregistrer().EstSucces);
        Assert.Equal(0, depot.NombreAjouts);
        Assert.False(service.Consulter().EstEnregistree);
    }

    [Fact]
    public void Enregistrer_IncidentEcritureConserveBrouillonEtPermetNouvelEssai()
    {
        IOException incident = new("secret simulé");
        DepotObservateur depot = new() { IncidentAjout = incident };
        ServiceCommandes service = new(depot, new CatalogueProduitsMemoire());
        service.AjouterProduit(1, 2);
        Assert.Same(incident, Assert.Throws<IOException>(() => service.Enregistrer()));
        Assert.False(service.Consulter().EstEnregistree);
        Assert.Equal(13m, service.Consulter().Total);
        depot.IncidentAjout = null;
        Assert.True(service.Enregistrer().EstSucces);
    }

    [Fact]
    public void Enregistrer_IncidentLectureNeDevientPasRefusMetier()
    {
        IOException incident = new("incident");
        DepotObservateur depot = new() { IncidentLecture = incident };
        ServiceCommandes service = new(depot, new CatalogueProduitsMemoire());
        service.AjouterProduit(1, 1);
        Assert.Same(incident, Assert.Throws<IOException>(() => service.Enregistrer()));
        Assert.Equal(0, depot.NombreAjouts);
    }

    [Fact]
    public void Sortie_InstantaneStableEtNouvelleCommandeVide()
    {
        ServiceCommandes service = new(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        CommandeDto avant = service.AjouterProduit(1, 1).Valeur;
        service.AjouterProduit(2, 1);
        Assert.Single(avant.Lignes);
        Assert.Equal(6.50m, avant.Total);
        Assert.Empty(service.Creer(2).Valeur.Lignes);
        Assert.Equal(3, service.ConsulterProduits().Count);
        Assert.Throws<ArgumentNullException>(() => new CommandeDto(null!));
    }

    private sealed class DepotObservateur : IDepotCommandes
    {
        public int NombreAjouts { get; private set; }
        public int NombreLectures { get; private set; }
        public Commande? Existante { get; init; }
        public Commande? Derniere { get; private set; }
        public IOException? IncidentAjout { get; set; }
        public IOException? IncidentLecture { get; init; }

        public void Ajouter(Commande commande)
        {
            if (IncidentAjout is not null)
            {
                throw IncidentAjout;
            }

            Derniere = commande;
            NombreAjouts++;
        }

        public Commande? Obtenir(int numero)
        {
            NombreLectures++;
            if (IncidentLecture is not null)
            {
                throw IncidentLecture;
            }

            return Existante;
        }
    }
}
