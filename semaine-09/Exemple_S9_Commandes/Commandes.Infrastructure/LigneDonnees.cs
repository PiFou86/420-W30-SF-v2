namespace Commandes.Infrastructure;

internal sealed class LigneDonnees
{
    public int NumeroProduit { get; }
    public string? Nom { get; }
    public decimal Prix { get; }
    public int Quantite { get; }

    public LigneDonnees(int numeroProduit, string? nom, decimal prix, int quantite)
    {
        NumeroProduit = numeroProduit;
        Nom = nom;
        Prix = prix;
        Quantite = quantite;
    }
}
