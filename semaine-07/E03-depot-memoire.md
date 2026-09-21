# Exercice 3

## Mission et durée

Environ 75 minutes. Transformez le catalogue de l'exercice 2 en un dépôt de réservations dont le contrat ne révèle pas la collection choisie.

## Travail demandé

1. Créez la solution `S07E03_Reservations` avec le projet principal `S07E03_Reservations`, le projet `S07E03_Reservations.Terminal` et le projet `S07E03_Reservations.Tests`, tous ciblant .NET 10. Les projets Terminal et Tests référencent le projet principal; celui-ci ne référence aucun des deux.
2. Placez la classe `Reservation` dans le projet principal. Implantez les préconditions et les cas de test préparés à l'exercice 1.
3. Définissez l'interface `IDepotReservations` avec `Ajouter(Reservation reservation)`, `Obtenir(int numero)` et `ObtenirToutes()`. Documentez le refus des doublons et le retour `null` pour une absence.
4. À partir de l'esquisse de `CatalogueReservations` réalisée à l'exercice 2, créez la classe `DepotReservationsMemoire` et faites-lui implanter l'interface. Reprenez les préconditions, le refus des doublons et la protection de la collection décidés plus tôt.
5. Dans la méthode statique `Main` de la classe `Program` du projet Terminal, créez explicitement le dépôt et deux réservations, puis affichez le résultat avec `Console.Out.WriteLine`. Aucune instruction de haut niveau.
6. Dans le projet de tests, codez les cas préparés en `ReservationTests`, `ReservationIntrouvableExceptionTests` et `DepotReservationsMemoireTests`. Vérifiez les préconditions, doublons, recherche et copie de collection. Utilisez de vrais objets, sans Moq ni conteneur.
7. Dans `DECISIONS.md`, expliquez pourquoi le client dépend de `IDepotReservations` et pourquoi une interface générique `IRepository<T>` serait prématurée ici.

Point de contrôle : `dotnet test S07E03_Reservations.slnx` doit réussir. La sérialisation JSON/YAML n'est pas demandée cette semaine.

<details>
<summary>Rappel des semaines précédentes</summary>

Revoir l'[injection et le point de composition en semaine 3](../semaine-03/README.md). Types/API utiles : interface, constructeur, `Dictionary<int, Reservation>`. Quel objet décide de l'implantation concrète, et quel objet se limite au contrat?

</details>
