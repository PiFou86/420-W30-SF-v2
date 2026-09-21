using Restaurant;

namespace Restaurant.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        Commande commande = new Commande("CMD-001");
        commande.AjouterLigne(
            new LigneCommande("POU-01", "Poutine", 14.50m, 2, 0m));

        Console.Out.WriteLine(
            $"Sous-total : {commande.SousTotal:0.00} $");
    }
}
