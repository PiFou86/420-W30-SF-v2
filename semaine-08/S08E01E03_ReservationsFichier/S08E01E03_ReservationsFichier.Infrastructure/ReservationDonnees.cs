using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

// Représentation du fichier uniquement; l'entité conserve les invariants.
internal sealed class ReservationDonnees
{
    public int Numero { get; set; }
    public string? Titre { get; set; }

    public ReservationDonnees()
    {
    }

    public ReservationDonnees(Reservation reservation)
    {
        Numero = reservation.Numero;
        Titre = reservation.Titre;
    }

    public Reservation VersEntite()
    {
        return new Reservation(Numero, Titre!);
    }

    public static List<Reservation> Convertir(List<ReservationDonnees?>? donnees)
    {
        if (donnees is null)
        {
            throw new InvalidDataException("Le document doit contenir une liste.");
        }

        List<Reservation> reservations = new();
        HashSet<int> numeros = new();
        foreach (ReservationDonnees? item in donnees)
        {
            if (item is null)
            {
                throw new InvalidDataException("Une entrée est nulle.");
            }

            Reservation reservation;
            try
            {
                reservation = item.VersEntite();
            }
            catch (ArgumentException exception)
            {
                throw new InvalidDataException("Une entrée viole les invariants.", exception);
            }

            if (!numeros.Add(reservation.Numero))
            {
                throw new InvalidDataException("Un numéro apparaît plusieurs fois.");
            }

            reservations.Add(reservation);
        }

        return reservations;
    }
}
