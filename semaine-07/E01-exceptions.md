# Exercice 1

## Mission et durée

Environ 40 minutes. Pour chaque situation, décidez si une précondition, un refus métier attendu ou une erreur technique est en jeu. Écrivez ensuite les éléments C# demandés.

## Situation

Une réservation possède un numéro strictement positif et un titre non vide. Une tentative de réservation peut être refusée parce qu'une plage horaire est déjà occupée. Plus tard, un dépôt fichier pourrait échouer à la lecture.

## Travail demandé

1. Sur papier ou dans un fichier Markdown, écrivez le constructeur de la classe `Reservation` : il refuse `numero <= 0` avec `ArgumentOutOfRangeException` et un titre vide avec `ArgumentException`.
2. Dans un service qui tente une réservation, expliquez pourquoi « plage déjà occupée » est un refus attendu plutôt qu'une exception technique. Donnez une signature possible utilisant `Result<Reservation>` ou un résultat équivalent.
3. Créez une exception `ReservationIntrouvableException` qui conserve le numéro demandé dans une propriété en lecture seule. Expliquez dans quel cas ce type apporte plus qu'une exception standard; ne l'utilisez pas pour chaque refus normal.
4. Décrivez au moins un cas de test pour chaque précondition du constructeur et un cas pour la propriété `Numero` de l'exception personnalisée. Ces tests seront codés dans le projet de tests de l'exercice 3, avec les classes `ReservationTests` et `ReservationIntrouvableExceptionTests`.
5. Précisez où vous intercepteriez une erreur de lecture technique et ce que vous éviteriez d'afficher à l'utilisateur. L'implantation du dépôt fichier attend la semaine 8.

Point de contrôle : vous devez avoir un extrait de constructeur, un extrait d'exception et trois cas de test nommés. Aucun projet .NET à créer à cette étape.

<details>
<summary>Rappel des semaines précédentes</summary>

Revoir les [tests AAA de la semaine 2](../semaine-02/README.md). API utiles : `Assert.Throws<T>`, `ArgumentException.ThrowIfNullOrWhiteSpace`. La valeur rejetée empêche-t-elle un objet invalide d'exister?

</details>
