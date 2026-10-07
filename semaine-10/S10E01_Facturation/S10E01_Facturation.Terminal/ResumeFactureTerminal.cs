using Commandes.Presentation;

namespace Commandes.Terminal;

public sealed class ResumeFactureTerminal : IObservateurCommande
{
    public void Actualiser(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        Console.Out.WriteLine($"Facture : {etat.Facture.SousTotal:F2} + {etat.Facture.Frais:F2} = {etat.Facture.Total:F2}; provisoire : {etat.Facture.EstProvisoire}");
    }
}
