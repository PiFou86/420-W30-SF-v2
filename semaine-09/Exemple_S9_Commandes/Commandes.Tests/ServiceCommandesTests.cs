using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class ServiceCommandesTests
{
    [Fact]
    public void Demarrage_AucuneCommandeEtOperationsRefusees()
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        Assert.Null(service.Consulter());
        Assert.False(service.AjouterProduit(2, 2).EstSucces);
        Assert.False(service.Enregistrer().EstSucces);
        Assert.Null(service.Consulter());
    }

    [Fact]
    public void Creer_PuisAjouterPuisEnregistrerPuisRecreerConserveLaPrecedente()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        CommandeDto creee = service.Creer(1042).Valeur;
        Assert.Equal(1042, creee.Numero);
        Assert.Empty(creee.Lignes);
        Assert.Equal(0m, creee.Total);
        Assert.Null(depot.Obtenir(1042));
        CommandeDto ajoutee = service.AjouterProduit(2, 2).Valeur;
        Assert.Equal(16m, ajoutee.Total);
        Assert.False(ajoutee.EstEnregistree);
        Assert.True(service.Enregistrer().Valeur.EstEnregistree);
        Assert.False(service.AjouterProduit(2, 1).EstSucces);
        Assert.False(service.Enregistrer().EstSucces);
        CommandeDto nouvelle = service.Creer(1043).Valeur;
        Assert.Empty(nouvelle.Lignes);
        Assert.False(nouvelle.EstEnregistree);
        Assert.Equal(1043, nouvelle.Numero);
        Assert.Equal(16m, depot.Obtenir(1042)!.Total);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Creer_NumeroInvalideConserveLeBrouillonMemeSiAbandonConfirme(int numero)
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 2);
        Assert.False(service.Creer(numero, abandonnerBrouillon: true).EstSucces);
        Assert.Equal(1042, service.Consulter()!.Numero);
        Assert.Equal(16m, service.Consulter()!.Total);
    }

    [Fact]
    public void Creer_SansConfirmationRefuseLaPerteDesLignes()
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 2);
        Assert.False(service.Creer(1043).EstSucces);
        Assert.Equal(1042, service.Consulter()!.Numero);
        Assert.Single(service.Consulter()!.Lignes);
    }

    [Fact]
    public void Creer_AvecAbandonExpliciteRemplaceSeulementLeBrouillon()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 2);
        Assert.True(service.Creer(1043, abandonnerBrouillon: true).EstSucces);
        Assert.Equal(1043, service.Consulter()!.Numero);
        Assert.Empty(service.Consulter()!.Lignes);
        Assert.Null(depot.Obtenir(1042));
    }

    [Fact]
    public void Creer_NumeroDejaEnregistreConserveLeBrouillon()
    {
        DepotCommandesMemoire depot = new DepotCommandesMemoire();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 1);
        service.Enregistrer();
        service.Creer(1043);
        service.AjouterProduit(2, 2);
        Assert.False(service.Creer(1042, abandonnerBrouillon: true).EstSucces);
        Assert.Equal(1043, service.Consulter()!.Numero);
        Assert.Equal(16m, service.Consulter()!.Total);
    }

    [Fact]
    public void Creer_IncidentLectureNeRemplacePasLeBrouillon()
    {
        DepotIncident depot = new DepotIncident();
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 2);
        depot.EchouerLecture = true;
        Assert.Throws<IOException>(() => service.Creer(1043, abandonnerBrouillon: true));
        Assert.Equal(1042, service.Consulter()!.Numero);
        Assert.Equal(16m, service.Consulter()!.Total);
    }

    [Fact]
    public void Enregistrer_IncidentPuisNouvelEssaiConserveLesLignes()
    {
        DepotIncident depot = new DepotIncident() { EchouerAjout = true };
        ServiceCommandes service = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
        service.Creer(1042);
        service.AjouterProduit(2, 2);
        Assert.Throws<IOException>(() => service.Enregistrer());
        Assert.False(service.Consulter()!.EstEnregistree);
        Assert.Equal(16m, service.Consulter()!.Total);
        depot.EchouerAjout = false;
        Assert.True(service.Enregistrer().Valeur.EstEnregistree);
    }

    [Fact]
    public void Enregistrer_CommandeVideEstUnRefus()
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.Creer(1042);
        Assert.False(service.Enregistrer().EstSucces);
        Assert.False(service.Consulter()!.EstEnregistree);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(999, 1)]
    [InlineData(2, 0)]
    [InlineData(2, -1)]
    public void Ajouter_RefusMetierNeModifiePasLaCommande(int produit, int quantite)
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.Creer(1042);
        Assert.False(service.AjouterProduit(produit, quantite).EstSucces);
        Assert.Empty(service.Consulter()!.Lignes);
    }

    [Fact]
    public void Dto_NeChangePasApresUneNouvelleOperation()
    {
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        CommandeDto vide = service.Creer(1042).Valeur;
        CommandeDto uneLigne = service.AjouterProduit(2, 2).Valeur;
        service.AjouterProduit(1, 1);
        Assert.Empty(vide.Lignes);
        Assert.Single(uneLigne.Lignes);
        Assert.Equal(16m, uneLigne.Total);
        Assert.Equal(2, service.Consulter()!.Lignes.Count);
    }

    [Fact]
    public void Domaine_ProtegeLesInvariantsSansInterfaceGraphique()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Commande(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new LigneCommande(new Produit(1, "Sandwich", 8m), 0));
        Assert.Throws<InvalidOperationException>(() => new Commande(1).Confirmer());
    }
}
