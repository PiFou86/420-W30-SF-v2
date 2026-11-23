using Commandes.Domaine;

namespace Commandes.Application;

public interface IDepotCommandes
{
    Commande? Obtenir(int identifiant);
}
