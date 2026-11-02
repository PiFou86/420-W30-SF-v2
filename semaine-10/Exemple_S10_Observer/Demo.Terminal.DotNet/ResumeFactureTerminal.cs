using Commandes.Presentation.DotNet;

namespace Commandes.Terminal;

public sealed class ResumeFactureTerminal : IObserver<EtatCommandeDto>
{
    public void OnNext(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        Console.Out.WriteLine($"Facture : {etat.Facture.SousTotal:F2} + {etat.Facture.Frais:F2} = {etat.Facture.Total:F2}; provisoire : {etat.Facture.EstProvisoire}");
    }

    public void OnError(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);
        Console.Error.WriteLine("Les notifications sont interrompues.");
    }

    public void OnCompleted()
    {
        Console.Out.WriteLine("Notifications terminées.");
    }

}
