using Commandes.Application;
using Commandes.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace S09E01_Commandes.WinForms;

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
            string cheminJournal = Path.GetFullPath(builder.Configuration["Journal:Chemin"] ?? "commandes.log", AppContext.BaseDirectory);
            journal = new JournalIncidents(cheminJournal);
            string format = builder.Configuration["Donnees:Format"] ?? "memoire";
            string chemin = Path.GetFullPath(builder.Configuration["Donnees:Chemin"] ?? "commandes.json", AppContext.BaseDirectory);
            if (string.Equals(chemin, cheminJournal, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Journal et données doivent être distincts.");
            }

            IDepotCommandes CreerDepot()
            {
                return format.ToLowerInvariant() switch
                {
                    "memoire" => new DepotCommandesMemoire(),
                    "json" => new DepotCommandesJson(chemin),
                    _ => throw new InvalidOperationException("Format inconnu.")
                };
            }

            if ((builder.Configuration["assemblage"] ?? "manuel") == "manuel")
            {
                ServiceCommandes service = new(CreerDepot(), new CatalogueProduitsMemoire());
                using FormCommande fenetre = new(service, journal);
                System.Windows.Forms.Application.Run(fenetre);
            }
            else if (builder.Configuration["assemblage"] == "cadriciel")
            {
                builder.Services.AddScoped<IDepotCommandes>(_ => CreerDepot());
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
        catch (Exception exception) when (exception is IOException or InvalidDataException
            or UnauthorizedAccessException or System.Text.Json.JsonException
            or ArgumentException or InvalidOperationException)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
