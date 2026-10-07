using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

public sealed class DepotReservationsMemoire : IDepotReservations
{
    // TODO : choisir une collection privée et protéger ses accès.
    public void Ajouter(Reservation element)
    {
        throw new NotImplementedException("Implanter l'ajout et son contrat.");
    }

    public Reservation? Obtenir(int numero)
    {
        throw new NotImplementedException("Implanter la recherche et son contrat.");
    }

    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        throw new NotImplementedException("Implanter la copie de collection.");
    }
}
