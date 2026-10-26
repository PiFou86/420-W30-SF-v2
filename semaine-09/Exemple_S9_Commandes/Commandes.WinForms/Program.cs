using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.WinForms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commandes.WinForms;

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

            if ((builder.Configuration["assemblage"] ?? "manuel") == "manuel")
            {
                ServiceCommandes service = new ServiceCommandes(configuration.CreerDepot(), new CatalogueProduitsMemoire());
                using FormCommande fenetre = new FormCommande(service, journal);
                System.Windows.Forms.Application.Run(fenetre);
            }
            else if (builder.Configuration["assemblage"] == "cadriciel")
            {
                builder.Services.AddScoped<IDepotCommandes>(_ => configuration.CreerDepot());
                builder.Services.AddScoped<ICatalogueProduits, CatalogueProduitsMemoire>();
                builder.Services.AddSingleton(journal);
                builder.Services.AddScoped<ServiceCommandes>();
                builder.Services.AddScoped<FormCommande>();
                using IHost hote = builder.Build();
                using IServiceScope portee = hote.Services.CreateScope();
                FormCommande fenetre = portee.ServiceProvider.GetRequiredService<FormCommande>();
                System.Windows.Forms.Application.Run(fenetre);
            }
            else
            {
                throw new InvalidOperationException("Assemblage inconnu.");
            }
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
        journal.Consigner("composition", exception);
        MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
