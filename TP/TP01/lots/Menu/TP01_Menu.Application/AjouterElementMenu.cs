namespace Menu.Application;

public sealed class AjouterElementMenu
{
    private readonly IDepotMenu m_depotMenu;

    public AjouterElementMenu(IDepotMenu depotMenu)
    {
        ArgumentNullException.ThrowIfNull(depotMenu);
        m_depotMenu = depotMenu;
    }

    // TODO : orchestrer l'ajout et laisser les invariants au domaine.
}
