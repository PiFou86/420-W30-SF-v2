namespace Commandes.Domaine;

public sealed class LigneCommande
{
    public Produit Produit { get; }
    public int Quantite { get; }
    public decimal Montant => Produit.Prix * Quantite;

    public LigneCommande(Produit produit, int quantite)
    {
        ArgumentNullException.ThrowIfNull(produit);
        if (quantite <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantite));
        }

        Produit = produit;
        Quantite = quantite;
    }
}
