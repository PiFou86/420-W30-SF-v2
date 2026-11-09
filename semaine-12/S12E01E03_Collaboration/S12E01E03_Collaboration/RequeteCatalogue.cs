namespace Collaboration;

public static class RequeteCatalogue
{
    public static IReadOnlyList<Produit> Rechercher(
        IEnumerable<Produit> produits, string fragment, int debut, int taille)
    {
        ArgumentNullException.ThrowIfNull(fragment);
        ArgumentOutOfRangeException.ThrowIfNegative(debut);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taille);
        return produits.Where(p => p.EnStock && p.Nom.Contains(fragment))
            .ToArray();
    }
}
