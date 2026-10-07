using Menu.Domaine;

namespace Menu.Application;

public interface IDepotMenu
{
    void Ajouter(ElementMenu element);
    ElementMenu? Obtenir(int numero);
    IReadOnlyCollection<ElementMenu> ObtenirToutes();
}
