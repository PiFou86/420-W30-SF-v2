namespace Commandes.Domaine;

public sealed class Commande
{
    private readonly List<LigneCommande> m_lignes;

    public Commande(int identifiant, IEnumerable<LigneCommande> lignes)
    {
        Identifiant = identifiant;
        m_lignes = new List<LigneCommande>(lignes);
    }

    public int Identifiant { get; }

    public decimal CalculerTotal()
    {
        decimal total = 0m;
        foreach (LigneCommande ligne in m_lignes)
        {
            total += ligne.PrixUnitaire * ligne.Quantite;
        }
        return total;
    }
}
