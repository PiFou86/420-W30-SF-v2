using Commandes.Domaine;

namespace Commandes.Application;

public interface ICatalogueProduits
{
    Produit? Obtenir(int numero);
    IReadOnlyCollection<Produit> ObtenirTous();
}
