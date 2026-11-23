using Commandes.Presentation;

namespace Commandes.Presentation.WinForms;

public partial class FormCommande : Form, IObserver<ResultatConsultation>
{
    private readonly ControleurCommande? m_controleur;

    public FormCommande()
    {
        InitializeComponent();
    }

    public FormCommande(ControleurCommande controleur) : this()
    {
        m_controleur = controleur;
    }

    private void bConsulter_Click(object? sender, EventArgs e)
    {
        ControleurCommande controleur = m_controleur
            ?? throw new InvalidOperationException("Le contrôleur doit être fourni au démarrage.");
        controleur.Consulter((int)nudNumero.Value);
    }

    public void OnNext(ResultatConsultation resultat)
    {
        if (resultat.Total is decimal total)
        {
            lTotal.Text = total.ToString("C2");
            return;
        }

        lTotal.Text = resultat.Raison;
    }

    public void OnError(Exception erreur)
    {
        lTotal.Text = "Consultation impossible pour le moment.";
    }

    public void OnCompleted()
    {
        bConsulter.Enabled = false;
    }
}
