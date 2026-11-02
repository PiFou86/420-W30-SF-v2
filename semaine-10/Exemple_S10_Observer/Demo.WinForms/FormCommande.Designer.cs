#nullable disable
namespace Demo.WinForms;

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
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        colProduit = new DataGridViewTextBoxColumn();
        colQuantite = new DataGridViewTextBoxColumn();
        colMontant = new DataGridViewTextBoxColumn();
        lNumero = new Label();
        tbNumero = new TextBox();
        bNouvelle = new Button();
        lCommande = new Label();
        lbProduits = new ListBox();
        lQuantite = new Label();
        nudQuantite = new NumericUpDown();
        bAjouter = new Button();
        dgvLignes = new DataGridView();
        lTotal = new Label();
        bEnregistrer = new Button();
        lEtat = new Label();
        bFacture = new Button();
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
        // colMontant
        // 
        colMontant.DataPropertyName = "Montant";
        dataGridViewCellStyle2.Format = "C2";
        colMontant.DefaultCellStyle = dataGridViewCellStyle2;
        colMontant.HeaderText = "Montant";
        colMontant.Name = "colMontant";
        colMontant.ReadOnly = true;
        // 
        // lNumero
        // 
        lNumero.Location = new Point(12, 9);
        lNumero.Name = "lNumero";
        lNumero.Size = new Size(191, 24);
        lNumero.TabIndex = 0;
        lNumero.Text = "Numéro de la nouvelle commande";
        // 
        // tbNumero
        // 
        tbNumero.AccessibleName = "Numéro de la nouvelle commande";
        tbNumero.Location = new Point(209, 6);
        tbNumero.Name = "tbNumero";
        tbNumero.Size = new Size(90, 23);
        tbNumero.TabIndex = 0;
        tbNumero.Text = "1";
        // 
        // bNouvelle
        // 
        bNouvelle.Location = new Point(305, 4);
        bNouvelle.Name = "bNouvelle";
        bNouvelle.Size = new Size(97, 27);
        bNouvelle.TabIndex = 1;
        bNouvelle.Text = "&Nouvelle";
        bNouvelle.UseVisualStyleBackColor = true;
        bNouvelle.Click += Nouvelle_Click;
        // 
        // lCommande
        // 
        lCommande.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lCommande.Location = new Point(12, 34);
        lCommande.Name = "lCommande";
        lCommande.Size = new Size(384, 26);
        lCommande.TabIndex = 2;
        lCommande.Text = "Commande en préparation";
        // 
        // lbProduits
        // 
        lbProduits.AccessibleName = "Produit à ajouter";
        lbProduits.Location = new Point(12, 65);
        lbProduits.Name = "lbProduits";
        lbProduits.Size = new Size(280, 94);
        lbProduits.TabIndex = 2;
        // 
        // lQuantite
        // 
        lQuantite.Location = new Point(298, 65);
        lQuantite.Name = "lQuantite";
        lQuantite.Size = new Size(54, 24);
        lQuantite.TabIndex = 3;
        lQuantite.Text = "Quantité";
        // 
        // nudQuantite
        // 
        nudQuantite.AccessibleName = "Quantité à ajouter";
        nudQuantite.Location = new Point(358, 63);
        nudQuantite.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        nudQuantite.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudQuantite.Name = "nudQuantite";
        nudQuantite.Size = new Size(90, 23);
        nudQuantite.TabIndex = 3;
        nudQuantite.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // bAjouter
        // 
        bAjouter.Enabled = false;
        bAjouter.Location = new Point(454, 59);
        bAjouter.Name = "bAjouter";
        bAjouter.Size = new Size(97, 27);
        bAjouter.TabIndex = 4;
        bAjouter.Text = "&Ajouter";
        bAjouter.UseVisualStyleBackColor = true;
        bAjouter.Click += Ajouter_Click;
        // 
        // dgvLignes
        // 
        dgvLignes.AccessibleName = "Lignes de la commande courante";
        dgvLignes.AllowUserToAddRows = false;
        dgvLignes.AllowUserToDeleteRows = false;
        dgvLignes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLignes.Columns.AddRange(new DataGridViewColumn[] { colProduit, colQuantite, colMontant });
        dgvLignes.Location = new Point(12, 165);
        dgvLignes.Name = "dgvLignes";
        dgvLignes.ReadOnly = true;
        dgvLignes.RowHeadersVisible = false;
        dgvLignes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLignes.Size = new Size(876, 415);
        dgvLignes.TabIndex = 5;
        // 
        // lTotal
        // 
        lTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lTotal.Location = new Point(12, 596);
        lTotal.Name = "lTotal";
        lTotal.Size = new Size(560, 18);
        lTotal.TabIndex = 6;
        lTotal.Text = "Sous-total : 0,00 $";
        // 
        // bEnregistrer
        // 
        bEnregistrer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bEnregistrer.Enabled = false;
        bEnregistrer.Location = new Point(758, 586);
        bEnregistrer.Name = "bEnregistrer";
        bEnregistrer.Size = new Size(130, 34);
        bEnregistrer.TabIndex = 7;
        bEnregistrer.Text = "&Enregistrer";
        bEnregistrer.UseVisualStyleBackColor = true;
        bEnregistrer.Click += Enregistrer_Click;
        // 
        // lEtat
        // 
        lEtat.AccessibleName = "État de la commande";
        lEtat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lEtat.Location = new Point(12, 623);
        lEtat.Name = "lEtat";
        lEtat.Size = new Size(876, 18);
        lEtat.TabIndex = 8;
        lEtat.Text = "Commande en préparation.";
        // 
        // bFacture
        // 
        bFacture.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bFacture.Location = new Point(632, 586);
        bFacture.Name = "bFacture";
        bFacture.Size = new Size(120, 34);
        bFacture.TabIndex = 6;
        bFacture.Text = "&Facture";
        bFacture.UseVisualStyleBackColor = true;
        bFacture.Click += Facture_Click;
        // 
        // FormCommande
        // 
        AcceptButton = bAjouter;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 650);
        Controls.Add(lNumero);
        Controls.Add(tbNumero);
        Controls.Add(bNouvelle);
        Controls.Add(lCommande);
        Controls.Add(lbProduits);
        Controls.Add(lQuantite);
        Controls.Add(nudQuantite);
        Controls.Add(bAjouter);
        Controls.Add(dgvLignes);
        Controls.Add(lTotal);
        Controls.Add(bEnregistrer);
        Controls.Add(lEtat);
        Controls.Add(bFacture);
        MinimumSize = new Size(820, 600);
        Name = "FormCommande";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Prise de commande";
        FormClosing += FormCommande_FormClosing;
        Load += FormCommande_Load;
        ((System.ComponentModel.ISupportInitialize)nudQuantite).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLignes).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lNumero;
    private TextBox tbNumero;
    private Button bNouvelle;
    private Label lCommande;
    private ListBox lbProduits;
    private Label lQuantite;
    private NumericUpDown nudQuantite;
    private Button bAjouter;
    private DataGridView dgvLignes;
    private Label lTotal;
    private Button bEnregistrer;
    private Label lEtat;
    private Button bFacture;
    private DataGridViewTextBoxColumn colProduit;
    private DataGridViewTextBoxColumn colQuantite;
    private DataGridViewTextBoxColumn colMontant;
}
