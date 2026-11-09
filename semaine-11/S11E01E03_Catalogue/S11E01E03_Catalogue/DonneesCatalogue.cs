namespace Catalogue;
public static class DonneesCatalogue
{
    public static List<Produit> CreerProduits() =>
    [
        new Produit(9, "EAU", "Eau", 0m, 4, "B"),
        new Produit(1, "SOU", "Soupe", 6.5m, 0, "P"),
        new Produit(5, "THE", "Thé", 3m, 7, "B"),
        new Produit(7, "BOL", "Bol", 12m, 2, "P"),
        new Produit(4, "CAF", "Café", 3m, 9, "B"),
        new Produit(8, "TAR", "Tarte", 6.5m, 0, "D"),
        new Produit(2, "SAN", "Sandwich", 9m, 4, "P"),
        new Produit(6, "BIS", "Biscuit", 2.5m, 8, "D"),
        new Produit(3, "JUS", "Jus", 3m, 1, "X"),
        new Produit(10, "CAF", "Café filtre", 3m, 2, "B"),
        new Produit(11, "CAE", "Café expresso", 3m, 2, "B"),
        new Produit(12, "CAC", "Café cappuccino", 3m, 2, "B"),
        new Produit(13, "CHO", "Cholatine", 3m, 2, "D"),
        new Produit(14, "CRO", "Croissant", 3m, 2, "D"),
    ];

    public static List<Categorie> CreerCategories() =>
    [
        new Categorie("B", "Boissons"),
        new Categorie("P", "Plats"),
        new Categorie("D", "Desserts")
    ];
}
