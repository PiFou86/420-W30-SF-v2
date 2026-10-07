using Commandes.Domaine;

namespace Commandes.Application;

public interface IDepotCommandes
{
    void Ajouter(Commande element);
    Commande? Obtenir(int numero);
    IReadOnlyCollection<Commande> ObtenirToutes();
}
