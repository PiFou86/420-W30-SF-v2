using System.Diagnostics;
using System.Text.Json;
using Commandes.Application;

namespace Commandes.Presentation.WinForms;

public partial class FormCommande : Form
{
    private readonly ServiceCommandes? m_service;

    public FormCommande()
    {
        InitializeComponent();
    }

    public FormCommande(ServiceCommandes service) : this()
    {
        m_service = service;
    }

    private void bConsulter_Click(object? sender, EventArgs e)
    {
        ServiceCommandes service = m_service
            ?? throw new InvalidOperationException("Le service doit être fourni au démarrage.");
        try
        {
            Result<decimal> resultat = service.ConsulterTotal((int)nudNumero.Value);
            if (!resultat.EstSucces)
            {
                lTotal.Text = resultat.MessageErreur;
                return;
            }
            lTotal.Text = resultat.Valeur.ToString("C2");
        }
        catch (IOException exception)
        {
            PresenterIncident(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            PresenterIncident(exception);
        }
        catch (JsonException exception)
        {
            PresenterIncident(exception);
        }
    }

    private void PresenterIncident(Exception exception)
    {
        Trace.TraceError(exception.ToString());
        lTotal.Text = "Consultation impossible pour le moment.";
    }
}
