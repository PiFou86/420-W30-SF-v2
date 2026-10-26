using Commandes.Application;

namespace Commandes.Portee.WinForms;

public interface IFabriqueSaisieCommande
{
    // Retourne seulement un instantané enregistré, ou null après une annulation.
    CommandeDto? Ouvrir(Form proprietaire, int numeroPropose);
}
