#nullable enable
namespace Contacts.WinForms;

partial class FormContacts
{
    private System.ComponentModel.IContainer? components = null;

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
        Name = "FormContacts";
        Text = "Fiche de contact — à construire";
        ClientSize = new Size(850, 580);
        MinimumSize = new Size(700, 450);
        AutoScaleMode = AutoScaleMode.Font;
        Load += FormContacts_Load;
        ResumeLayout(false);
        // Construire les contrôles dans le designer et relier chaque Click une seule fois.
    }
}
