using Reservations.Domaine;
namespace Reservations.Infrastructure;

public class ConvertisseurReservations
{
    public ReservationDonnees VersDto(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        return new ReservationDonnees(reservation);
    }

    public Reservation VersEntite(ReservationDonnees dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return new Reservation(dto.Numero, dto.Titre!);
    }

    public List<Reservation> VersEntites(IEnumerable<ReservationDonnees?>? dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);
        return dtos.Select(dto => VersEntite(dto!)).ToList();
    }
}
