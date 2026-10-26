namespace Commandes.Application;

public sealed class ProduitDto
{
    public int Numero { get; }
    public string Nom { get; }
    public decimal Prix { get; }

    public ProduitDto(int numero, string nom, decimal prix)
    {
        Numero = numero;
        Nom = nom;
        Prix = prix;
    }
}
