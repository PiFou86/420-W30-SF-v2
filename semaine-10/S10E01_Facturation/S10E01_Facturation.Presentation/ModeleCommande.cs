using Commandes.Application;

namespace Commandes.Presentation;

// Adaptateur du modèle manipulé par la présentation; aucune règle métier ici.
// Usage synchrone, sur un seul fil d'exécution.
public sealed class ModeleCommande
{
    private readonly ServiceCommandes m_commandes;
    private readonly ServiceFacturation m_facturation;
    private readonly IJournalIncidents m_journal;

    public ModeleCommande(ServiceCommandes commandes, ServiceFacturation facturation,
        IJournalIncidents journal)
    {
        ArgumentNullException.ThrowIfNull(commandes);
        ArgumentNullException.ThrowIfNull(facturation);
        ArgumentNullException.ThrowIfNull(journal);
        m_commandes = commandes;
        m_facturation = facturation;
        m_journal = journal;
    }

    public EtatCommandeDto Consulter()
    {
        return new EtatCommandeDto(m_commandes.Consulter(), m_facturation.Consulter());
    }

    public IReadOnlyCollection<ProduitDto> ConsulterProduits()
    {
        return m_commandes.ConsulterProduits();
    }

    // À implanter : contrat détaillé dans l'énoncé.
    public IDisposable Attacher(IObservateurCommande observateur)
    {
        ArgumentNullException.ThrowIfNull(observateur);
        throw new NotImplementedException("Implanter l'abonnement explicite.");
    }

    public Result<CommandeDto> Nouvelle(int numero)
    {
        throw new NotImplementedException("Appeler le service et publier seulement en succès.");
    }

    public Result<CommandeDto> Ajouter(int numeroProduit, int quantite)
    {
        throw new NotImplementedException("Appeler le service et publier seulement en succès.");
    }

    public Result<CommandeDto> Enregistrer()
    {
        throw new NotImplementedException("Distinguer succès, refus et incident.");
    }
}
