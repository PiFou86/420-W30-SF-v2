namespace Collaboration;

public interface IDepotProduits
{
    Produit? Obtenir(int numero);
    IReadOnlyList<Produit> ObtenirTous();
    IReadOnlyList<Produit> Rechercher(string fragment, int debut, int taille);
}

public interface IDepotCategories
{
    Categorie? Obtenir(int numero);
    IReadOnlyList<Categorie> ObtenirTous();
}
