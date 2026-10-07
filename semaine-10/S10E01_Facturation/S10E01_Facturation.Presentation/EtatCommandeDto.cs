using Commandes.Application;

namespace Commandes.Presentation;

// Instantané de sortie : les objets contenus sont aussi immuables.
public sealed class EtatCommandeDto
{
    public CommandeDto Commande { get; }
    public FactureDto Facture { get; }

    public EtatCommandeDto(CommandeDto commande, FactureDto facture)
    {
        ArgumentNullException.ThrowIfNull(commande);
        ArgumentNullException.ThrowIfNull(facture);
        Commande = commande;
        Facture = facture;
    }
}
