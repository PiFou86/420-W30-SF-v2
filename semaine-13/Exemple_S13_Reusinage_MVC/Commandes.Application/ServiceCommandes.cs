using Commandes.Domaine;

namespace Commandes.Application;

public sealed class ServiceCommandes
{
    private readonly IDepotCommandes m_depot;

    public ServiceCommandes(IDepotCommandes depot)
    {
        m_depot = depot;
    }

    public Result<decimal> ConsulterTotal(int identifiant)
    {
        Commande? commande = m_depot.Obtenir(identifiant);
        if (commande is null)
        {
            return Result<decimal>.Echec("Commande introuvable.");
        }
        return Result<decimal>.Succes(commande.CalculerTotal());
    }
}
