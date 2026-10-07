namespace Reservations.Application;

public sealed class CreerReservation
{
    private readonly IDepotReservations m_depotReservations;

    public CreerReservation(IDepotReservations depotReservations)
    {
        ArgumentNullException.ThrowIfNull(depotReservations);
        m_depotReservations = depotReservations;
    }

    // TODO : détecter les conflits par le port, puis enregistrer la réservation.
}
