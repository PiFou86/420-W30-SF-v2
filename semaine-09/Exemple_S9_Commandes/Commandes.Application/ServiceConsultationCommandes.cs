namespace Commandes.Application;

public sealed class ServiceConsultationCommandes
{
    private readonly IDepotCommandes m_depot;

    public ServiceConsultationCommandes(IDepotCommandes depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    public IReadOnlyCollection<CommandeDto> ConsulterEnregistrees()
    {
        return m_depot.ObtenirToutes()
            .Where(commande => commande.EstConfirmee)
            .OrderBy(commande => commande.Numero)
            .Select(commande => new CommandeDto(commande)).ToArray();
    }
}
