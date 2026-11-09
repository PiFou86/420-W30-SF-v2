# Exercice 2

**Répondre à des questions de catalogue**

Reprendre [la même solution](S11E01E03_Catalogue/S11E01E03_Catalogue.slnx). Ajouter les consultations dans le projet principal et les appeler depuis `Program.cs` du projet Terminal. Générer et lancer la solution. Les numéros dans les résultats permettent de vérifier à la fois les produits retenus et leur ordre.

## Disponibilité et budget

| Demande | Résultat attendu |
| --- | --- |
| Projeter les noms des produits en stock dans l’ordre de la source, puis compter les produits disponibles | 12 produits. Numéros correspondants dans l’ordre · 9, 5, 7, 4, 2, 6, 3, 10, 11, 12, 13, 14. |
| Déterminer s’il y a une rupture, puis identifier les produits concernés par numéro croissant | Oui. Numéros 1 et 8. |
| Produire une liste de réapprovisionnement pour les produits dont il reste 1 ou 2 unités, par numéro croissant | 3, 7, 10, 11, 12, 13, 14. |
| Proposer les boissons dont le prix est supérieur à 0 $ et au plus 3 $, avec au moins 2 unités en stock. Classer par stock décroissant, puis par numéro croissant. | 4, 5, 10, 11, 12. |

Utiliser `Where` et `Select` pour obtenir les listes. Utiliser `Count` pour le nombre et `Any` pour l’alerte.

## Préfixe, tri et projection

Interroger le catalogue complet, y compris les produits en rupture. Utiliser `Select` pour produire les champs à afficher.

| Demande | Résultat attendu |
| --- | --- |
| Lister les produits dont le code débute par `CA`, par stock décroissant puis numéro croissant. Afficher le numéro, le nom et les unités en stock. | 4 Café (9 unités), 10 Café filtre (2), 11 Café expresso (2), 12 Café cappuccino (2). |
| Lister les produits dont le prix unitaire est de 3,00 $ ou plus, par prix décroissant puis numéro croissant. Afficher le numéro, le nom et le prix. | 12 produits. Numéros dans l’ordre · 7, 2, 1, 8, 3, 4, 5, 10, 11, 12, 13, 14. |
| Compter les produits de type café | 4 produits. Pour ce catalogue, le type café correspond au préfixe `CA`. Les deux produits de code `CAF` comptent séparément. |

Utiliser `StartsWith` pour le préfixe et `Count` pour le décompte des cafés.

## Les cinq stocks de plus grande valeur

Pour chaque produit, la valeur du stock est le prix unitaire multiplié par la quantité en stock. Projeter le numéro, le nom et cette valeur dans un résultat de consultation. Classer par valeur décroissante puis numéro croissant et conserver les cinq premières lignes.

| Numéro | Produit | Valeur du stock |
| ---: | --- | ---: |
| 2 | Sandwich | 36,00 $ |
| 4 | Café | 27,00 $ |
| 7 | Bol | 24,00 $ |
| 5 | Thé | 21,00 $ |
| 6 | Biscuit | 20,00 $ |

## Code unique et premier candidat

Une recherche par code est censée trouver au plus un produit. Comparer `Single` et `SingleOrDefault` avec le catalogue fourni.

| Code demandé | `Single` | `SingleOrDefault` |
| --- | --- | --- |
| `EAU` | Produit 9 | Produit 9 |
| `ABSENT` | `InvalidOperationException` | `null` |
| `CAF` | `InvalidOperationException` | `InvalidOperationException` |

Pour choisir un produit disponible dans une catégorie, classer par prix croissant puis par numéro croissant. Comparer `First` et `FirstOrDefault` sur les mêmes candidats.

| Candidats | `First` | `FirstOrDefault` |
| --- | --- | --- |
| Catégorie inexistante | `InvalidOperationException` | `null` |
| Catégorie `X` | Jus (3) | Jus (3) |
| Catégorie `B` | Eau (9) | Eau (9) |
| Boissons de la sélection budget ci-dessus | Café (4) | Café (4) |

Expliquer pourquoi prendre le premier des deux produits `CAF` masquerait un problème pour une recherche par code unique.

## Audit des codes

Produire la liste des codes répétés. Pour chacun, afficher son nombre d’occurrences et les numéros des produits concernés, en ordre croissant. Le catalogue fourni donne **`CAF`, 2 occurrences, produits 4 et 10**. Vérifier aussi une séquence qui contient seulement les produits 4 et 9. Elle ne doit signaler aucun code répété.

<details>
<summary>Repère</summary>

Un regroupement par code avec `GroupBy` permet de vérifier la taille de chaque groupe.

</details>
