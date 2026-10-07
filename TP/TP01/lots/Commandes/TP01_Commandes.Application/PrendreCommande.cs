namespace Commandes.Application;

public sealed class PrendreCommande
{
    private readonly IDepotCommandes m_depotCommandes;

    public PrendreCommande(IDepotCommandes depotCommandes)
    {
        ArgumentNullException.ThrowIfNull(depotCommandes);
        m_depotCommandes = depotCommandes;
    }

    // TODO : orchestrer le cas d'utilisation sans déplacer les règles du domaine ici.
}
