using Commandes.Application;
using Commandes.Domaine;

namespace Commandes.Tests;

internal sealed class DepotMemoire : IDepotCommandes
{
    public int DernierIdentifiant { get; private set; }

    public Commande? Obtenir(int identifiant)
    {
        DernierIdentifiant = identifiant;
        return new Commande(identifiant, new LigneCommande[] { new LigneCommande(4m, 2) });
    }
}
