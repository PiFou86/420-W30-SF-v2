using Commandes.Domaine;

namespace Commandes.Application;

public sealed class ProduitDto
{
    public int Numero { get; }
    public string Nom { get; }
    public decimal Prix { get; }

    public ProduitDto(int numero, string nom, decimal prix)
    {
        Numero = numero;
        Nom = nom;
        Prix = prix;
    }
}
public sealed class LigneCommandeDto
{
    public string Produit { get; }
    public int Quantite { get; }
    public decimal Montant { get; }

    public LigneCommandeDto(string produit, int quantite, decimal montant)
    {
        Produit = produit;
        Quantite = quantite;
        Montant = montant;
    }
}

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
        LigneCommandeDto[] lignes = commande.Lignes.Select(ligne =>
            new LigneCommandeDto(ligne.Produit.Nom, ligne.Quantite, ligne.Montant)).ToArray();
        Lignes = Array.AsReadOnly(lignes);
        Total = commande.Total;
    }
}
