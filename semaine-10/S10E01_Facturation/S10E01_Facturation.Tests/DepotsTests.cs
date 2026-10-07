using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class DepotsTests : IDisposable
{
    private readonly string m_dossier = Path.Combine(Path.GetTempPath(), $"commandes-tests-{Guid.NewGuid():N}");
    private string Chemin => Path.Combine(m_dossier, "commandes.json");

    public DepotsTests()
    {
        Directory.CreateDirectory(m_dossier);
    }

    private IDepotCommandes Creer(string format)
    {
        return format == "json" ? new DepotCommandesJson(Chemin) : new DepotCommandesMemoire();
    }

    [Theory]
    [InlineData("json")]
    [InlineData("memoire")]
    public void Contrat_PreconditionsAbsenceDoublonEtValeur(string format)
    {
        IDepotCommandes depot = Creer(format);
        Assert.Null(depot.Obtenir(1));
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(-1));
        Assert.Throws<ArgumentNullException>(() => depot.Ajouter(null!));
        Commande commande = new(1);
        commande.Ajouter(new Produit(1, "A", 6.5m), 2);
        commande.Confirmer();
        depot.Ajouter(commande);
        Assert.Equal(13m, depot.Obtenir(1)?.Total);
        Assert.True(depot.Obtenir(1)?.EstConfirmee);
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(commande));
    }

    [Fact]
    public void Memoire_ProtegeAjoutEtLectureParCopie()
    {
        DepotCommandesMemoire depot = new();
        Commande originale = new(1);
        originale.Ajouter(new Produit(1, "A", 1m), 1);
        depot.Ajouter(originale);
        originale.Ajouter(new Produit(2, "B", 9m), 1);
        Commande lue = depot.Obtenir(1)!;
        lue.Ajouter(new Produit(3, "C", 8m), 1);
        Assert.Equal(1m, depot.Obtenir(1)?.Total);
    }

    [Fact]
    public void Json_NouvelleInstanceReconstitueCommande()
    {
        ServiceCommandes service = new(new DepotCommandesJson(Chemin), new CatalogueProduitsMemoire());
        service.Creer(101);
        service.AjouterProduit(1, 2);
        service.AjouterProduit(2, 1);
        Assert.True(service.Enregistrer().EstSucces);
        Commande? relue = new DepotCommandesJson(Chemin).Obtenir(101);
        Assert.NotNull(relue);
        Assert.Equal(22m, relue.Total);
        Assert.Equal(2, relue.Lignes.Count);
        Assert.True(relue.EstConfirmee);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Json_RefuseCheminInvalide(string? chemin)
    {
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => new DepotCommandesJson(chemin!)));
    }

    [Theory]
    [InlineData("null")]
    [InlineData("[null]")]
    [InlineData("[{\"Numero\":0,\"Lignes\":[]}]")]
    [InlineData("[{\"Numero\":1,\"Lignes\":null}]")]
    [InlineData("[{\"Numero\":1,\"Lignes\":[null]}]")]
    [InlineData("[{\"Numero\":1,\"Lignes\":[]},{\"Numero\":1,\"Lignes\":[]}]")]
    [InlineData("[{\"Numero\":1,\"EstConfirmee\":true,\"Lignes\":[]}]")]
    [InlineData("[{\"Numero\":1,\"Lignes\":[{\"NumeroProduit\":1,\"Nom\":\"A\",\"Prix\":1,\"Quantite\":0}]}]")]
    [InlineData("[{\"Numero\":1,\"Lignes\":[{\"NumeroProduit\":1,\"Nom\":null,\"Prix\":1,\"Quantite\":1}]}]")]
    public void Json_DonneesInvalidesNeSontPasReecrites(string contenu)
    {
        File.WriteAllText(Chemin, contenu);
        DepotCommandesJson depot = new(Chemin);
        Assert.Throws<InvalidDataException>(() => depot.Obtenir(1));
        Assert.Throws<InvalidDataException>(() => depot.Ajouter(new Commande(2)));
        Assert.Equal(contenu, File.ReadAllText(Chemin));
    }

    [Fact]
    public void Json_SyntaxeInvalideListeVideEtDossierManquantDistincts()
    {
        File.WriteAllText(Chemin, "{invalide");
        Assert.Throws<JsonException>(() => new DepotCommandesJson(Chemin).Obtenir(1));
        File.WriteAllText(Chemin, "[]");
        Assert.Null(new DepotCommandesJson(Chemin).Obtenir(1));
        File.Delete(Chemin);
        Directory.Delete(m_dossier);
        Assert.Throws<DirectoryNotFoundException>(() => new DepotCommandesJson(Chemin).Obtenir(1));
    }

    [Fact]
    public void Catalogue_ContratEtCopieStructure()
    {
        CatalogueProduitsMemoire catalogue = new();
        Assert.Throws<ArgumentOutOfRangeException>(() => catalogue.Obtenir(0));
        Assert.Null(catalogue.Obtenir(99));
        Produit[] copie = Assert.IsType<Produit[]>(catalogue.ObtenirTous());
        copie[0] = new Produit(100, "B", 1m);
        Assert.Equal("Soupe", catalogue.Obtenir(1)?.Nom);
        Assert.Equal(0m, catalogue.Obtenir(3)?.Prix);
    }

    public void Dispose()
    {
        if (Directory.Exists(m_dossier))
        {
            Directory.Delete(m_dossier, recursive: true);
        }
    }
}
