using Commandes.Presentation;

namespace Commandes.Tests;

internal sealed class VueEspionne : IObserver<ResultatConsultation>
{
    public List<ResultatConsultation> Notifications { get; } = new List<ResultatConsultation>();

    public void OnNext(ResultatConsultation resultat)
    {
        Notifications.Add(resultat);
    }

    public void OnError(Exception erreur)
    {
        throw new InvalidOperationException("Fin du flux inattendue.", erreur);
    }

    public void OnCompleted()
    {
        throw new InvalidOperationException("Fin du flux inattendue.");
    }
}
