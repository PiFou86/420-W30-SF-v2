using Contacts.Application;
using Contacts.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Contacts.WinForms;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        IJournalIncidents journal = new JournalIncidents(Path.Combine(AppContext.BaseDirectory, "contacts.log"));
        try
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
            {
                Args = args, ContentRootPath = AppContext.BaseDirectory
            });
            builder.Logging.ClearProviders();
            ConfigurationContacts configuration = new ConfigurationContacts(builder.Configuration, AppContext.BaseDirectory);
            journal = configuration.CreerJournal();
            switch (builder.Configuration["assemblage"] ?? "manuel")
            {
                case "manuel":
                    IDepotContacts depot = configuration.CreerDepot();
                    CreerContact creation = new CreerContact(depot);
                    ConsulterContacts consultation = new ConsulterContacts(depot);
                    using (FormContacts fenetre = new FormContacts(creation, consultation, journal))
                    {
                        System.Windows.Forms.Application.Run(fenetre);
                    }
                    break;
                case "cadriciel":
                    builder.Services.AddScoped<IDepotContacts>(_ => configuration.CreerDepot());
                    builder.Services.AddScoped<CreerContact>();
                    builder.Services.AddScoped<ConsulterContacts>();
                    builder.Services.AddSingleton(journal);
                    builder.Services.AddScoped<FormContacts>();
                    using (IHost hote = builder.Build())
                    using (IServiceScope portee = hote.Services.CreateScope())
                    {
                        FormContacts fenetre = portee.ServiceProvider.GetRequiredService<FormContacts>();
                        System.Windows.Forms.Application.Run(fenetre);
                    }
                    break;
                default:
                    throw new InvalidOperationException("Mode d'assemblage inconnu.");
            }
        }
        catch (UnauthorizedAccessException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (System.Text.Json.JsonException exception)
        {
            SignalerIncident(journal, exception);
        }
        catch (InvalidDataException exception)
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
        journal.Consigner("lancement contacts", exception);
        MessageBox.Show("Impossible de démarrer la fiche de contact.", "Contacts",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
