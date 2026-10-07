using Menu.Application;
using Menu.Domaine;

namespace Menu.Infrastructure;

public sealed class DepotMenuMemoire : IDepotMenu
{
    // TODO : choisir une collection privée et protéger ses accès.
    public void Ajouter(ElementMenu element)
    {
        throw new NotImplementedException("Implanter l'ajout et son contrat.");
    }

    public ElementMenu? Obtenir(int numero)
    {
        throw new NotImplementedException("Implanter la recherche et son contrat.");
    }

    public IReadOnlyCollection<ElementMenu> ObtenirToutes()
    {
        throw new NotImplementedException("Implanter la copie de collection.");
    }
}
