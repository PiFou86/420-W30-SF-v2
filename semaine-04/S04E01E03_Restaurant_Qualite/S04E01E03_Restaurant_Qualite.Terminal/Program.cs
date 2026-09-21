using Restaurant.Qualite;

internal static class Program
{
    public static void Main(string[] args)
    {
        // E01 : conservez une méthode d’assemblage manuel, puis ajoutez une
        // méthode équivalente utilisant Host.CreateApplicationBuilder(args).
        // Choisissez la première avec --manuel et la seconde par défaut.
        Client client = new("client@exemple.ca");
        ServiceCommandes service = new();
        Commande commande = service.Creer(1001, 40m, client);

        Console.Out.WriteLine($"Total : {commande.SousTotal + commande.Taxe:C}");
    }
}
