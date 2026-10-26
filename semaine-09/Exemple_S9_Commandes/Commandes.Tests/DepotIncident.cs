using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;

namespace Commandes.Tests;

internal sealed class DepotIncident : IDepotCommandes
{
    private readonly DepotCommandesMemoire m_depot = new DepotCommandesMemoire();
    public bool EchouerLecture { get; set; }
    public bool EchouerAjout { get; set; }

    public void Ajouter(Commande commande)
    {
        if (EchouerAjout)
        {
            throw new IOException("Incident d'écriture simulé.");
        }

        m_depot.Ajouter(commande);
    }

    public IReadOnlyCollection<Commande> ObtenirToutes()
    {
        if (EchouerLecture)
        {
            throw new IOException("Incident de lecture simulé.");
        }

        return m_depot.ObtenirToutes();
    }

    public Commande? Obtenir(int numero)
    {
        if (EchouerLecture)
        {
            throw new IOException("Incident de lecture simulé.");
        }

        return m_depot.Obtenir(numero);
    }
}
