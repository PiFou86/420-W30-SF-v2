namespace Commandes.Application;

public sealed class LigneCommandeDto
{
    public string Produit { get; }
    public int Quantite { get; }
    public decimal PrixUnitaire { get; }
    public decimal Montant { get; }

    public LigneCommandeDto(string produit, int quantite, decimal prixUnitaire, decimal montant)
    {
        Produit = produit;
        Quantite = quantite;
        PrixUnitaire = prixUnitaire;
        Montant = montant;
    }
}
