#nullable disable
namespace Commandes.Presentation.WinForms;

partial class FormCommande
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
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        colProduit = new DataGridViewTextBoxColumn();
        colQuantite = new DataGridViewTextBoxColumn();
        colPrixUnitaire = new DataGridViewTextBoxColumn();
        colMontant = new DataGridViewTextBoxColumn();
        bNouvelle = new Button();
        lCommande = new Label();
        lbProduits = new ListBox();
        lQuantite = new Label();
        nudQuantite = new NumericUpDown();
        bAjouter = new Button();
        dgvLignes = new DataGridView();
        lTotal = new Label();
        lStatut = new Label();
        bEnregistrer = new Button();
        lEtat = new Label();
        ((System.ComponentModel.ISupportInitialize)nudQuantite).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLignes).BeginInit();
        SuspendLayout();
        // 
        // colProduit
        // 
        colProduit.DataPropertyName = "Produit";
        colProduit.FillWeight = 200F;
        colProduit.HeaderText = "Produit";
        colProduit.Name = "colProduit";
        colProduit.ReadOnly = true;
        // 
        // colQuantite
        // 
        colQuantite.DataPropertyName = "Quantite";
        colQuantite.HeaderText = "Quantité";
        colQuantite.Name = "colQuantite";
        colQuantite.ReadOnly = true;
        // 
        // colPrixUnitaire
        // 
        colPrixUnitaire.DataPropertyName = "PrixUnitaire";
        dataGridViewCellStyle3.Format = "C2";
        colPrixUnitaire.DefaultCellStyle = dataGridViewCellStyle3;
        colPrixUnitaire.HeaderText = "Prix unitaire";
        colPrixUnitaire.Name = "colPrixUnitaire";
        colPrixUnitaire.ReadOnly = true;
        // 
        // colMontant
        // 
        colMontant.DataPropertyName = "Montant";
        dataGridViewCellStyle4.Format = "C2";
        colMontant.DefaultCellStyle = dataGridViewCellStyle4;
        colMontant.HeaderText = "Sous-total";
        colMontant.Name = "colMontant";
        colMontant.ReadOnly = true;
        // 
        // bNouvelle
        // 
        bNouvelle.Location = new Point(12, 12);
        bNouvelle.Name = "bNouvelle";
        bNouvelle.Size = new Size(240, 36);
        bNouvelle.TabIndex = 0;
        bNouvelle.Text = "&Créer une nouvelle commande";
        bNouvelle.Click += bNouvelle_Click;
        // 
        // lCommande
        // 
        lCommande.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lCommande.Location = new Point(12, 51);
        lCommande.Name = "lCommande";
        lCommande.Size = new Size(800, 28);
        lCommande.TabIndex = 1;
        // 
        // lbProduits
        // 
        lbProduits.AccessibleName = "Produit à ajouter";
        lbProduits.Location = new Point(12, 82);
        lbProduits.Name = "lbProduits";
        lbProduits.Size = new Size(240, 84);
        lbProduits.TabIndex = 2;
        // 
        // lQuantite
        // 
        lQuantite.Location = new Point(258, 88);
        lQuantite.Name = "lQuantite";
        lQuantite.Size = new Size(76, 28);
        lQuantite.TabIndex = 3;
        lQuantite.Text = "Quantité";
        // 
        // nudQuantite
        // 
        nudQuantite.AccessibleName = "Quantité à ajouter";
        nudQuantite.Location = new Point(340, 86);
        nudQuantite.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudQuantite.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudQuantite.Name = "nudQuantite";
        nudQuantite.Size = new Size(90, 27);
        nudQuantite.TabIndex = 4;
        nudQuantite.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // bAjouter
        // 
        bAjouter.Enabled = false;
        bAjouter.Location = new Point(436, 80);
        bAjouter.Name = "bAjouter";
        bAjouter.Size = new Size(120, 36);
        bAjouter.TabIndex = 5;
        bAjouter.Text = "&Ajouter";
        bAjouter.Click += bAjouter_Click;
        // 
        // dgvLignes
        // 
        dgvLignes.AccessibleName = "Lignes de la commande courante";
        dgvLignes.AllowUserToAddRows = false;
        dgvLignes.AllowUserToDeleteRows = false;
        dgvLignes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLignes.ColumnHeadersHeight = 25;
        dgvLignes.Columns.AddRange(new DataGridViewColumn[] { colProduit, colQuantite, colPrixUnitaire, colMontant });
        dgvLignes.Location = new Point(12, 172);
        dgvLignes.Name = "dgvLignes";
        dgvLignes.ReadOnly = true;
        dgvLignes.RowHeadersVisible = false;
        dgvLignes.Size = new Size(1036, 485);
        dgvLignes.TabIndex = 6;
        // 
        // lTotal
        // 
        lTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lTotal.Location = new Point(12, 660);
        lTotal.Name = "lTotal";
        lTotal.Size = new Size(320, 28);
        lTotal.TabIndex = 7;
        // 
        // lStatut
        // 
        lStatut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lStatut.Location = new Point(351, 660);
        lStatut.Name = "lStatut";
        lStatut.Size = new Size(220, 28);
        lStatut.TabIndex = 8;
        // 
        // bEnregistrer
        // 
        bEnregistrer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bEnregistrer.Enabled = false;
        bEnregistrer.Location = new Point(868, 672);
        bEnregistrer.Name = "bEnregistrer";
        bEnregistrer.Size = new Size(180, 36);
        bEnregistrer.TabIndex = 9;
        bEnregistrer.Text = "&Enregistrer";
        bEnregistrer.Click += bEnregistrer_Click;
        // 
        // lEtat
        // 
        lEtat.AccessibleName = "Message de l'opération";
        lEtat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lEtat.Location = new Point(12, 688);
        lEtat.Name = "lEtat";
        lEtat.Size = new Size(840, 23);
        lEtat.TabIndex = 10;
        lEtat.Text = "Créez une commande pour commencer.";
        // 
        // FormCommande
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1060, 720);
        Controls.Add(bNouvelle);
        Controls.Add(lCommande);
        Controls.Add(lbProduits);
        Controls.Add(lQuantite);
        Controls.Add(nudQuantite);
        Controls.Add(bAjouter);
        Controls.Add(dgvLignes);
        Controls.Add(lTotal);
        Controls.Add(lStatut);
        Controls.Add(bEnregistrer);
        Controls.Add(lEtat);
        Font = new Font("Segoe UI", 11F);
        MinimumSize = new Size(1000, 640);
        Name = "FormCommande";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Préparation S9 — prise de commande";
        FormClosing += FormCommande_FormClosing;
        Load += FormCommande_Load;
        Shown += FormCommande_Shown;
        ((System.ComponentModel.ISupportInitialize)nudQuantite).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLignes).EndInit();
        ResumeLayout(false);
    }
    private Button bNouvelle;
    private Label lCommande;
    private ListBox lbProduits;
    private Label lQuantite;
    private NumericUpDown nudQuantite;
    private Button bAjouter;
    private DataGridView dgvLignes;
    private Label lTotal;
    private Label lStatut;
    private Button bEnregistrer;
    private Label lEtat;
    private DataGridViewTextBoxColumn colProduit;
    private DataGridViewTextBoxColumn colQuantite;
    private DataGridViewTextBoxColumn colPrixUnitaire;
    private DataGridViewTextBoxColumn colMontant;

}
