using Commandes.Domaine;

namespace Commandes.Application;

public sealed record ProduitDto(int Numero, string Nom, decimal Prix);
public sealed record LigneCommandeDto(string Produit, int Quantite, decimal Montant);

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
        Lignes = Array.AsReadOnly(commande.Lignes.Select(ligne => new LigneCommandeDto(
            ligne.Produit.Nom, ligne.Quantite, ligne.Montant)).ToArray());
        Total = commande.Total;
    }
}
