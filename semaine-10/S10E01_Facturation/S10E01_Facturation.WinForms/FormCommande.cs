using System.Globalization;
using Commandes.Application;
using Commandes.Presentation;

namespace S10E01_Facturation.WinForms;

public partial class FormCommande : Form, IVueCommande
{
    private ControleurCommande? m_controleur;
    private EtatCommandeDto? m_etatCourant;
    private IReadOnlyCollection<ProduitDto>? m_produits;
    public event EventHandler? DemandeFacture;

    public FormCommande()
    {
        InitializeComponent();
    }

    public void Relier(ControleurCommande controleur, IReadOnlyCollection<ProduitDto> produits)
    {
        ArgumentNullException.ThrowIfNull(controleur);
        ArgumentNullException.ThrowIfNull(produits);
        if (m_controleur is not null)
        {
            throw new InvalidOperationException("Le formulaire est déjà relié.");
        }

        m_controleur = controleur;
        m_produits = produits;
    }

    private void FormCommande_Load(object? sender, EventArgs e)
    {
        if (m_produits is null || m_controleur is null || m_etatCourant is null)
        {
            return; // Le formulaire non relié reste utilisable par le designer.
        }

        colMontant.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-CA");
        lbProduits.DisplayMember = nameof(ProduitDto.Nom);
        lbProduits.DataSource = m_produits.ToArray();
        OnNext(m_etatCourant);
    }

    private ControleurCommande Controleur
    {
        get
        {
            if (m_controleur is null)
            {
                throw new InvalidOperationException("Le formulaire doit recevoir son contrôleur au lancement.");
            }

            return m_controleur;
        }
    }

    private void Ajouter_Click(object? sender, EventArgs e)
    {
        if (lbProduits.SelectedItem is not ProduitDto produit)
        {
            lEtat.Text = "Sélectionnez un produit.";
            lbProduits.Focus();
            return;
        }

        int quantite = decimal.ToInt32(nudQuantite.Value);

        Controleur.Ajouter(produit.Numero, quantite);
    }

    private void Nouvelle_Click(object? sender, EventArgs e)
    {
        if (!int.TryParse(tbNumero.Text, out int numero))
        {
            lEtat.Text = "Le numéro doit être un entier positif.";
            tbNumero.Focus();
            return;
        }

        if (m_etatCourant is not null && PossedeBrouillon(m_etatCourant)
            && MessageBox.Show(this, "Abandonner les lignes non enregistrées?", "Nouvelle commande",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        {
            return;
        }

        Controleur.Nouvelle(numero);
    }

    private void Enregistrer_Click(object? sender, EventArgs e)
    {
        bEnregistrer.Enabled = false;
        try
        {
            Controleur.Enregistrer();
        }
        finally
        {
            bEnregistrer.Enabled = m_etatCourant is not null && PossedeBrouillon(m_etatCourant);
        }
    }

    public void OnNext(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        m_etatCourant = etat;
        if (!IsHandleCreated)
        {
            return;
        }

        CommandeDto commande = etat.Commande;
        lCommande.Text = $"Commande courante : {commande.Numero}";
        dgvLignes.DataSource = null;
        dgvLignes.DataSource = commande.Lignes.ToArray();
        lTotal.Text = $"Sous-total : {commande.Total.ToString("C", CultureInfo.GetCultureInfo("fr-CA"))}";
        if (commande.EstEnregistree)
        {
            lEtat.Text = "Commande enregistrée.";
        }
        else
        {
            lEtat.Text = "Commande en préparation.";
        }
        bAjouter.Enabled = !commande.EstEnregistree;
        bEnregistrer.Enabled = PossedeBrouillon(etat);
    }

    public void PresenterRefus(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        lEtat.Text = message;
    }

    public void PresenterIncident(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        lEtat.Text = message;
    }

    private static bool PossedeBrouillon(EtatCommandeDto etat)
    {
        return !etat.Commande.EstEnregistree && etat.Commande.Lignes.Count > 0;
    }

    private void Facture_Click(object? sender, EventArgs e)
    {
        DemandeFacture?.Invoke(this, EventArgs.Empty);
    }

    private void FormCommande_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (m_etatCourant is not null && PossedeBrouillon(m_etatCourant))
        {
            e.Cancel = MessageBox.Show(this, "Quitter sans enregistrer les lignes?", "Commande non enregistrée",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes;
        }
    }

    // Fournis : aucun détail technique n'est présenté à l'utilisateur.
    public void OnError(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);
        PresenterIncident("Les notifications de la commande sont interrompues.");
    }

    public void OnCompleted()
    {
        lEtat.Text = "Notifications terminées.";
    }

}
