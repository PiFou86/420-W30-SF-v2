#nullable enable
namespace S09E01_Commandes.WinForms;

partial class FormCommande
{
    private System.ComponentModel.IContainer? m_components;

    protected override void Dispose(bool disposing)
    {
        if (disposing && m_components is not null)
        {
            m_components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        m_components = new System.ComponentModel.Container();
        SuspendLayout();
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 650);
        MinimumSize = new Size(800, 550);
        Name = "FormCommande";
        Text = "Prise de commande - à construire";
        ResumeLayout(false);
        // TODO : construire la présentation avec le designer.
    }
}
