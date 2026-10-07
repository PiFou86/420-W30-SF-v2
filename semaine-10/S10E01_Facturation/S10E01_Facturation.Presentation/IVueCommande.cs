namespace Commandes.Presentation;

public interface IVueCommande : IObservateurCommande
{
    void PresenterRefus(string message);
    void PresenterIncident(string message);
}
