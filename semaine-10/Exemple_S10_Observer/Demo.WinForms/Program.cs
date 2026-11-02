using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.DotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.WinForms;

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

            if ((builder.Configuration["assemblage"] ?? "manuel") == "manuel")
            {
                ServiceCommandes commandes = new ServiceCommandes(CreerDepot(format, chemin), new CatalogueProduitsMemoire());
                InitialiserCommande(commandes);
                using ModeleCommande modele = new ModeleCommande(commandes, new ServiceFacturation(commandes), journal);
                using FormCommande fenetre = new FormCommande();
                using ControleurCommande controleur = new ControleurCommande(modele, fenetre, journal);
                Executer(modele, fenetre, controleur);
            }
            else if (builder.Configuration["assemblage"] == "cadriciel")
            {
                builder.Services.AddScoped<IDepotCommandes>(_ => CreerDepot(format, chemin));
                builder.Services.AddScoped<ICatalogueProduits, CatalogueProduitsMemoire>();
                builder.Services.AddSingleton(journal);
                builder.Services.AddScoped<ServiceCommandes>();
                builder.Services.AddScoped<ServiceFacturation>();
                builder.Services.AddScoped<ModeleCommande>();
                builder.Services.AddScoped<FormCommande>();
                builder.Services.AddScoped<IVueCommande>(services => services.GetRequiredService<FormCommande>());
                builder.Services.AddScoped<ControleurCommande>();
                using IHost hote = builder.Build();
                using IServiceScope portee = hote.Services.CreateScope();
                ServiceCommandes commandes = portee.ServiceProvider.GetRequiredService<ServiceCommandes>();
                InitialiserCommande(commandes);
                ModeleCommande modele = portee.ServiceProvider.GetRequiredService<ModeleCommande>();
                FormCommande fenetre = portee.ServiceProvider.GetRequiredService<FormCommande>();
                ControleurCommande controleur = portee.ServiceProvider.GetRequiredService<ControleurCommande>();
                Executer(modele, fenetre, controleur);
            }
            else
            {
                throw new InvalidOperationException("Assemblage inconnu.");
            }
        }
        catch (IOException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (InvalidDataException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (UnauthorizedAccessException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (System.Text.Json.JsonException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (ArgumentException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (InvalidOperationException exception)
        {
            journal.Consigner("composition", exception);
            MessageBox.Show("Impossible de démarrer la prise de commande.", "Prise de commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void InitialiserCommande(ServiceCommandes commandes)
    {
        Result<CommandeDto> resultat = commandes.CreerAutomatiquement();
        if (!resultat.EstSucces)
        {
            throw new InvalidOperationException(resultat.MessageErreur);
        }
    }

    private static IDepotCommandes CreerDepot(string format, string chemin)
    {
        switch (format.ToLowerInvariant())
        {
            case "memoire":
                return new DepotCommandesMemoire();
            case "json":
                return new DepotCommandesJson(chemin);
            default:
                throw new InvalidOperationException("Format inconnu.");
        }
    }

    private static void Executer(ModeleCommande modele, FormCommande fenetre, ControleurCommande controleur)
    {
        fenetre.Relier(controleur, modele.ConsulterProduits());
        fenetre.DemandeFacture += (_, _) =>
        {
            FormFacture facture = new FormFacture(modele);
            facture.Show(fenetre);
        };
        System.Windows.Forms.Application.Run(fenetre);
    }
}
