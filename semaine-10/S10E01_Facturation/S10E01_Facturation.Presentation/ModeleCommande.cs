using Commandes.Application;

namespace Commandes.Presentation;

// Adaptateur du modèle manipulé par la présentation; aucune règle métier ici.
// Usage synchrone, sur un seul fil d'exécution.
public sealed class ModeleCommande : IObservable<EtatCommandeDto>, IDisposable
{
    private readonly ServiceCommandes m_commandes;
    private readonly ServiceFacturation m_facturation;
    private readonly IJournalIncidents m_journal;
    private readonly List<IObserver<EtatCommandeDto>> m_observateurs = new List<IObserver<EtatCommandeDto>>();
    private bool m_notificationEnCours;
    private bool m_estTermine;

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

    // Pas de notification initiale. Le propriétaire initialise la vue séparément.
    public IDisposable Subscribe(IObserver<EtatCommandeDto> observateur)
    {
        ArgumentNullException.ThrowIfNull(observateur);
        ObjectDisposedException.ThrowIf(m_estTermine, this);
        if (m_observateurs.Any(item => ReferenceEquals(item, observateur)))
        {
            throw new InvalidOperationException("Cet observateur est déjà attaché.");
        }

        // S10-S : inscrire cet observateur dans la liste.
        // À compléter : l’objet retourné ci-dessous sait déjà désabonner.
        return new AbonnementCommande(m_observateurs, observateur);
    }

    public Result<CommandeDto> Nouvelle(int numero)
    {
        VerifierAbsenceReentree();
        return PublierSucces(m_commandes.Creer(numero));
    }

    public Result<CommandeDto> Ajouter(int numeroProduit, int quantite)
    {
        VerifierAbsenceReentree();
        return PublierSucces(m_commandes.AjouterProduit(numeroProduit, quantite));
    }

    public Result<CommandeDto> Enregistrer()
    {
        VerifierAbsenceReentree();
        return PublierSucces(m_commandes.Enregistrer());
    }

    private void VerifierAbsenceReentree()
    {
        ObjectDisposedException.ThrowIf(m_estTermine, this);
        if (m_notificationEnCours)
        {
            throw new InvalidOperationException("Une actualisation ne doit pas déclencher une commande.");
        }
    }

    private Result<CommandeDto> PublierSucces(Result<CommandeDto> resultat)
    {
        if (!resultat.EstSucces)
        {
            return resultat;
        }

        EtatCommandeDto etat = Consulter();
        IObserver<EtatCommandeDto>[] observateurs = m_observateurs.ToArray();
        m_notificationEnCours = true;
        try
        {
            foreach (IObserver<EtatCommandeDto> observateur in observateurs)
            {
                try
                {
                    // S10-N : transmettre cet instantané à cet observateur.
                    // À compléter : appeler le contrat IObserver<EtatCommandeDto>.
                }
                catch (Exception exception)
                {
                    // Frontière d'affichage : une vue défaillante n'annule pas
                    // une opération déjà réussie et ne bloque pas les autres vues.
                    m_journal.Consigner("actualiser vue", exception);
                }
            }
        }
        finally
        {
            m_notificationEnCours = false;
        }

        return resultat;
    }

    // Fourni : fin normale de la source à la fin de sa durée de vie.
    public void Dispose()
    {
        Terminer(null);
    }

    // Fourni pour la démonstration. Un incident récupérable du dépôt ne termine
    // pas le flux : le contrôleur conserve alors son traitement habituel.
    public void SignalerErreurFatale(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);
        Terminer(erreur);
    }

    private void Terminer(Exception? erreur)
    {
        if (m_estTermine)
        {
            return;
        }

        VerifierAbsenceReentree();
        m_estTermine = true;
        IObserver<EtatCommandeDto>[] observateurs = m_observateurs.ToArray();
        m_observateurs.Clear();
        foreach (IObserver<EtatCommandeDto> observateur in observateurs)
        {
            try
            {
                if (erreur is null)
                {
                    observateur.OnCompleted();
                }
                else
                {
                    observateur.OnError(erreur);
                }
            }
            catch (Exception incident)
            {
                m_journal.Consigner("terminer notifications", incident);
            }
        }
    }

}
