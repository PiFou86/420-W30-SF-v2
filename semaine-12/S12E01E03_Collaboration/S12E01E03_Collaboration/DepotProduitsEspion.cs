namespace Collaboration;

// Point de départ pour une doublure manuelle. Le stockage des appels peut être étendu.
public sealed class DepotProduitsEspion : IDepotProduits
{
    public int? NumeroDemande { get; private set; }

    public Produit? Obtenir(int numero)
    {
        NumeroDemande = numero;
        return null;
    }

    public IReadOnlyList<Produit> ObtenirTous() => Array.Empty<Produit>();
    public IReadOnlyList<Produit> Rechercher(string fragment, int debut, int taille) => Array.Empty<Produit>();
}
