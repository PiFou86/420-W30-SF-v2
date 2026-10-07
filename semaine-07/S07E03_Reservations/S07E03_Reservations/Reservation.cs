namespace Reservations;

public sealed class Reservation
{
    public int Numero { get; }
    public string Titre { get; }

    public Reservation(int numero, string titre)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(titre);
        Numero = numero;
        Titre = titre;
    }
}
