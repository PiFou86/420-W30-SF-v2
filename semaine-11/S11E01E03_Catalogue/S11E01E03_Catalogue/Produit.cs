namespace Catalogue;
public sealed class Produit
{
    public Produit(int numero, string code, string nom, decimal prix, int stock, string categorie)
    {
        Numero = numero;
        Code = code;
        Nom = nom;
        Prix = prix;
        Stock = stock;
        Categorie = categorie;
    }

    public int Numero { get; }
    public string Code { get; }
    public string Nom { get; }
    public decimal Prix { get; }
    public int Stock { get; }
    public string Categorie { get; }
}
