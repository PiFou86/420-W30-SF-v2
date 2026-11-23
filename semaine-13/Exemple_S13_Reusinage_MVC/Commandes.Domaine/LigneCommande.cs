namespace Commandes.Domaine;

public sealed class LigneCommande
{
    public LigneCommande(decimal prixUnitaire, int quantite)
    {
        PrixUnitaire = prixUnitaire;
        Quantite = quantite;
    }

    public decimal PrixUnitaire { get; }
    public int Quantite { get; }
}
