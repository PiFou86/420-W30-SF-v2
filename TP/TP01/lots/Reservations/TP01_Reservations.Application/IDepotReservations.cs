using Reservations.Domaine;

namespace Reservations.Application;

public interface IDepotReservations
{
    void Ajouter(Reservation element);
    Reservation? Obtenir(int numero);
    IReadOnlyCollection<Reservation> ObtenirToutes();
}
