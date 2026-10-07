namespace Reservations;

public interface IDepotReservations
{
    void Ajouter(Reservation reservation);
    Reservation? Obtenir(int numero);
    IReadOnlyCollection<Reservation> ObtenirToutes();
}
