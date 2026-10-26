using System.Text.Json;
using System.Globalization;
using Commandes.Application;

namespace Commandes.Portee.WinForms;

public partial class FormAccueil : Form
{
    private readonly ServiceConsultationCommandes? m_consultation;
    private readonly IFabriqueSaisieCommande? m_fabrique;
    private readonly IJournalIncidents? m_journal;
    private int m_numeroPropose = 1042;

    public FormAccueil()
    {
        InitializeComponent();
    }

    public FormAccueil(ServiceConsultationCommandes consultation,
        IFabriqueSaisieCommande fabrique, IJournalIncidents journal) : this()
    {
        ArgumentNullException.ThrowIfNull(consultation);
        ArgumentNullException.ThrowIfNull(fabrique);
        ArgumentNullException.ThrowIfNull(journal);
        m_consultation = consultation;
        m_fabrique = fabrique;
        m_journal = journal;
    }

    private void FormAccueil_Load(object? sender, EventArgs e)
    {
        colTotal.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-CA");
        if (m_consultation != null)
        {
            Actualiser();
        }
    }

    private void bActualiser_Click(object? sender, EventArgs e)
    {
        Actualiser();
    }

    private void bNouvelle_Click(object? sender, EventArgs e)
    {
        if (m_fabrique == null)
        {
            return;
        }

        bNouvelle.Enabled = false;
        CommandeDto? enregistree = null;
        bool actualisee;
        try
        {
            enregistree = m_fabrique.Ouvrir(this, m_numeroPropose);
        }
        finally
        {
            // La fabrique a déjà libéré la portée, y compris après une annulation.
            actualisee = Actualiser();
            bNouvelle.Enabled = true;
        }

        if (actualisee && enregistree != null)
        {
            lEtat.Text = $"Commande {enregistree.Numero} enregistrée. Portée de saisie libérée.";
        }
    }

    private bool Actualiser()
    {
        if (m_consultation == null)
        {
            return false;
        }

        try
        {
            IReadOnlyCollection<CommandeDto> commandes = m_consultation.ConsulterEnregistrees();
            List<CommandeListeViewModel> lignes = new List<CommandeListeViewModel>();
            foreach (CommandeDto commande in commandes)
            {
                CommandeListeViewModel ligne = new CommandeListeViewModel(
                    commande.Numero, commande.Lignes.Count, commande.Total);
                lignes.Add(ligne);
            }
            // Remplacer l'affichage seulement après une lecture réussie.
            dgvCommandes.DataSource = lignes;
            if (commandes.Count > 0)
            {
                int dernierNumero = commandes.Max(commande => commande.Numero);
                m_numeroPropose = dernierNumero;
                if (dernierNumero < int.MaxValue)
                {
                    m_numeroPropose = dernierNumero + 1;
                }
            }

            lEtat.Text = $"{commandes.Count} commande(s) enregistrée(s).";
            return true;
        }
        catch (InvalidDataException exception)
        {
            m_journal?.Consigner("consulter commandes enregistrées", exception);
            lEtat.Text = "Actualisation impossible. La liste affichée peut être ancienne.";
            return false;
        }
        catch (UnauthorizedAccessException exception)
        {
            m_journal?.Consigner("consulter commandes enregistrées", exception);
            lEtat.Text = "Actualisation impossible. La liste affichée peut être ancienne.";
            return false;
        }
        catch (JsonException exception)
        {
            m_journal?.Consigner("consulter commandes enregistrées", exception);
            lEtat.Text = "Actualisation impossible. La liste affichée peut être ancienne.";
            return false;
        }
        catch (IOException exception)
        {
            m_journal?.Consigner("consulter commandes enregistrées", exception);
            lEtat.Text = "Actualisation impossible. La liste affichée peut être ancienne.";
            return false;
        }
    }
}
