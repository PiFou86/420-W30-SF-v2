using Commandes.Application;
using Commandes.Domaine;

namespace Commandes.Infrastructure;

public sealed class CatalogueProduitsMemoire : ICatalogueProduits
{
    private readonly Dictionary<int, Produit> m_produits = new()
    {
        [1] = new Produit(1, "Soupe", 6.50m),
        [2] = new Produit(2, "Sandwich", 9m),
        [3] = new Produit(3, "Eau", 0m)
    };

    public Produit? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        return m_produits.GetValueOrDefault(numero);
    }

    public IReadOnlyCollection<Produit> ObtenirTous()
    {
        return m_produits.Values.ToArray();
    }
}
