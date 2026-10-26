#nullable disable
namespace Commandes.Portee.WinForms;

partial class FormAccueil
{
    private System.ComponentModel.IContainer components = null;


    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        colNumero = new DataGridViewTextBoxColumn();
        colNombreLignes = new DataGridViewTextBoxColumn();
        DataGridViewCellStyle styleTotal = new DataGridViewCellStyle();
        colTotal = new DataGridViewTextBoxColumn();
        lTitre = new Label();
        bNouvelle = new Button();
        bActualiser = new Button();
        dgvCommandes = new DataGridView();
        lEtat = new Label();
        ((System.ComponentModel.ISupportInitialize)dgvCommandes).BeginInit();
        SuspendLayout();
        // 
        // lTitre
        // 
        lTitre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lTitre.Location = new Point(12, 9);
        lTitre.Name = "lTitre";
        lTitre.Size = new Size(920, 30);
        lTitre.TabIndex = 0;
        lTitre.Text = "Commandes enregistrées";
        // 
        // bNouvelle
        // 
        bNouvelle.Location = new Point(12, 42);
        bNouvelle.Name = "bNouvelle";
        bNouvelle.Size = new Size(220, 36);
        bNouvelle.TabIndex = 1;
        bNouvelle.Text = "&Nouvelle commande";
        bNouvelle.Click += bNouvelle_Click;
        // 
        // bActualiser
        // 
        bActualiser.Location = new Point(238, 42);
        bActualiser.Name = "bActualiser";
        bActualiser.Size = new Size(140, 36);
        bActualiser.TabIndex = 2;
        bActualiser.Text = "&Actualiser";
        bActualiser.Click += bActualiser_Click;
        // 
        // dgvCommandes
        // 
        dgvCommandes.AccessibleName = "Liste des commandes enregistrées";
        dgvCommandes.AllowUserToAddRows = false;
        dgvCommandes.AllowUserToDeleteRows = false;
        dgvCommandes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCommandes.Location = new Point(12, 84);
        dgvCommandes.Name = "dgvCommandes";
        dgvCommandes.ReadOnly = true;
        dgvCommandes.AutoGenerateColumns = false;
        colNumero.Name = "colNumero";
        colNumero.HeaderText = "Numéro";
        colNumero.DataPropertyName = "Numero";
        colNumero.ReadOnly = true;
        colNombreLignes.Name = "colNombreLignes";
        colNombreLignes.HeaderText = "Nombre de lignes";
        colNombreLignes.DataPropertyName = "NombreLignes";
        colNombreLignes.ReadOnly = true;
        styleTotal.Format = "C2";
        colTotal.Name = "colTotal";
        colTotal.HeaderText = "Total";
        colTotal.DataPropertyName = "Total";
        colTotal.ReadOnly = true;
        colTotal.DefaultCellStyle = styleTotal;
        dgvCommandes.Columns.AddRange(new DataGridViewColumn[] { colNumero, colNombreLignes, colTotal });
        dgvCommandes.RowHeadersVisible = false;
        dgvCommandes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCommandes.Size = new Size(936, 502);
        dgvCommandes.TabIndex = 3;
        // 
        // lEtat
        // 
        lEtat.AccessibleName = "État de la liste";
        lEtat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lEtat.Location = new Point(12, 589);
        lEtat.Name = "lEtat";
        lEtat.Size = new Size(920, 22);
        lEtat.TabIndex = 4;
        lEtat.Text = "Aucune commande chargée.";
        // 
        // FormAccueil
        // 
        AcceptButton = bNouvelle;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 620);
        Controls.Add(lTitre);
        Controls.Add(bNouvelle);
        Controls.Add(bActualiser);
        Controls.Add(dgvCommandes);
        Controls.Add(lEtat);
        Font = new Font("Segoe UI", 11F);
        MinimumSize = new Size(800, 500);
        Name = "FormAccueil";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Commandes — une portée par saisie";
        Load += FormAccueil_Load;
        ((System.ComponentModel.ISupportInitialize)dgvCommandes).EndInit();
        ResumeLayout(false);
    }
    private Label lTitre;
    private Button bNouvelle;
    private Button bActualiser;
    private DataGridView dgvCommandes;
    private Label lEtat;
    private DataGridViewTextBoxColumn colTotal;
    private DataGridViewTextBoxColumn colNumero;
    private DataGridViewTextBoxColumn colNombreLignes;

}
