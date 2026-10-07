using Commandes.Application;
using Commandes.Infrastructure;

namespace TP01_Commandes.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        IDepotCommandes depot = new DepotCommandesMemoire();
        _ = new PrendreCommande(depot);
        Console.Out.WriteLine("Départ du lot Commandes prêt à compléter.");
    }
}
