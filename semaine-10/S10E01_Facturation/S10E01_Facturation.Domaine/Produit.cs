namespace Commandes.Domaine;

public sealed record Produit
{
    public int Numero { get; }
    public string Nom { get; }
    public decimal Prix { get; }

    public Produit(int numero, string nom, decimal prix)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        if (prix < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(prix));
        }

        Numero = numero;
        Nom = nom;
        Prix = prix;
    }
}
