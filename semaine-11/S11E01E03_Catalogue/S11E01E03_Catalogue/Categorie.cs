namespace Catalogue;
public sealed class Categorie
{
    public Categorie(string code, string nom)
    {
        Code = code;
        Nom = nom;
    }

    public string Code { get; }
    public string Nom { get; }
}
