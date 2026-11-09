# Exercice 3

**Joindre, afficher par catégorie et paginer**

Dans [la même solution](S11E01E03_Catalogue/S11E01E03_Catalogue.slnx), créer les modèles de résultat dans le projet principal et afficher les résultats depuis `Program.cs` du projet Terminal. Générer et exécuter la solution après les changements.

## Deux vues du catalogue

Joindre les produits et les catégories sur leur code. Pour chaque ligne, produire le numéro, le nom, le prix et le nom de la catégorie. Inclure les produits en rupture dans cet essai.

- La jointure intérieure retourne **13 lignes**. Le Jus (3) est absent et le Café filtre (10) apparaît dans « Boissons ».
- La version qui conserve les produits sans catégorie retourne **14 lignes**. Le Jus (3) affiche « Non renseigné ». Les deux produits de code `CAF` apparaissent chacun une fois puisque la jointure se fait sur la clé de catégorie.

## Affichage par catégorie

À partir de la jointure qui conserve tous les produits, afficher le **nom de la catégorie** comme en-tête, puis le numéro et le nom de chaque produit du groupe. Dans chaque groupe, classer les produits par numéro croissant.

| Catégorie affichée | Numéros attendus |
| --- | --- |
| Boissons | 4, 5, 9, 10, 11, 12 |
| Desserts | 6, 8, 13, 14 |
| Non renseigné | 3 |
| Plats | 1, 2, 7 |

## Une page, deux tris

Construire une consultation qui conserve uniquement les produits disponibles, trie les candidats, puis extrait une page avec `Skip` et `Take`. Recevoir le tri en paramètre sous forme de `Func` et choisir la lambda depuis le Terminal. Prévoir un tri par prix croissant et un autre par stock décroissant. Dans les deux cas, départager les égalités par numéro croissant. Enrichir les lignes avec leur catégorie, conserver la ligne qui manque de référence et matérialiser le résultat avant de le retourner.

`debut` compte les éléments sautés à partir de zéro. Refuser un début négatif et une taille inférieure à 1.

| Début (taille de 5) | Tri par prix | Tri par stock |
| ---: | --- | --- |
| 0 | 9, 6, 3, 4, 5 | 4, 6, 5, 2, 9 |
| 5 | 10, 11, 12, 13, 14 | 7, 10, 11, 12, 13 |
| 10 | 2, 7 | 14, 3 |
| 15 | Page vide | Page vide |

Choisir un scénario, au besoin avec un autre début ou une autre taille, qui échouerait si le filtre était appliqué après la pagination. Montrer le résultat attendu et les numéros incorrects que produirait cet ordre.

<!-- ## Prolongement facultatif — Bilan par catégorie

À réaliser après le bloc de 3 h ou si vous terminez plus tôt. Prévoir **15 à 30 min**.

Pour les produits en stock qui coûtent au plus 3,00 $, dresser un bilan pour **chaque catégorie connue**, même quand elle n’a aucun produit admissible. Afficher le nombre de produits, les unités en stock et leur valeur totale. La valeur d’un produit est son prix unitaire multiplié par son stock. Le produit sans référence de catégorie reste visible dans la consultation paginée.

| Catégorie | Produits | Unités | Valeur du stock |
| --- | ---: | ---: | ---: |
| Boissons | 6 | 26 | 66,00 $ |
| Plats | 0 | 0 | 0,00 $ |
| Desserts | 3 | 12 | 32,00 $ |

<details>
<summary>Repères</summary>

Comparer `Join` et `GroupJoin`. Pour le bilan, déterminer quelle liste doit servir de point de départ afin de conserver les catégories sans produit admissible. `Sum` calcule les totaux.

</details> -->
