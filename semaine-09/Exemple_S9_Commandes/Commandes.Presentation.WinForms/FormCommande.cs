using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using Commandes.Application;

namespace Commandes.Presentation.WinForms;

public partial class FormCommande : Form
{
    private readonly ServiceCommandes? m_service;
    private readonly IJournalIncidents? m_journal;
    private int m_prochainNumero = 1042;

    [DefaultValue(ModeFormCommande.Standard)]
    public ModeFormCommande Mode { get; set; } = ModeFormCommande.Standard;
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CommandeDto? Result { get; private set; }

    // Le constructeur sans paramètre permet au designer de créer le formulaire.
    public FormCommande()
    {
        InitializeComponent();
        // Les liaisons sont préparées dans Load, après l'injection du service.
    }

    public FormCommande(ServiceCommandes service, IJournalIncidents journal) : this()
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(journal);
        m_service = service;
        m_journal = journal;
        // L'objet est construit; son événement Load n'a pas encore été déclenché.
    }

    private void FormCommande_Load(object? sender, EventArgs e)
    {
        if (m_service == null)
        {
            return;
        }

        // Le format monétaire est configuré à l'exécution, hors du code du designer.
        colPrixUnitaire.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-CA");
        colMontant.DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-CA");
        bNouvelle.Visible = Mode == ModeFormCommande.Standard;
        lbProduits.DisplayMember = nameof(ProduitDto.Nom);
        lbProduits.DataSource = m_service.ConsulterProduits().ToArray();
        Rafraichir(m_service.Consulter());
    }

    private void FormCommande_Shown(object? sender, EventArgs e)
    {
        if (Mode == ModeFormCommande.CommandeUnique)
        {
            lbProduits.Focus();
        }
        else
        {
            bNouvelle.Focus();
        }
    }

    private ServiceCommandes Service
    {
        get
        {
            if (m_service == null)
            {
                throw new InvalidOperationException("Le lancement doit injecter le service.");
            }

            return m_service;
        }
    }

    #region Créer la commande avant d'ajouter une ligne
    private void bNouvelle_Click(object? sender, EventArgs e)
    {
        if (Mode == ModeFormCommande.CommandeUnique)
        {
            return;
        }

        // new crée l'objet. ShowDialog déclenche son affichage et attend sa fermeture.
        using FormNouvelleCommande fenetre = new FormNouvelleCommande(m_prochainNumero);
        if (fenetre.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        // Propriété de l'objet fenêtre, distincte de DialogResult et de Result<T>.
        SaisieNouvelleCommande? saisie = fenetre.Result;
        if (saisie == null)
        {
            throw new InvalidOperationException("La saisie validée est absente.");
        }

        int numero = saisie.Numero;
        CommandeDto? courante = Service.Consulter();
        bool abandonnerBrouillon = false;
        if (courante != null && !courante.EstEnregistree && courante.Lignes.Count > 0)
        {
            abandonnerBrouillon = MessageBox.Show(this,
                "Abandonner les lignes non enregistrées et créer une nouvelle commande?",
                "Nouvelle commande", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
            if (!abandonnerBrouillon)
            {
                return;
            }
        }

        try
        {
            Result<CommandeDto> resultat = Service.Creer(numero, abandonnerBrouillon);
            Presenter(resultat, "Commande créée. Choisissez un produit et une quantité.");
            if (resultat.EstSucces)
            {
                // Ce numéro est seulement proposé pour la prochaine création.
                m_prochainNumero = numero;
                if (numero < int.MaxValue)
                {
                    m_prochainNumero = numero + 1;
                }
                lbProduits.Focus();
            }
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident("créer commande", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident("créer commande", exception);
        }
        catch (JsonException exception)
        {
            SignalerIncident("créer commande", exception);
        }
        catch (IOException exception)
        {
            SignalerIncident("créer commande", exception);
        }
    }
    #endregion

    #region Ajouter une ligne à la commande existante
    private void bAjouter_Click(object? sender, EventArgs e)
    {
        ProduitDto? produit = lbProduits.SelectedItem as ProduitDto;
        if (produit == null)
        {
            lEtat.Text = "Sélectionnez un produit.";
            lbProduits.Focus();
            return;
        }

        int quantite = decimal.ToInt32(nudQuantite.Value);
        Result<CommandeDto> resultat = Service.AjouterProduit(produit.Numero, quantite);
        Presenter(resultat, "Produit ajouté à la commande.");
    }
    #endregion

    #region Enregistrer et permettre une nouvelle tentative après un incident
    private void bEnregistrer_Click(object? sender, EventArgs e)
    {
        bEnregistrer.Enabled = false;
        try
        {
            Result<CommandeDto> resultat = Service.Enregistrer();
            Presenter(resultat, "Commande enregistrée. Vous pouvez en créer une nouvelle.");
            if (resultat.EstSucces && Mode == ModeFormCommande.CommandeUnique)
            {
                Result = resultat.Valeur;
                DialogResult = DialogResult.OK;
            }
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident("enregistrer commande", exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident("enregistrer commande", exception);
        }
        catch (JsonException exception)
        {
            SignalerIncident("enregistrer commande", exception);
        }
        catch (IOException exception)
        {
            SignalerIncident("enregistrer commande", exception);
        }
        finally
        {
            Rafraichir(Service.Consulter());
        }
    }
    #endregion

    private void Presenter(Result<CommandeDto> resultat, string messageSucces)
    {
        if (!resultat.EstSucces)
        {
            lEtat.Text = resultat.MessageErreur;
            return;
        }

        Rafraichir(resultat.Valeur);
        lEtat.Text = messageSucces;
    }

    #region Afficher le DTO et l'état des contrôles
    private void Rafraichir(CommandeDto? commande)
    {
        bool modifiable = false;
        decimal total = 0;
        dgvLignes.DataSource = null;
        bEnregistrer.Enabled = false;

        if (commande == null)
        {
            lCommande.Text = "Aucune commande courante";
            lStatut.Text = "À créer";
        }
        else
        {
            lCommande.Text = $"Commande courante : {commande.Numero}";
            if (commande.EstEnregistree)
            {
                lStatut.Text = "Enregistrée";
            }
            else
            {
                lStatut.Text = "En préparation";
                modifiable = true;
            }

            dgvLignes.DataSource = commande.Lignes.ToArray();
            total = commande.Total;
            bEnregistrer.Enabled = modifiable && commande.Lignes.Count > 0;
        }

        lTotal.Text = "Total : " + total.ToString("C", CultureInfo.GetCultureInfo("fr-CA"));
        lbProduits.Enabled = modifiable;
        nudQuantite.Enabled = modifiable;
        bAjouter.Enabled = modifiable;
        if (modifiable)
        {
            AcceptButton = bAjouter;
        }
        else if (Mode == ModeFormCommande.Standard)
        {
            AcceptButton = bNouvelle;
        }
        else
        {
            AcceptButton = null;
        }
    }
    #endregion

    private void SignalerIncident(string operation, Exception exception)
    {
        m_journal?.Consigner(operation, exception);
        lEtat.Text = "Opération impossible pour le moment. La commande courante est conservée.";
    }

    private void FormCommande_FormClosing(object? sender, FormClosingEventArgs e)
    {
        CommandeDto? courante = m_service?.Consulter();
        if (courante != null && !courante.EstEnregistree && courante.Lignes.Count > 0)
        {
            e.Cancel = MessageBox.Show(this, "Quitter sans enregistrer les lignes?",
                "Commande non enregistrée", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes;
        }
    }
}
