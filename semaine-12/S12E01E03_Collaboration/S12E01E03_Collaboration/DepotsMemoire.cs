namespace Collaboration;

public sealed class DepotProduitsMemoire : IDepotProduits
{
    private readonly IReadOnlyList<Produit> m_produits = Array.AsReadOnly(new[]
    {
        new Produit(3, "Soupe du jour", true),
        new Produit(1, "Soupe épicée", false),
        new Produit(2, "Soupe aux légumes", true)
    });

    public Produit? Obtenir(int numero) => m_produits.SingleOrDefault(p => p.Numero == numero);
    public IReadOnlyList<Produit> ObtenirTous() => m_produits;
    public IReadOnlyList<Produit> Rechercher(string fragment, int debut, int taille) =>
        RequeteCatalogue.Rechercher(m_produits, fragment, debut, taille);
}

public sealed class DepotCategoriesMemoire : IDepotCategories
{
    private readonly IReadOnlyList<Categorie> m_categories = Array.AsReadOnly(new[]
    {
        new Categorie(10, "Plats"),
        new Categorie(20, "Boissons")
    });

    public Categorie? Obtenir(int numero) => m_categories.SingleOrDefault(c => c.Numero == numero);
    public IReadOnlyList<Categorie> ObtenirTous() => m_categories;
}
