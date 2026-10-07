namespace Reservations;

public sealed class DepotReservationsMemoire : IDepotReservations
{
    // Exercice 3 : ajouter ici le dictionnaire privé d'instance, initialement vide.
    public void Ajouter(Reservation reservation)
    {
        // Exercice 3 : stocker, refuser null ou doublon sans remplacer.
        throw new NotImplementedException();
    }
    public Reservation? Obtenir(int numero)
    {
        // Exercice 3 : refuser numero <= 0; objet stocké ou null si absent.
        throw new NotImplementedException();
    }
    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        // Exercice 3 : retourner un nouveau tableau de copie Reservation[].
        throw new NotImplementedException();
    }
}
