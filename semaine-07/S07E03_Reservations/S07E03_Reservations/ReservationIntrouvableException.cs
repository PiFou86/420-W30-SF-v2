namespace Reservations;

public sealed class ReservationIntrouvableException : Exception
{
    public int Numero { get; }

    public ReservationIntrouvableException(int numero)
        : base($"Réservation {numero} introuvable.")
    {
        // Exercice 1 : vérifier le numéro positif et conserver Numero.
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
