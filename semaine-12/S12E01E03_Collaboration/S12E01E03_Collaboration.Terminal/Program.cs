using Collaboration;

internal static class Program
{
    public static void Main(string[] args)
    {
        IDepotProduits produits = new DepotProduitsMemoire();
        IDepotCategories categories = new DepotCategoriesMemoire();
        Console.Out.WriteLine($"{produits.ObtenirTous().Count} produits, {categories.ObtenirTous().Count} catégories.");
    }
}
