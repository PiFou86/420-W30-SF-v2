using System.Text.Json;
using Contacts.Application;

namespace Contacts.WinForms;

public partial class FormContacts : Form
{
    private readonly CreerContact? m_creation;
    private readonly ConsulterContacts? m_consultation;
    private readonly IJournalIncidents? m_journal;

    public FormContacts()
    {
        InitializeComponent();
    }

    public FormContacts(CreerContact creation, ConsulterContacts consultation, IJournalIncidents journal) : this()
    {
        ArgumentNullException.ThrowIfNull(creation);
        ArgumentNullException.ThrowIfNull(consultation);
        ArgumentNullException.ThrowIfNull(journal);
        m_creation = creation;
        m_consultation = consultation;
        m_journal = journal;
    }

    private CreerContact Creation
    {
        get
        {
            if (m_creation == null)
            {
                throw new InvalidOperationException("Le lancement doit injecter le cas d'utilisation.");
            }

            return m_creation;
        }
    }

    private ConsulterContacts Consultation
    {
        get
        {
            if (m_consultation == null)
            {
                throw new InvalidOperationException("Le lancement doit injecter la consultation.");
            }

            return m_consultation;
        }
    }

    private void FormContacts_Load(object? sender, EventArgs e)
    {
        // E2-L : charger tous les contacts au début de l'exercice 2. Rien à ajouter en E1.
    }

    private void bCreer_Click(object? sender, EventArgs e)
    {
        try
        {
            // E1-C : construire DemandeCreationContact, appeler Creation.Executer et traiter le résultat.
            // E2-C : après succès, effacer la saisie, vider la recherche et actualiser la liste.
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident("créer contact", exception);
        }
        catch (JsonException exception)
        {
            SignalerIncident("créer contact", exception);
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident("créer contact", exception);
        }
        catch (IOException exception)
        {
            SignalerIncident("créer contact", exception);
        }
    }

    private void bEffacer_Click(object? sender, EventArgs e)
    {
        EffacerSaisie();
        AfficherMessage(string.Empty);
    }

    private void EffacerSaisie()
    {
        // E1-E : effacer les deux champs et redonner le focus au nom.
    }

    private void AfficherMessage(string message)
    {
        // E1-M : afficher message dans lEtat.
    }

    private void bRechercher_Click(object? sender, EventArgs e)
    {
        try
        {
            // E2-S : appeler Consultation.Rechercher avec le mot-clef puis AfficherContacts.
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident("rechercher contacts", exception);
        }
        catch (JsonException exception)
        {
            SignalerIncident("rechercher contacts", exception);
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident("rechercher contacts", exception);
        }
        catch (IOException exception)
        {
            SignalerIncident("rechercher contacts", exception);
        }
    }

    private void bTous_Click(object? sender, EventArgs e)
    {
        try
        {
            // E2-T : vider tbMotClef puis afficher Consultation.ObtenirTous().
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident("lister contacts", exception);
        }
        catch (JsonException exception)
        {
            SignalerIncident("lister contacts", exception);
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident("lister contacts", exception);
        }
        catch (IOException exception)
        {
            SignalerIncident("lister contacts", exception);
        }
    }

    private void AfficherContacts(IReadOnlyCollection<ContactDto> contacts)
    {
        // E2-A : alimenter dgvContacts depuis les DTO, afficher le nombre de résultats dans lNombre.
    }

    private void SignalerIncident(string operation, Exception exception)
    {
        m_journal?.Consigner(operation, exception);
        AfficherMessage("Opération impossible pour le moment. Réessayez après correction de l'incident.");
    }
}
