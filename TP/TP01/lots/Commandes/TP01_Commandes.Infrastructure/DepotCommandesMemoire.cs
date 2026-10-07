using Commandes.Application;
using Commandes.Domaine;

namespace Commandes.Infrastructure;

public sealed class DepotCommandesMemoire : IDepotCommandes
{
    // TODO : choisir une collection privée et protéger ses accès.
    public void Ajouter(Commande element)
    {
        throw new NotImplementedException("Implanter l'ajout et son contrat.");
    }

    public Commande? Obtenir(int numero)
    {
        throw new NotImplementedException("Implanter la recherche et son contrat.");
    }

    public IReadOnlyCollection<Commande> ObtenirToutes()
    {
        throw new NotImplementedException("Implanter la copie de collection.");
    }
}
