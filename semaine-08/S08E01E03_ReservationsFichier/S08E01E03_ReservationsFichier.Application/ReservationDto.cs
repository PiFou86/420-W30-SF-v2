using Reservations.Domaine;

namespace Reservations.Application;

public sealed record ReservationDto
{
    public int Numero { get; }
    public string Titre { get; }

    public ReservationDto(int numero, string titre)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(titre);
        Numero = numero;
        Titre = titre;
    }

    public ReservationDto(Reservation reservation)
        : this(Verifier(reservation).Numero, reservation.Titre)
    {
    }

    private static Reservation Verifier(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        return reservation;
    }
}
