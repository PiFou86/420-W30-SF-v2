#nullable disable
namespace Demo.WinForms;

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
        // 
        // lResumeFacture
        // 
        lResumeFacture.AccessibleName = "Résumé de facture";
        lResumeFacture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lResumeFacture.Location = new Point(12, 9);
        lResumeFacture.Name = "lResumeFacture";
        lResumeFacture.Size = new Size(444, 243);
        lResumeFacture.TabIndex = 0;
        lResumeFacture.Text = "Facture de consultation";
        // 
        // FormFacture
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(468, 261);
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
