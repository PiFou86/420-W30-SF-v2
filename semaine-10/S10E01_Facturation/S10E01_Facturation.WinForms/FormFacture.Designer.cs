#nullable disable
namespace S10E01_Facturation.WinForms;

partial class FormFacture
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
        lResumeFacture = new Label();
        SuspendLayout();
        // lResumeFacture
        lResumeFacture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lResumeFacture.Location = new Point(20, 20);
        lResumeFacture.Name = "lResumeFacture";
        lResumeFacture.Size = new Size(440, 240);
        lResumeFacture.Text = "Facture de consultation";
        lResumeFacture.AccessibleName = "Résumé de facture";
        // FormFacture
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(480, 280);
        Controls.Add(lResumeFacture);
        MinimumSize = new Size(460, 300);
        Name = "FormFacture";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Facture de consultation";
        Load += FormFacture_Load;
        Disposed += FormFacture_Disposed;
        ResumeLayout(false);
    }

    private Label lResumeFacture;
}
