using Commandes.Presentation;

namespace Commandes.Terminal;

public sealed class VueTerminal : IVueCommande
{
    public void Actualiser(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        Console.Out.WriteLine($"Commande {etat.Commande.Numero} : {etat.Commande.Total:F2}");
    }

    public void PresenterRefus(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Console.Out.WriteLine($"Refus : {message}");
    }

    public void PresenterIncident(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Console.Error.WriteLine(message);
    }
}
