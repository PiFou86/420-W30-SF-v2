namespace Commandes.Presentation;

public interface IObservateurCommande
{
    void Actualiser(EtatCommandeDto etat);
}
