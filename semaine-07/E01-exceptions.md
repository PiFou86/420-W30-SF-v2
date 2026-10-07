# Exercice 1

## Mission - 15 minutes

Prédire une exécution et compléter une exception personnalisée simple dans le [départ](S07E03_Reservations/README.md). La classe Reservation, la méthode Tracer de la classe ParcoursExceptions et la surcharge d'exception avec une cause sont fournies; ne pas les réécrire.

## Travail essentiel

1. Sans exécuter, prédire la trace de la méthode Tracer avec les numéros 17 et 0. Lancer ensuite le Terminal avec `--exceptions`; expliquer pourquoi « créée » manque en cas d'échec et pourquoi finally n'est pas un catch.
2. Dans la bibliothèque principale, compléter **seulement le constructeur à un paramètre** de la classe ReservationIntrouvableException. Le message et l'appel à base sont déjà fournis : refuser un numéro non positif avec ArgumentOutOfRangeException, puis conserver le numéro dans la propriété Numero.
3. Dans le projet Tests, ajouter deux cas : le numéro 17 est conservé; le numéro 0 est refusé avec le paramètre `numero`. Construire directement les objets. En une phrase, distinguer une recherche ordinaire absente, représentée par null, d'un objet annoncé connu mais manquant, signalé par ce type personnalisé dans le parcours strict fourni.

Point de contrôle : prédiction vérifiée et deux nouveaux tests réussis. Les quatre cas fournis sur Reservation restent inchangés. L'écriture de la trace, la gestion d'InnerException et les tests supplémentaires des autres valeurs ne sont pas demandés dans le parcours allégé.

```bash
dotnet run --project S07E03_Reservations.Terminal -- --exceptions
dotnet test S07E03_Reservations.slnx
```

Exécuter depuis le dossier de la solution.

<details>
<summary>Rappel : tests AAA</summary>

[Semaine 2](../semaine-02/README.md). API : Assert.Throws<T>, Assert.Equal, ParamName et nameof. Quel invariant protège le numéro positif?

</details>
