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
        lNumero = new Label();
        nudNumero = new NumericUpDown();
        bConsulter = new Button();
        lTotal = new Label();
        ((System.ComponentModel.ISupportInitialize)nudNumero).BeginInit();
        SuspendLayout();
        lNumero.AutoSize = true;
        lNumero.Location = new Point(16, 20);
        lNumero.Name = "lNumero";
        lNumero.Size = new Size(160, 20);
        lNumero.TabIndex = 0;
        lNumero.Text = "Numéro de commande";
        nudNumero.Location = new Point(195, 16);
        nudNumero.Minimum = 1;
        nudNumero.Maximum = 99999;
        nudNumero.Name = "nudNumero";
        nudNumero.Size = new Size(110, 27);
        nudNumero.TabIndex = 1;
        nudNumero.Value = 1;
        bConsulter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        bConsulter.Location = new Point(330, 13);
        bConsulter.Name = "bConsulter";
        bConsulter.Size = new Size(150, 34);
        bConsulter.TabIndex = 2;
        bConsulter.Text = "&Consulter le total";
        bConsulter.Click += bConsulter_Click;
        lTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lTotal.Location = new Point(16, 70);
        lTotal.Name = "lTotal";
        lTotal.Size = new Size(464, 70);
        lTotal.TabIndex = 3;
        lTotal.Text = "Choisir une commande, puis consulter.";
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(500, 160);
        Controls.Add(lNumero);
        Controls.Add(nudNumero);
        Controls.Add(bConsulter);
        Controls.Add(lTotal);
        MinimumSize = new Size(516, 200);
        Name = "FormCommande";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Consultation de commande";
        ((System.ComponentModel.ISupportInitialize)nudNumero).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lNumero;
    private NumericUpDown nudNumero;
    private Button bConsulter;
    private Label lTotal;
}
