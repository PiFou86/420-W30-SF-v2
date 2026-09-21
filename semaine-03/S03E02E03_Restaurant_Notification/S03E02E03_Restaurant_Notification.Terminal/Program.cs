using Restaurant;

internal static class Program
{
    public static void Main(string[] args)
    {
        ServiceCommandes service = new();
        service.Creer(1001);
    }
}
