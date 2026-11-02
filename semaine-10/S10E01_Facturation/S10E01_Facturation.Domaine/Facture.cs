namespace Commandes.Domaine;

// Frais de service fictifs de 10 %, choisis uniquement pour l’exemple.
public sealed class Facture
{
    private const decimal TauxFrais = 0.10m;
    public decimal SousTotal { get; }
    public decimal Frais { get; }
    public decimal Total { get; }

    public Facture(decimal sousTotal)
    {
        if (sousTotal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(sousTotal));
        }

        SousTotal = decimal.Round(sousTotal, 2, MidpointRounding.AwayFromZero);
        Frais = decimal.Round(SousTotal * TauxFrais, 2, MidpointRounding.AwayFromZero);
        Total = SousTotal + Frais;
    }
}
