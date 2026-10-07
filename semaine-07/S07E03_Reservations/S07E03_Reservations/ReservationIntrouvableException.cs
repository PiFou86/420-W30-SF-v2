namespace Reservations;

public sealed class ReservationIntrouvableException : Exception
{
    public int Numero { get; }

    public ReservationIntrouvableException(int numero)
        : base($"Réservation {numero} introuvable.")
    {
        // Exercice 1 : remplacer cette levée par la précondition et l'affectation.
        // numero <= 0 : ArgumentOutOfRangeException, paramètre numero.
        // numero > 0 : construction réussie, Numero reçoit numero.
        throw new NotImplementedException();
    }

    // Surcharge fournie : la cause n'est pas à réimplanter dans ce parcours.
    public ReservationIntrouvableException(int numero, Exception? cause)
        : base($"Réservation {numero} introuvable.", cause)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        Numero = numero;
    }
}
