namespace Reservations.Domaine;

public sealed class Reservation
{
    public int Numero { get; }
    public string Titre { get; }

    public Reservation(int numero, string titre)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero);
        ArgumentException.ThrowIfNullOrWhiteSpace(titre);
        Numero = numero;
        Titre = titre;
    }
}
