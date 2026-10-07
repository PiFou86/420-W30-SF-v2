namespace Reservations;

public sealed class DepotReservationsMemoire : IDepotReservations
{
    public void Ajouter(Reservation reservation)
    {
        throw new NotImplementedException();
    }
    public Reservation? Obtenir(int numero)
    {
        throw new NotImplementedException();
    }
    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        throw new NotImplementedException();
    }
}
