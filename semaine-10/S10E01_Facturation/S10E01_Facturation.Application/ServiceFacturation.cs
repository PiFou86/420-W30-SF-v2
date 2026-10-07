using Commandes.Domaine;

namespace Commandes.Application;

public sealed class ServiceFacturation
{
    private readonly ServiceCommandes m_commandes;

    public ServiceFacturation(ServiceCommandes commandes)
    {
        ArgumentNullException.ThrowIfNull(commandes);
        m_commandes = commandes;
    }

    public FactureDto Consulter()
    {
        CommandeDto commande = m_commandes.Consulter();
        Facture facture = new(commande.Total);
        return new FactureDto(commande.Numero, facture.SousTotal,
            facture.Frais, facture.Total, !commande.EstEnregistree);
    }
}
