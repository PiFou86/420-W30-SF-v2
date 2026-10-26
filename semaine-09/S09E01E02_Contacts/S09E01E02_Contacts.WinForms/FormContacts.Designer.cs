#nullable disable
namespace Contacts.WinForms;

partial class FormContacts
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
        SuspendLayout();
        // 
        // FormContacts
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(684, 411);
        MinimumSize = new Size(700, 450);
        Name = "FormContacts";
        Text = "Fiche de contact — à construire";
        Load += FormContacts_Load;
        ResumeLayout(false);
        // Construire les contrôles dans le designer et relier chaque Click une seule fois.
    }
}
