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
        FormCommande vue = new FormCommande();
        System.Windows.Forms.Application.Run(vue);
    }
}
