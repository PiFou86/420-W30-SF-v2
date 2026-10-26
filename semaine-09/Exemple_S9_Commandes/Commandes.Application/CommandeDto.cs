using Commandes.Domaine;

namespace Commandes.Application;

public sealed class CommandeDto
{
    public int Numero { get; }
    public bool EstEnregistree { get; }
    public IReadOnlyCollection<LigneCommandeDto> Lignes { get; }
    public decimal Total { get; }

    public CommandeDto(Commande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);
        Numero = commande.Numero;
        EstEnregistree = commande.EstConfirmee;
        List<LigneCommandeDto> lignes = new List<LigneCommandeDto>();
        foreach (LigneCommande ligne in commande.Lignes)
        {
            LigneCommandeDto dto = new LigneCommandeDto(ligne.Produit.Nom,
                ligne.Quantite, ligne.Produit.Prix, ligne.Montant);
            lignes.Add(dto);
        }

        Lignes = lignes.AsReadOnly();
        Total = commande.Total;
    }
}
