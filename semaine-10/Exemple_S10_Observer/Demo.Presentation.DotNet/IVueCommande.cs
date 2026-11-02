namespace Commandes.Presentation.DotNet;

public interface IVueCommande : IObserver<EtatCommandeDto>
{
    void PresenterRefus(string message);
    void PresenterIncident(string message);
}
