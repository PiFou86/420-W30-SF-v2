using Reservations.Domaine;
namespace Reservations.Infrastructure;

public class ReservationDonnees
{
    public int Numero { get; set; }
    public string? Titre { get; set; }

    // Nécessaire à la désérialisation.
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
}
