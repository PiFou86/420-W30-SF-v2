#nullable disable
namespace S10E01_Facturation.WinForms;

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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
        // colProduit
        colProduit.DataPropertyName = "Produit";
        colProduit.HeaderText = "Produit";
        colProduit.Name = "colProduit";
        colProduit.ReadOnly = true;
        // colQuantite
        colQuantite.DataPropertyName = "Quantite";
        colQuantite.HeaderText = "Quantité";
        colQuantite.Name = "colQuantite";
        colQuantite.ReadOnly = true;
        // colMontant
        colMontant.DataPropertyName = "Montant";
        colMontant.HeaderText = "Montant";
        colMontant.Name = "colMontant";
        colMontant.ReadOnly = true;
        colProduit.FillWeight = 200F;
        dataGridViewCellStyle1.Format = "C2";
        colMontant.DefaultCellStyle = dataGridViewCellStyle1;
        // lNumero
        lNumero.Location = new Point(16, 20);
        lNumero.Name = "lNumero";
        lNumero.Size = new Size(270, 24);
        lNumero.Text = "Numéro de la nouvelle commande";
        lNumero.TabStop = false;
        // tbNumero
        tbNumero.Location = new Point(300, 16);
        tbNumero.Name = "tbNumero";
        tbNumero.Size = new Size(90, 27);
        tbNumero.TabIndex = 0;
        tbNumero.Text = "1";
        tbNumero.AccessibleName = "Numéro de la nouvelle commande";
        // bNouvelle
        bNouvelle.Location = new Point(410, 14);
        bNouvelle.Name = "bNouvelle";
        bNouvelle.Size = new Size(120, 32);
        bNouvelle.Text = "&Nouvelle";
        bNouvelle.TabIndex = 1;
        bNouvelle.UseVisualStyleBackColor = true;
        bNouvelle.Click += Nouvelle_Click;
        // lCommande
        lCommande.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lCommande.Location = new Point(16, 64);
        lCommande.Name = "lCommande";
        lCommande.Size = new Size(700, 26);
        lCommande.Text = "Commande en préparation";
        lCommande.TabStop = false;
        // lbProduits
        lbProduits.Location = new Point(16, 106);
        lbProduits.Name = "lbProduits";
        lbProduits.Size = new Size(280, 100);
        lbProduits.TabIndex = 2;
        lbProduits.AccessibleName = "Produit à ajouter";
        // lQuantite
        lQuantite.Location = new Point(320, 110);
        lQuantite.Name = "lQuantite";
        lQuantite.Size = new Size(90, 24);
        lQuantite.Text = "Quantité";
        lQuantite.TabStop = false;
        // nudQuantite
        nudQuantite.Location = new Point(420, 106);
        nudQuantite.Name = "nudQuantite";
        nudQuantite.Size = new Size(90, 27);
        nudQuantite.TabIndex = 3;
        nudQuantite.Minimum = 1;
        nudQuantite.Maximum = 1000;
        nudQuantite.Value = 1;
        nudQuantite.AccessibleName = "Quantité à ajouter";
        // bAjouter
        bAjouter.Location = new Point(540, 104);
        bAjouter.Name = "bAjouter";
        bAjouter.Size = new Size(120, 32);
        bAjouter.Text = "&Ajouter";
        bAjouter.TabIndex = 4;
        bAjouter.UseVisualStyleBackColor = true;
        bAjouter.Click += Ajouter_Click;
        bAjouter.Enabled = false;
        // dgvLignes
        dgvLignes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLignes.Location = new Point(16, 222);
        dgvLignes.Name = "dgvLignes";
        dgvLignes.Size = new Size(868, 300);
        dgvLignes.TabIndex = 5;
        dgvLignes.AllowUserToAddRows = false;
        dgvLignes.AllowUserToDeleteRows = false;
        dgvLignes.AutoGenerateColumns = false;
        dgvLignes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLignes.Columns.AddRange(new DataGridViewColumn[] { colProduit, colQuantite, colMontant });
        dgvLignes.ReadOnly = true;
        dgvLignes.RowHeadersVisible = false;
        dgvLignes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLignes.AccessibleName = "Lignes de la commande courante";
        // lTotal
        lTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lTotal.Location = new Point(16, 546);
        lTotal.Name = "lTotal";
        lTotal.Size = new Size(560, 30);
        lTotal.Text = "Sous-total : 0,00 $";
        lTotal.TabStop = false;
        // bEnregistrer
        bEnregistrer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bEnregistrer.Location = new Point(754, 540);
        bEnregistrer.Name = "bEnregistrer";
        bEnregistrer.Size = new Size(130, 34);
        bEnregistrer.Text = "&Enregistrer";
        bEnregistrer.TabIndex = 7;
        bEnregistrer.UseVisualStyleBackColor = true;
        bEnregistrer.Click += Enregistrer_Click;
        bEnregistrer.Enabled = false;
        // lEtat
        lEtat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lEtat.Location = new Point(16, 590);
        lEtat.Name = "lEtat";
        lEtat.Size = new Size(868, 42);
        lEtat.Text = "Commande en préparation.";
        lEtat.TabStop = false;
        lEtat.AccessibleName = "État de la commande";
        // bFacture
        bFacture.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bFacture.Location = new Point(614, 540);
        bFacture.Name = "bFacture";
        bFacture.Size = new Size(120, 34);
        bFacture.Text = "&Facture";
        bFacture.TabIndex = 6;
        bFacture.UseVisualStyleBackColor = true;
        bFacture.Click += Facture_Click;
        // FormCommande
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
        Load += FormCommande_Load;
        FormClosing += FormCommande_FormClosing;
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
