using System.Globalization;
using System.Text.Json;
using Commandes.Application;

namespace S10E01_Facturation.WinForms;

public partial class FormCommande : Form
{
    private readonly ServiceCommandes? m_service;
    private readonly IJournalIncidents? m_journal;

    // Réservé au designer; le point de composition utilise le constructeur injecté.
    public FormCommande()
    {
        InitializeComponent();
    }

    public FormCommande(ServiceCommandes service, IJournalIncidents journal) : this()
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(journal);
        m_service = service;
        m_journal = journal;
        m_listeProduits.DisplayMember = nameof(ProduitDto.Nom);
        m_listeProduits.DataSource = service.ConsulterProduits().ToArray();
        Rafraichir(service.Consulter());
    }

    private ServiceCommandes Service => m_service
        ?? throw new InvalidOperationException("Le formulaire doit recevoir son service au lancement.");

    private void Ajouter_Click(object? sender, EventArgs e)
    {
        if (m_listeProduits.SelectedItem is not ProduitDto produit)
        {
            m_etat.Text = "Sélectionnez un produit.";
            m_listeProduits.Focus();
            return;
        }

        if (!int.TryParse(m_quantite.Text, out int quantite))
        {
            m_etat.Text = "La quantité doit être un entier.";
            m_quantite.Focus();
            m_quantite.SelectAll();
            return;
        }

        Result<CommandeDto> resultat = Service.AjouterProduit(produit.Numero, quantite);
        Presenter(resultat);
    }

    private void Nouvelle_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(m_numero.Text, out int numero))
        {
            m_etat.Text = "Le numéro doit être un entier positif.";
            m_numero.Focus();
            return;
        }

        CommandeDto courante = Service.Consulter();
        if (!courante.EstEnregistree && courante.Lignes.Count > 0
            && MessageBox.Show(this, "Abandonner les lignes non enregistrées?", "Nouvelle commande",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        {
            return;
        }

        Presenter(Service.Creer(numero));
    }

    private void Enregistrer_Click(object? sender, EventArgs e)
    {
        m_enregistrer.Enabled = false;
        try
        {
            Result<CommandeDto> resultat = Service.Enregistrer();
            Presenter(resultat);
            if (resultat.EstSucces)
            {
                m_etat.Text = "Commande enregistrée.";
            }
        }
        catch (Exception exception) when (exception is IOException or InvalidDataException
            or UnauthorizedAccessException or JsonException)
        {
            m_journal?.Consigner("enregistrer commande", exception);
            m_etat.Text = "Enregistrement impossible pour le moment. Vos lignes sont conservées.";
        }
        finally
        {
            CommandeDto courante = Service.Consulter();
            m_enregistrer.Enabled = courante.Lignes.Count > 0 && !courante.EstEnregistree;
        }
    }

    private void Presenter(Result<CommandeDto> resultat)
    {
        if (!resultat.EstSucces)
        {
            m_etat.Text = resultat.MessageErreur;
            return;
        }

        Rafraichir(resultat.Valeur);
    }

    private void Rafraichir(CommandeDto commande)
    {
        m_titreCourant.Text = $"Commande courante : {commande.Numero}";
        m_grille.DataSource = null;
        m_grille.DataSource = commande.Lignes.ToArray();
        m_total.Text = $"Total : {commande.Total.ToString("C", CultureInfo.GetCultureInfo("fr-CA"))}";
        m_etat.Text = commande.EstEnregistree ? "Commande enregistrée." : "Commande en préparation.";
        m_ajouter.Enabled = !commande.EstEnregistree;
        m_enregistrer.Enabled = commande.Lignes.Count > 0 && !commande.EstEnregistree;
    }

    private void FormCommande_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (m_service is null)
        {
            return;
        }

        CommandeDto courante = m_service.Consulter();
        if (!courante.EstEnregistree && courante.Lignes.Count > 0)
        {
            e.Cancel = MessageBox.Show(this, "Quitter sans enregistrer les lignes?", "Commande non enregistrée",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }
    }
}
