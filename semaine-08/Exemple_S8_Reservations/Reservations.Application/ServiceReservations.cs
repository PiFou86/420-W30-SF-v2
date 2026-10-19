using Microsoft.Extensions.Logging;
using Reservations.Domaine;
namespace Reservations.Application;

public sealed class ServiceReservations
{
    private readonly IDepotReservations m_depot;
    private readonly ILogger<ServiceReservations> m_journal;

    public ServiceReservations(
        IDepotReservations depot,
        ILogger<ServiceReservations> journal)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(journal);
        m_depot = depot;
        m_journal = journal;
    }

    public List<Reservation> Lire()
    {
        m_journal.LogInformation("Lecture des réservations.");
        return m_depot.Lire();
    }

    public Result<Reservation> Creer(int numero, string titre)
    {
        // Vérification du doublon avant la création, comme dans le parcours des diapos.
        if (m_depot.Obtenir(numero) is not null)
        {
            return Result<Reservation>.Echec("Ce numéro est déjà utilisé.");
        }
        Reservation reservation = new(numero, titre);
        m_depot.Ajouter(reservation);
        return Result<Reservation>.Succes(reservation);
    }
}
