using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

public sealed class DepotReservationsMemoire : IDepotReservations
{
    private readonly Dictionary<int, Reservation> m_reservations = new();

    public void Ajouter(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        if (!m_reservations.TryAdd(reservation.Numero, reservation))
        {
            throw new InvalidOperationException("Numéro déjà présent.");
        }
    }

    public Reservation? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        return m_reservations.GetValueOrDefault(numero);
    }

    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        return m_reservations.Values.ToArray();
    }
}
