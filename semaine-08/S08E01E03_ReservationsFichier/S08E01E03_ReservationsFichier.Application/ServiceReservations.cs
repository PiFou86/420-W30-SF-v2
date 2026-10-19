using Reservations.Domaine;

namespace Reservations.Application;

public sealed class ServiceReservations
{
    private readonly IDepotReservations m_depot;

    public ServiceReservations(IDepotReservations depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    public Result<ReservationDto> TenterAjouter(int numero, string titre)
    {
        if (numero <= 0 || string.IsNullOrWhiteSpace(titre))
        {
            return Result<ReservationDto>.Echec("Numéro ou titre invalide.");
        }

        if (m_depot.Obtenir(numero) is not null)
        {
            return Result<ReservationDto>.Echec("Numéro déjà présent.");
        }

        Reservation reservation = new(numero, titre);
        m_depot.Ajouter(reservation);
        return Result<ReservationDto>.Succes(new ReservationDto(reservation));
    }

    public ReservationDto? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        Reservation? reservation = m_depot.Obtenir(numero);
        return reservation is null ? null : new ReservationDto(reservation);
    }
}
