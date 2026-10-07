using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Commandes.Presentation;
using Xunit;

namespace Commandes.Tests;

public sealed class FacturationTests
{
    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(22, 22, 2.2, 24.2)]
    [InlineData(0.045, 0.05, 0.01, 0.06)]
    [InlineData(0.015, 0.02, 0, 0.02)]
    public void Facture_NormaliseEtArronditLesMontants(double entree, double sousTotal, double frais, double total)
    {
        Facture facture = new((decimal)entree);
        Assert.Equal((decimal)sousTotal, facture.SousTotal);
        Assert.Equal((decimal)frais, facture.Frais);
        Assert.Equal((decimal)total, facture.Total);
    }

    [Fact]
    public void Facture_RefuseNegatifEtNeMasquePasDepassement()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Facture(-0.01m));
        Assert.Throws<OverflowException>(() => new Facture(decimal.MaxValue));
    }

    [Fact]
    public void ServiceFacturation_RefuseDependanceNulle()
    {
        Assert.Throws<ArgumentNullException>(() => new ServiceFacturation(null!));
    }

    [Fact]
    public void ServiceFacturation_SuitLaCommandeEtConserveAncienDto()
    {
        ServiceCommandes commandes = new(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        ServiceFacturation service = new(commandes);
        FactureDto initiale = service.Consulter();
        commandes.Creer(101);
        commandes.AjouterProduit(1, 2);
        commandes.AjouterProduit(2, 1);
        FactureDto provisoire = service.Consulter();
        Assert.Equal(101, provisoire.NumeroCommande);
        Assert.Equal(24.20m, provisoire.Total);
        Assert.True(provisoire.EstProvisoire);
        commandes.Enregistrer();
        Assert.False(service.Consulter().EstProvisoire);
        Assert.True(provisoire.EstProvisoire);
        Assert.Equal(0m, initiale.Total);
    }

    [Fact]
    public void EtatCommandeDto_RefuseSesComposantesNulles()
    {
        ServiceCommandes commandes = new(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        Assert.Throws<ArgumentNullException>(() => new EtatCommandeDto(null!, new ServiceFacturation(commandes).Consulter()));
        Assert.Throws<ArgumentNullException>(() => new EtatCommandeDto(commandes.Consulter(), null!));
    }
}
