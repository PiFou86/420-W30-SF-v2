using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.WinForms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commandes.Portee.WinForms;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        IJournalIncidents journal = new JournalIncidents(Path.Combine(AppContext.BaseDirectory, "commandes.log"));
        try
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory
            });
            builder.Logging.ClearProviders();
            ConfigurationCommandes configuration = new ConfigurationCommandes(builder.Configuration, AppContext.BaseDirectory);
            journal = configuration.CreerJournal();

            // Un dépôt commun, mais un service contenant le brouillon par portée.
            ServicesSessionCommandes.AjouterSessionsCommandes(builder.Services, configuration.CreerDepot(),
                new CatalogueProduitsMemoire(), journal);
            builder.Services.AddScoped<FormCommande>();
            builder.Services.AddSingleton<IFabriqueSaisieCommande, FabriqueSaisieCommande>();
            builder.Services.AddSingleton<FormAccueil>();
            using IHost hote = builder.Build();
            FormAccueil accueil = hote.Services.GetRequiredService<FormAccueil>();
            System.Windows.Forms.Application.Run(accueil);
        }
        catch (InvalidDataException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (System.Text.Json.JsonException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (IOException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (ArgumentException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (InvalidOperationException exception)
        {
            SignalerIncident(journal, exception);
        }
    }

    private static void SignalerIncident(IJournalIncidents journal, Exception exception)
    {
        journal.Consigner("composition avec portées", exception);
        MessageBox.Show("Impossible de démarrer la liste des commandes.", "Commandes",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
