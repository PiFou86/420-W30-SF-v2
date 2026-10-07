namespace Commandes.Application;

public sealed record FactureDto(int NumeroCommande, decimal SousTotal,
    decimal Frais, decimal Total, bool EstProvisoire);
