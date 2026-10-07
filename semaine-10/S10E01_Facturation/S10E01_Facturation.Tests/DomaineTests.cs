using Commandes.Domaine;
using Xunit;

namespace Commandes.Tests;

public sealed class DomaineTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructeurs_RefusentNumeroNonPositif(int numero)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(numero, "A", 1m));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Commande(numero));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Produit_RefuseNomInvalide(string? nom)
    {
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => new Produit(1, nom!, 1m)));
    }

    [Fact]
    public void Produit_PrixZeroValideEtPrixNegatifRefuse()
    {
        Assert.Equal(0, new Produit(1, "Eau", 0m).Prix);
        Assert.Throws<ArgumentOutOfRangeException>(() => new Produit(1, "A", -0.01m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void LigneEtAjout_RefusentQuantiteNonPositive(int quantite)
    {
        Produit produit = new(1, "A", 1m);
        Assert.Throws<ArgumentOutOfRangeException>(() => new LigneCommande(produit, quantite));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Commande(1).Ajouter(produit, quantite));
    }

    [Fact]
    public void LigneEtAjout_RefusentProduitNull()
    {
        Assert.Throws<ArgumentNullException>(() => new LigneCommande(null!, 1));
        Assert.Throws<ArgumentNullException>(() => new Commande(1).Ajouter(null!, 1));
    }

    [Fact]
    public void Total_CalculePlusieursLignesEtCopieIndependante()
    {
        Commande commande = new(101);
        commande.Ajouter(new Produit(1, "Soupe", 6.50m), 2);
        commande.Ajouter(new Produit(2, "Sandwich", 9m), 1);
        Assert.Equal(22m, commande.Total);
        Commande copie = commande.Copier();
        copie.Ajouter(new Produit(3, "Autre", 1m), 1);
        Assert.Equal(22m, commande.Total);
        Assert.Equal(23m, copie.Total);
    }

    [Fact]
    public void Confirmation_RefuseVideEtProtegeCommandeConfirmee()
    {
        Commande commande = new(1);
        Assert.Throws<InvalidOperationException>(() => commande.Confirmer());
        commande.Ajouter(new Produit(1, "A", 1m), 1);
        commande.Confirmer();
        Assert.True(commande.Copier().EstConfirmee);
        Assert.Throws<InvalidOperationException>(() => commande.Ajouter(new Produit(2, "B", 2m), 1));
    }

    [Fact]
    public void Lignes_LaCopieNeModifiePasLaCollectionInterne()
    {
        Commande commande = new(1);
        commande.Ajouter(new Produit(1, "A", 1m), 1);
        LigneCommande[] copie = Assert.IsType<LigneCommande[]>(commande.Lignes);
        copie[0] = new LigneCommande(new Produit(2, "B", 2m), 1);
        Assert.Equal(1m, commande.Total);
    }
}
