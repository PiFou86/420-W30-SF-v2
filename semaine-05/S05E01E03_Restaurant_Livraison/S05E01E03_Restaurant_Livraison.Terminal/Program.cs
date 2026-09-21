using Restaurant.Livraison;

internal static class Program
{
    public static void Main(string[] args)
    {
        Client client = new("Or", 1200);
        ServiceLivraisons service = new();
        decimal frais = service.CalculerFrais(client, 35m, 4, "prioritaire");

        Console.Out.WriteLine($"Frais : {frais:C}");
    }
}
