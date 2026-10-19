namespace Reservations.Domaine;

public sealed class ReservationIntrouvableException : Exception
{
    public int Numero { get; }

    public ReservationIntrouvableException(int numero)
        : base($"Réservation {numero} introuvable.")
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        Numero = numero;
    }
}
