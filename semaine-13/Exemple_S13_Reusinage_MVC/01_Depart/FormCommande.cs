using System.Diagnostics;
using System.Text.Json;

namespace Commandes.Presentation.WinForms;

public partial class FormCommande : Form
{
    private readonly string m_chemin;

    public FormCommande()
    {
        InitializeComponent();
        m_chemin = Path.Combine(AppContext.BaseDirectory, "commandes.json");
    }

    private void bConsulter_Click(object? sender, EventArgs e)
    {
        try
        {
            lTotal.Text = ConsulterTotal((int)nudNumero.Value, m_chemin);
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
