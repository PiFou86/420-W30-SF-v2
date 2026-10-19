using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reservations.Application;
using Reservations.Infrastructure;

namespace S08E01E03_ReservationsFichier.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        JournalIncidents? journal = null;
        try
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory
            });
            builder.Logging.ClearProviders();
            string format = builder.Configuration["Donnees:Format"] ?? "json";
            string chemin = ResoudreChemin(builder.Configuration["Donnees:Chemin"]);
            string cheminJournal = ResoudreChemin(builder.Configuration["Journal:Chemin"]);
            if (string.Equals(chemin, cheminJournal, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Journal et données doivent être distincts.");
            }

            journal = new JournalIncidents(cheminJournal);
            string assemblage = builder.Configuration["assemblage"] ?? "manuel";
            if (assemblage == "manuel")
            {
                IDepotReservations depot = CreerDepot(format, chemin);
                Afficher(new ServiceReservations(depot));
            }
            else if (assemblage == "cadriciel")
            {
                builder.Services.AddScoped<IDepotReservations>(_ => CreerDepot(format, chemin));
                builder.Services.AddScoped<ServiceReservations>();
                using IHost hote = builder.Build();
                using IServiceScope portee = hote.Services.CreateScope();
                ServiceReservations service = portee.ServiceProvider.GetRequiredService<ServiceReservations>();
                Afficher(service);
            }
            else
            {
                throw new InvalidOperationException("Mode d'assemblage inconnu.");
            }
        }
        catch (Exception exception) when (exception is IOException
            or InvalidDataException or UnauthorizedAccessException or System.Text.Json.JsonException
            or YamlDotNet.Core.YamlException or ArgumentException or InvalidOperationException)
        {
            journal?.Consigner("lecture/configuration", exception);
            Console.Error.WriteLine("Impossible de charger les réservations pour le moment.");
            Environment.ExitCode = 1;
        }
    }

    private static string ResoudreChemin(string? chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        return Path.GetFullPath(chemin, AppContext.BaseDirectory);
    }

    private static IDepotReservations CreerDepot(string format, string chemin)
    {
        return format.ToLowerInvariant() switch
        {
            "json" => new DepotReservationsJson(chemin),
            "yaml" => new DepotReservationsYaml(chemin),
            "memoire" => new DepotReservationsMemoire(),
            _ => throw new InvalidOperationException("Format de dépôt inconnu.")
        };
    }

    private static void Afficher(ServiceReservations service)
    {
        ReservationDto? reservation = service.Obtenir(1);
        Console.Out.WriteLine(reservation is null
            ? "Aucune réservation trouvée."
            : new ReservationViewModel(reservation).Libelle);
    }
}
