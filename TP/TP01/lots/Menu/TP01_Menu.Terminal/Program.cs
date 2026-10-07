using Menu.Application;
using Menu.Infrastructure;

namespace TP01_Menu.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        IDepotMenu depot = new DepotMenuMemoire();
        _ = new AjouterElementMenu(depot);
        Console.Out.WriteLine("Départ du lot Menu prêt à compléter.");
    }
}
