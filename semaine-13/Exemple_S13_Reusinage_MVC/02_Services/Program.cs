using Commandes.Application;
using Commandes.Infrastructure;
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
        FormCommande vue = new FormCommande(service);
        System.Windows.Forms.Application.Run(vue);
    }
}
