namespace Commandes.Presentation;

// Comparaison de mécanisme après la version explicite : à compléter.
public sealed class SignalCommande
{
    public void Publier(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        throw new NotImplementedException("Comparer avec un événement C#.");
    }
}
