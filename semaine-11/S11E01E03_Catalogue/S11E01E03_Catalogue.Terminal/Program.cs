using Catalogue;

internal static class Program
{
    public static void Main(string[] args)
    {
        List<Produit> produits = DonneesCatalogue.CreerProduits();
        List<Categorie> categories = DonneesCatalogue.CreerCategories();
        Console.Out.WriteLine($"Catalogue chargé : {produits.Count} produits, {categories.Count} catégories.");
    }
}
