using Commandes.Domaine;

namespace Commandes.Application;

public interface IDepotCommandes
{
    void Ajouter(Commande commande);
    Commande? Obtenir(int numero);
}

public interface ICatalogueProduits
{
    Produit? Obtenir(int numero);
    IReadOnlyCollection<Produit> ObtenirTous();
}

public interface IJournalIncidents
{
    void Consigner(string operation, Exception exception);
}
