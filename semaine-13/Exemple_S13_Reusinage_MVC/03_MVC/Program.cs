using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation;
using System.Globalization;
using Commandes.Presentation.WinForms;

namespace Commandes.Demonstration;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("fr-CA");
        ApplicationConfiguration.Initialize();
        string chemin = Path.Combine(AppContext.BaseDirectory, "commandes.json");
        IDepotCommandes depot = new DepotCommandesJson(chemin);
        ServiceCommandes service = new ServiceCommandes(depot);
        ModeleConsultation modele = new ModeleConsultation(service);
        ControleurCommande controleur = new ControleurCommande(modele);
        using FormCommande vue = new FormCommande(controleur);
        using IDisposable abonnement = modele.Subscribe(vue);
        System.Windows.Forms.Application.Run(vue);
    }
}
