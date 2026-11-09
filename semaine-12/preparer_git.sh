#!/usr/bin/env bash
set -euo pipefail

destination="${1:-bac-a-sable-s12}"
if [[ -e "$destination" ]]; then
    printf 'Choisir un nouveau dossier de destination.\n' >&2
    exit 1
fi

source_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
mkdir -p "$destination"
cp -R "$source_dir/S12E01E03_Collaboration" "$destination/"
cd "$destination"
find . -type d \( -name bin -o -name obj \) -prune -exec rm -r {} +
git init -q -b dev
git config user.name "Atelier S12"
git config user.email "atelier@example.invalid"
printf '**/bin/\n**/obj/\n' > .gitignore

requete="S12E01E03_Collaboration/S12E01E03_Collaboration/RequeteCatalogue.cs"
ecrire_initial() {
    cat > "$requete" <<'CS'
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
CS
}

ecrire_dev() {
    cat > "$requete" <<'CS'
namespace Collaboration;

public static class RequeteCatalogue
{
    public static IReadOnlyList<Produit> Rechercher(
        IEnumerable<Produit> produits, string fragment, int debut, int taille)
    {
        ArgumentNullException.ThrowIfNull(fragment);
        ArgumentOutOfRangeException.ThrowIfNegative(debut);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taille);
        return produits.Where(p => p.EnStock && p.Nom.Contains(fragment, StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }
}
CS
}

ecrire_feature() {
    cat > "$requete" <<'CS'
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
            .OrderBy(p => p.Numero).Skip(debut).Take(taille).ToArray();
    }
}
CS
}

ecrire_review() {
    cat > "$requete" <<'CS'
namespace Collaboration;

public static class RequeteCatalogue
{
    public static IReadOnlyList<Produit> Rechercher(
        IEnumerable<Produit> produits, string fragment, int debut, int taille)
    {
        ArgumentNullException.ThrowIfNull(fragment);
        ArgumentOutOfRangeException.ThrowIfNegative(debut);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taille);
        return produits.OrderBy(p => p.Numero).Skip(debut).Take(taille)
            .Where(p => p.EnStock && p.Nom.Contains(fragment, StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }
}
CS
}

ecrire_initial
git add .
git commit -qm "Catalogue de départ"
git branch fonctionnalite/pagination
ecrire_dev
git add "$requete"
git commit -qm "Rechercher sans distinction de casse"
git switch -q fonctionnalite/pagination
ecrire_feature
git add "$requete"
git commit -qm "Paginer dans un ordre stable"
git switch -qc revue/pagination dev
ecrire_review
git add "$requete"
git commit -qm "Ajouter une page au catalogue"
git switch -q dev
printf 'Branches prêtes dans %s\n' "$PWD"
