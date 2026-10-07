using Commandes.Application;
using Commandes.Domaine;

namespace Commandes.Infrastructure;

public sealed class DepotCommandesMemoire : IDepotCommandes
{
    private readonly Dictionary<int, Commande> m_commandes = new();

    public void Ajouter(Commande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);
        if (!m_commandes.TryAdd(commande.Numero, commande.Copier()))
        {
            throw new InvalidOperationException("Numéro déjà enregistré.");
        }
    }

    public Commande? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        return m_commandes.GetValueOrDefault(numero)?.Copier();
    }
}
