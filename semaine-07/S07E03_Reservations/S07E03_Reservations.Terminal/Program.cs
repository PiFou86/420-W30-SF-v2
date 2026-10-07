using Reservations;

namespace S07E03_Reservations.Terminal;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Contains("--exceptions"))
        {
            Console.Out.WriteLine($"Trace valide : {string.Join(", ", ParcoursExceptions.Tracer(17))}");
            Console.Out.WriteLine($"Trace invalide : {string.Join(", ", ParcoursExceptions.Tracer(0))}");
            return;
        }

        if (args.Contains("--depot"))
        {
            IDepotReservations depot = new DepotReservationsMemoire();
            depot.Ajouter(new Reservation(17, "Réunion"));
            depot.Ajouter(new Reservation(18, "Atelier"));
            Console.Out.WriteLine($"Réservation 17 : {depot.Obtenir(17)?.Titre}");
            Console.Out.WriteLine($"Recherche 99 absente : {depot.Obtenir(99) is null}");
            Console.Out.WriteLine($"Nombre de réservations : {depot.ObtenirToutes().Count}");
            return;
        }

        // Mode --collections, également utilisé par défaut.
        DemonstrationCollections.Executer();
    }
}
