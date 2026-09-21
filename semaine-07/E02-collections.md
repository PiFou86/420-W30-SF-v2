# Exercice 2

## Mission et durée

Environ 40 minutes. Choisissez des collections pour des besoins observables avant de coder.

## Travail demandé

1. Dans `CHOIX_COLLECTIONS.md`, associez une collection à chacun de ces besoins : retrouver une réservation par numéro; conserver des étiquettes uniques; traiter des demandes dans l'ordre d'arrivée; afficher un historique ordonné qui accepte les doublons. Justifiez chaque choix en une phrase.
2. Esquissez une classe `CatalogueReservations` avec une collection privée adaptée à la recherche par numéro. Précisez les contrats des méthodes `Ajouter(Reservation reservation)` et `Obtenir(int numero)` : refus de `null`, refus du doublon, refus du numéro non positif et retour `null` pour l'absence.
3. Précisez comment la méthode `ObtenirToutes()` protège la structure interne. Expliquez explicitement pourquoi elle ne rend pas les objets `Reservation` immuables.
4. Préparez une liste de cas de test pour l'ajout, le doublon, l'absence, les préconditions et la protection de la collection. Vous coderez ces tests dans le projet de tests de l'exercice 3, sous le nom `DepotReservationsMemoireTests` après avoir introduit l'interface.

Point de contrôle : votre choix de collection et vos contrats sont écrits dans `CHOIX_COLLECTIONS.md`; aucun deuxième projet .NET à créer.

<details>
<summary>Rappel des semaines précédentes</summary>

Revoir l'[encapsulation des collections en semaine 1](../semaine-01/README.md). API possibles : `Dictionary<TKey,TValue>`, `IReadOnlyCollection<T>`, `ToArray()`. Le client peut-il ajouter un élément sans passer par `CatalogueReservations.Ajouter`?

</details>
