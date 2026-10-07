#nullable enable
namespace S10E01_Facturation.WinForms;

partial class FormCommande
{
    private System.ComponentModel.IContainer? m_components;
    private TableLayoutPanel m_principal = null!;
    private FlowLayoutPanel m_entete = null!;
    private FlowLayoutPanel m_saisie = null!;
    private FlowLayoutPanel m_pied = null!;
    private Button m_nouvelle = null!;
    private Label m_labelNumero = null!;
    private Label m_labelQuantite = null!;

    private Label m_titreCourant = null!;
    private TextBox m_numero = null!;
    private TextBox m_quantite = null!;
    private ListBox m_listeProduits = null!;
    private DataGridView m_grille = null!;
    private Button m_ajouter = null!;
    private Button m_enregistrer = null!;
    private Label m_total = null!;
    private Label m_etat = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && m_components is not null)
        {
            m_components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        m_components = new System.ComponentModel.Container();
        m_principal = new TableLayoutPanel();
        m_entete = new FlowLayoutPanel();
        m_saisie = new FlowLayoutPanel();
        m_pied = new FlowLayoutPanel();
        m_nouvelle = new Button();
        m_labelNumero = new Label();
        m_labelQuantite = new Label();
        m_titreCourant = new Label();
        m_numero = new TextBox();
        m_quantite = new TextBox();
        m_listeProduits = new ListBox();
        m_grille = new DataGridView();
        m_ajouter = new Button();
        m_enregistrer = new Button();
        m_total = new Label();
        m_etat = new Label();
        SuspendLayout();
        m_principal.Dock = DockStyle.Fill;
        m_principal.Padding = new Padding(16);
        m_principal.ColumnCount = 1;
        m_principal.RowCount = 6;
        m_principal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        m_principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
        m_principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 110));
        m_principal.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        m_principal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        m_principal.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
        m_entete.Dock = DockStyle.Fill;
        m_entete.TabIndex = 0;
        m_entete.AutoSize = true;
        m_labelNumero.Text = "Numéro de la nouvelle commande";
        m_labelNumero.AutoSize = true;
        m_numero.Name = "txtNumero";
        m_numero.AccessibleName = "Numéro proposé pour une nouvelle commande";
        m_numero.Text = "1";
        m_numero.Width = 80;
        m_numero.TabIndex = 0;
        m_nouvelle.Name = "btnNouvelle";
        m_nouvelle.Text = "&Nouvelle";
        m_nouvelle.AutoSize = true;
        m_nouvelle.TabIndex = 1;
        m_nouvelle.Click += Nouvelle_Click;
        m_entete.Controls.AddRange(new Control[] { m_labelNumero, m_numero, m_nouvelle });
        m_titreCourant.AutoSize = true;
        m_titreCourant.TabIndex = 1;
        m_titreCourant.Dock = DockStyle.Fill;
        m_saisie.Dock = DockStyle.Fill;
        m_saisie.TabIndex = 2;
        m_saisie.AutoScroll = true;
        m_listeProduits.Name = "lstProduits";
        m_listeProduits.AccessibleName = "Produit à ajouter";
        m_listeProduits.Size = new Size(220, 90);
        m_listeProduits.TabIndex = 0;
        m_labelQuantite.Text = "Quantité";
        m_labelQuantite.AutoSize = true;
        m_quantite.Name = "txtQuantite";
        m_quantite.AccessibleName = "Quantité à ajouter";
        m_quantite.Text = "1";
        m_quantite.Width = 70;
        m_quantite.TabIndex = 1;
        m_ajouter.Name = "btnAjouter";
        m_ajouter.Text = "&Ajouter";
        m_ajouter.AutoSize = true;
        m_ajouter.TabIndex = 2;
        m_ajouter.Click += Ajouter_Click;
        m_saisie.Controls.AddRange(new Control[] { m_listeProduits, m_labelQuantite, m_quantite, m_ajouter });
        m_grille.Name = "grilleLignes";
        m_grille.AccessibleName = "Lignes de la commande courante";
        m_grille.Dock = DockStyle.Fill;
        m_grille.ReadOnly = true;
        m_grille.AllowUserToAddRows = false;
        m_grille.AllowUserToDeleteRows = false;
        m_grille.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        m_grille.TabIndex = 3;
        m_pied.Dock = DockStyle.Fill;
        m_pied.TabIndex = 4;
        m_pied.AutoSize = true;
        m_total.AutoSize = true;
        m_total.Margin = new Padding(3, 9, 24, 3);
        m_enregistrer.Name = "btnEnregistrer";
        m_enregistrer.Text = "&Enregistrer";
        m_enregistrer.AutoSize = true;
        m_enregistrer.TabIndex = 0;
        m_enregistrer.Click += Enregistrer_Click;
        m_pied.Controls.AddRange(new Control[] { m_total, m_enregistrer });
        m_etat.Name = "lblEtat";
        m_etat.TabIndex = 5;
        m_etat.Dock = DockStyle.Fill;
        m_etat.AccessibleName = "État de la commande";
        m_principal.Controls.Add(m_entete, 0, 0);
        m_principal.Controls.Add(m_titreCourant, 0, 1);
        m_principal.Controls.Add(m_saisie, 0, 2);
        m_principal.Controls.Add(m_grille, 0, 3);
        m_principal.Controls.Add(m_pied, 0, 4);
        m_principal.Controls.Add(m_etat, 0, 5);
        Controls.Add(m_principal);
        Name = "FormCommande";
        Text = "Prise de commande";
        ClientSize = new Size(900, 650);
        MinimumSize = new Size(800, 550);
        AutoScaleMode = AutoScaleMode.Font;
        StartPosition = FormStartPosition.CenterScreen;
        AcceptButton = m_ajouter;
        FormClosing += FormCommande_FormClosing;
        ResumeLayout(false);
    }
}
