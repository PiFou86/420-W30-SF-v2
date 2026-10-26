#nullable disable
namespace Commandes.Presentation.WinForms;

partial class FormNouvelleCommande
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
        lInstruction = new Label();
        lNumero = new Label();
        nudNumero = new NumericUpDown();
        bValider = new Button();
        bAnnuler = new Button();
        ((System.ComponentModel.ISupportInitialize)nudNumero).BeginInit();
        SuspendLayout();
        // 
        // lInstruction
        // 
        lInstruction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lInstruction.Location = new Point(12, 9);
        lInstruction.Name = "lInstruction";
        lInstruction.Size = new Size(260, 24);
        lInstruction.TabIndex = 0;
        lInstruction.Text = "Choisissez un numéro.";
        // 
        // lNumero
        // 
        lNumero.Location = new Point(12, 37);
        lNumero.Name = "lNumero";
        lNumero.Size = new Size(80, 28);
        lNumero.TabIndex = 1;
        lNumero.Text = "Numéro";
        // 
        // nudNumero
        // 
        nudNumero.AccessibleName = "Numéro de la nouvelle commande";
        nudNumero.Location = new Point(98, 38);
        nudNumero.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
        nudNumero.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudNumero.Name = "nudNumero";
        nudNumero.Size = new Size(160, 27);
        nudNumero.TabIndex = 2;
        nudNumero.Value = new decimal(new int[] { 1042, 0, 0, 0 });
        // 
        // bValider
        // 
        bValider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bValider.Location = new Point(66, 80);
        bValider.Name = "bValider";
        bValider.Size = new Size(100, 36);
        bValider.TabIndex = 3;
        bValider.Text = "&Valider";
        bValider.Click += bValider_Click;
        // 
        // bAnnuler
        // 
        bAnnuler.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        bAnnuler.CausesValidation = false;
        bAnnuler.DialogResult = DialogResult.Cancel;
        bAnnuler.Location = new Point(172, 80);
        bAnnuler.Name = "bAnnuler";
        bAnnuler.Size = new Size(100, 36);
        bAnnuler.TabIndex = 4;
        bAnnuler.Text = "&Annuler";
        // 
        // FormNouvelleCommande
        // 
        AcceptButton = bValider;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = bAnnuler;
        ClientSize = new Size(284, 128);
        Controls.Add(lInstruction);
        Controls.Add(lNumero);
        Controls.Add(nudNumero);
        Controls.Add(bValider);
        Controls.Add(bAnnuler);
        Font = new Font("Segoe UI", 11F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormNouvelleCommande";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Nouvelle commande";
        Load += FormNouvelleCommande_Load;
        Shown += FormNouvelleCommande_Shown;
        ((System.ComponentModel.ISupportInitialize)nudNumero).EndInit();
        ResumeLayout(false);
    }
    private Label lInstruction;
    private Label lNumero;
    private NumericUpDown nudNumero;
    private Button bValider;
    private Button bAnnuler;

}
