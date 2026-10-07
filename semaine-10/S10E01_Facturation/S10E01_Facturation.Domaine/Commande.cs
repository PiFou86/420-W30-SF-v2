namespace Commandes.Domaine;

public sealed class Commande
{
    private readonly List<LigneCommande> m_lignes = new();
    public int Numero { get; }
    public bool EstConfirmee { get; private set; }
    public IReadOnlyCollection<LigneCommande> Lignes => m_lignes.ToArray();

    public decimal Total
    {
        get
        {
            decimal total = 0;
            foreach (LigneCommande ligne in m_lignes)
            {
                total += ligne.Montant;
            }

            return total;
        }
    }

    public Commande(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        Numero = numero;
    }

    public void Ajouter(Produit produit, int quantite)
    {
        if (EstConfirmee)
        {
            throw new InvalidOperationException("La commande est confirmée.");
        }

        m_lignes.Add(new LigneCommande(produit, quantite));
    }

    public void Confirmer()
    {
        if (m_lignes.Count == 0)
        {
            throw new InvalidOperationException("La commande est vide.");
        }

        EstConfirmee = true;
    }

    public Commande Copier()
    {
        Commande copie = new(Numero);
        foreach (LigneCommande ligne in m_lignes)
        {
            copie.Ajouter(ligne.Produit, ligne.Quantite);
        }

        if (EstConfirmee)
        {
            copie.Confirmer();
        }

        return copie;
    }
}
