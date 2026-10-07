using Reservations.Application;
using Reservations.Infrastructure;

namespace TP01_Reservations.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        IDepotReservations depot = new DepotReservationsMemoire();
        _ = new CreerReservation(depot);
        Console.Out.WriteLine("Départ du lot Reservations prêt à compléter.");
    }
}
