using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reservations.Application;
using Reservations.Domaine;
using Reservations.Infrastructure;
namespace Reservations.Terminal;

// Demos :
// dotnet run --Scenario=lister
// dotnet run --Senario=demo
// dotnet run --Scenario=lister
// dotnet run --Scenario=ajouter --Reservation:numero=4 --Reservation:titre="Reunion PFL"
// dotnet run --Scenario=lister

internal static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(
            new HostApplicationBuilderSettings
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory
            });
        string? cheminConfigure = builder.Configuration["Donnees:Chemin"];
        ArgumentException.ThrowIfNullOrWhiteSpace(cheminConfigure);
        string chemin = Path.GetFullPath(cheminConfigure, AppContext.BaseDirectory);
        string format = builder.Configuration["Donnees:Format"] ?? "json";
        builder.Services.AddScoped<IDepotReservations>(
            fournisseur => CreerDepot(format, chemin));
        builder.Services.AddScoped<ServiceReservations>();
        using IHost hote = builder.Build();
        ILogger journal = hote.Services.GetRequiredService<ILoggerFactory>()
            .CreateLogger("Demarrage");
        try
        {
            using IServiceScope portee = hote.Services.CreateScope();
            ServiceReservations service = portee.ServiceProvider
                .GetRequiredService<ServiceReservations>();
            string scenario = builder.Configuration["Scenario"] ?? "demo";
            switch (scenario)
            {
                case "demo":
                    AfficherResultat(service.Creer(1, "Réunion"));
                    AfficherResultat(service.Creer(2, "Atelier"));
                    Console.Out.WriteLine("Relecture avec une nouvelle instance du dépôt :");
                    Afficher(CreerDepot(format, chemin).Lire());
                    Console.Out.WriteLine("Refus attendu d'un doublon :");
                    AfficherResultat(service.Creer(1, "Autre titre"));
                    MontrerConversions(service.Lire());
                    break;
                case "lister":
                    Afficher(service.Lire());
                    break;
                case "ajouter":
                    if (!int.TryParse(builder.Configuration["Reservation:Numero"], out int numero)
                        || string.IsNullOrWhiteSpace(builder.Configuration["Reservation:Titre"]))
                    {
                        Console.Error.WriteLine("Fournir un numéro entier et un titre non vide.");
                        Environment.ExitCode = 2;
                        return;
                    }
                    AfficherResultat(service.Creer(numero, builder.Configuration["Reservation:Titre"]!));
                    break;
                default:
                    Console.Error.WriteLine("Scénario prévu : demo, lister ou ajouter.");
                    Environment.ExitCode = 2;
                    return;
            }
            journal.LogInformation("Scénario {Scenario} terminé.", scenario);
        }
        catch (Exception exception)
        {
            journal.LogError("Échec de l'opération ({Categorie}).", exception.GetType().Name);
            Console.Error.WriteLine("Impossible de terminer l'opération sur les réservations.");
            Environment.ExitCode = 1;
            return;
        }
    }

    private static IDepotReservations CreerDepot(string format, string chemin)
    {
        switch (format.ToLowerInvariant())
        {
            case "json":
                return new DepotReservationsJson(chemin);
            case "yaml":
                return new DepotReservationsYaml(chemin);
            default:
                throw new ArgumentException("Format prévu : json ou yaml.");
        }
    }

    private static void AfficherResultat(Result<Reservation> resultat)
    {
        if (resultat.EstSucces)
        {
            Console.Out.WriteLine($"Réservation {resultat.Valeur!.Numero} enregistrée.");
        }
        else
        {
            Console.Out.WriteLine($"Refus : {resultat.MessageErreur}");
        }
    }

    private static void Afficher(List<Reservation> reservations)
    {
        foreach (Reservation reservation in reservations)
        {
            Console.Out.WriteLine($"{reservation.Numero} : {reservation.Titre}");
        }
        Console.Out.WriteLine($"{reservations.Count} réservation(s).");
    }

    private static void MontrerConversions(List<Reservation> reservations)
    {
        ReservationDonnees dto = new(reservations[0]);
        Reservation directe = dto.VersEntite();
        ConvertisseurReservations convertisseur = new();
        Reservation viaClasse = convertisseur.VersEntite(dto);
        List<Reservation> collection = convertisseur.VersEntites(
            reservations.Select(convertisseur.VersDto));
        Console.Out.WriteLine($"Conversions : {directe.Titre} / {viaClasse.Titre}; {collection.Count} entités reconstruites.");
    }
}
