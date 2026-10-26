using Contacts.Application;
using Contacts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Contacts.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
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
            CreerContact creation = new CreerContact(configuration.CreerDepot());
            ConsulterContacts consultation = new ConsulterContacts(configuration.CreerDepot());
            IReadOnlyCollection<ContactDto> contacts;
            switch (builder.Configuration["Scenario"] ?? "demo")
            {
                case "demo":
                case "ajouter":
                    DemandeCreationContact demande = new DemandeCreationContact(builder.Configuration["Contact:Nom"] ?? "Alex Roy",
                        builder.Configuration["Contact:Courriel"] ?? "alex@example.test");
                    Result<ContactDto> resultat = creation.Executer(demande);
                    if (resultat.EstSucces)
                    {
                        Console.Out.WriteLine($"Contact créé : {resultat.Valeur.Nom}.");
                    }
                    else
                    {
                        Console.Out.WriteLine($"Refus : {resultat.MessageErreur}");
                    }
                    contacts = consultation.ObtenirTous();
                    break;
                case "lister":
                    contacts = consultation.ObtenirTous();
                    break;
                case "rechercher":
                    contacts = consultation.Rechercher(builder.Configuration["MotClef"] ?? string.Empty);
                    break;
                default:
                    Console.Error.WriteLine("Scénario inconnu.");
                    Environment.ExitCode = 1;
                    return;
            }

            foreach (ContactDto contact in contacts)
            {
                Console.Out.WriteLine($"{contact.Nom} | {contact.Courriel}");
            }

            Console.Out.WriteLine($"{contacts.Count} contact(s).");
            return;
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
        journal.Consigner("terminal contacts", exception);
        Console.Error.WriteLine("Opération impossible pour le moment.");
        Environment.ExitCode = 1;
    }
}
