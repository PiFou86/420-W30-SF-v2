using Reservations.Domaine;
namespace Reservations.Infrastructure;

internal static class ValidationReservations
{
    public static List<Reservation> Reconstruire(List<ReservationDonnees?>? donnees)
    {
        if (donnees is null || donnees.Any(dto => dto is null))
        {
            throw new InvalidDataException("La collection de données est nulle ou contient une entrée nulle.");
        }
        // Les invariants sont revérifiés par le constructeur du domaine.
        List<Reservation> reservations = donnees.Select(dto => dto!.VersEntite()).ToList();
        if (reservations.Select(r => r.Numero).Distinct().Count() != reservations.Count)
        {
            throw new InvalidDataException("Le fichier contient des numéros répétés.");
        }
        return reservations;
    }
}
