namespace Collaboration;

public sealed class Produit
{
    public Produit(int numero, string nom, bool enStock)
    {
        Numero = numero;
        Nom = nom;
        EnStock = enStock;
    }

    public int Numero { get; }
    public string Nom { get; }
    public bool EnStock { get; }
}

public sealed class Categorie
{
    public Categorie(int numero, string nom)
    {
        Numero = numero;
        Nom = nom;
    }

    public int Numero { get; }
    public string Nom { get; }
}
