# Exercice 1

## But et durée - 15 minutes

Prédire le parcours d'une exception standard, puis compléter le constructeur d'une exception personnalisée. Utiliser la solution existante [S07E03_Reservations.slnx](S07E03_Reservations/S07E03_Reservations.slnx), partagée par les trois exercices.

## Fichiers à utiliser

Les chemins de ce tableau partent du dossier de solution `semaine-07/S07E03_Reservations/`.

| Fichier | Action |
| --- | --- |
| `DECISIONS.md` | Remplir la section Exercice 1 |
| `S07E03_Reservations/ParcoursExceptions.cs` | Lire la méthode Tracer, sans la modifier |
| `S07E03_Reservations/ReservationIntrouvableException.cs` | Modifier le constructeur qui reçoit seulement `int numero` |
| `S07E03_Reservations.Tests/ReservationIntrouvableExceptionTests.cs` | Créer ce fichier pour les deux nouveaux tests |

La classe Reservation, la trace, le Terminal et le constructeur `ReservationIntrouvableException(int numero, Exception? cause)` sont fournis.

## 1. Prédire, puis comparer

Dans les deux lignes du tableau Exercice 1 de `DECISIONS.md`, écrire **avant le lancement** la liste ordonnée des libellés que la méthode `ParcoursExceptions.Tracer` ajoutera pour 17, puis pour 0. Il s'agit des libellés de la liste retournée, sans les préfixes « Trace valide » et « Trace invalide » du Terminal.

Lancer ensuite le mode `--exceptions`, puis remplir la colonne « Observé ». Sous le tableau, répondre en une phrase à chacune des deux questions :

- Pourquoi le libellé « créée » n'est-il pas ajouté quand le numéro vaut 0?
- Quelle différence entre le rôle de catch et celui de finally?

Ce mode appelle le constructeur de **Reservation** et montre une exception standard. Il fonctionne déjà dans le départ et ne vérifie pas votre exception personnalisée.

## 2. Compléter un seul constructeur

Dans `ReservationIntrouvableException.cs`, modifier uniquement le corps du constructeur `ReservationIntrouvableException(int numero)` :

- remplacer sa ligne `throw new NotImplementedException();`;
- si `numero <= 0`, lancer **ArgumentOutOfRangeException** en indiquant le nom du paramètre `numero`;
- si `numero > 0`, affecter cette valeur à la propriété **Numero**. La construction doit alors réussir.

Conserver la signature, la propriété, le message et l'appel à base déjà fournis. Ne pas modifier le constructeur à deux paramètres.

Un numéro valide permet de **créer l'objet exception**; le constructeur ne doit pas lancer lui-même ReservationIntrouvableException. Le code appelant pourra lancer cet objet lorsqu'il signale une anomalie.

## 3. Écrire deux tests

Créer la classe `ReservationIntrouvableExceptionTests` dans le fichier de tests indiqué. Reprendre les imports `Reservations` et `Xunit`, l'espace de noms et la structure `[Fact]`/AAA du fichier fourni `ReservationFournieTests.cs`. Construire les objets directement.

| Cas | Action à tester | Vérification attendue |
| --- | --- | --- |
| Numéro valide | Construire `ReservationIntrouvableException` avec 17 | La construction réussit et sa propriété `Numero` vaut 17 |
| Numéro invalide | Construire **la même classe** avec 0 | `Assert.Throws<ArgumentOutOfRangeException>` renvoie l'erreur levée; sa propriété `ParamName` vaut la chaîne `"numero"` |

`ParamName` est la propriété de l'erreur ArgumentOutOfRangeException capturée; ce n'est ni le message affiché ni la propriété Numero de votre type personnalisé. Ne pas retester ici le constructeur de Reservation.

Dans `DECISIONS.md`, compléter aussi la phrase qui distingue une recherche normalement absente d'un objet annoncé connu mais manquant. La classe ConsultationReservations et son parcours strict Exiger sont fournis; aucune implantation de ce client n'est demandée.

## Vérifier et terminer

Depuis le dossier contenant `S07E03_Reservations.slnx` :

```bash
dotnet run --project S07E03_Reservations.Terminal -- --exceptions
dotnet test S07E03_Reservations.slnx --filter FullyQualifiedName~ReservationIntrouvableExceptionTests
```

Terminé lorsque la section Exercice 1 est renseignée et que les **deux nouveaux tests** passent. Un résultat de quatre cas lors du lancement de la suite complète correspond aux tests déjà fournis, pas aux tests demandés ici. Les traces et la gestion de la cause ne sont pas à réécrire.

<details>
<summary>Rappel : tests AAA</summary>

[Tests de la semaine 2](../semaine-02/README.md). API : Assert.Throws<T>, Assert.Equal, ParamName et nameof. Le nom du paramètre dans le code se distingue de sa valeur lors de l'appel.

</details>
