using Reservations.Domaine;
namespace Reservations.Application;

public interface IDepotReservations
{
    List<Reservation> Lire();
    Reservation? Obtenir(int numero);
    void Ajouter(Reservation reservation);
}
