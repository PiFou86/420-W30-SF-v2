namespace Reservations;

public sealed class ConsultationReservations
{
    private readonly IDepotReservations m_depot;

    public ConsultationReservations(IDepotReservations depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    // Contrat strict pour un objet annoncé connu, pas une recherche ordinaire.
    public Reservation Exiger(int numero)
    {
        return m_depot.Obtenir(numero)
            ?? throw new ReservationIntrouvableException(numero);
    }
}
