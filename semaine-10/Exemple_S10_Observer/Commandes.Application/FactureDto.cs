namespace Commandes.Application;

public sealed class FactureDto
{
    public int NumeroCommande { get; }
    public decimal SousTotal { get; }
    public decimal Frais { get; }
    public decimal Total { get; }
    public bool EstProvisoire { get; }

    public FactureDto(int numeroCommande, decimal sousTotal, decimal frais, decimal total, bool estProvisoire)
    {
        NumeroCommande = numeroCommande;
        SousTotal = sousTotal;
        Frais = frais;
        Total = total;
        EstProvisoire = estProvisoire;
    }
}
