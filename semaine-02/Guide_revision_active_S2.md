# Guide de révision active — Semaine 2

## Tests unitaires, AAA, TDD, doublures et Moq

Ce guide aide à transformer les techniques de test en décisions explicites. Commencez par prédire le comportement attendu et la preuve à observer. Consultez ensuite les exemples pour vérifier la structure et la précision de votre test.

## Capacités à vérifier

À la fin de votre étude, vous devriez pouvoir :

- structurer une solution avec un projet de production et un projet de tests;
- formuler le comportement observable d'un test;
- écrire un test avec Arranger, Agir et Auditer;
- choisir des cas normaux, frontières et invalides;
- tester une exception précise;
- expliquer le cycle rouge-vert-réusinage;
- choisir entre objet réel, doublure manuelle et Moq;
- distinguer la préparation d'un collaborateur de la vérification d'une interaction.

## Carte des notions

| Notion | Question centrale | Indice |
|---|---|---|
| Test unitaire | Quel comportement observable une petite unité doit-elle garantir? | Le test est rapide, isolé et précis. |
| AAA | Comment rendre l'intention du scénario visible? | Préparer, exécuter une action principale, auditer. |
| Test paramétré | Plusieurs données vérifient-elles la même règle? | Une théorie exécute le même raisonnement sur plusieurs cas. |
| TDD | Quel petit comportement pilote la prochaine modification? | Rouge, vert, puis réusinage. |
| Doublure | Quel collaborateur faut-il remplacer pour observer ou contrôler le test? | La doublure sert le test; elle n'est pas son sujet. |
| Moq | Quelle configuration ou interaction veut-on automatiser? | `Setup` prépare; `Verify` audite. |

## Repère 1 — Structure de la solution de tests

Le projet de tests référence le projet de production. Les bibliothèques de test et Moq appartiennent au projet de tests, pas au domaine.

### Questions de rappel actif

1. Quelle différence faites-vous entre `ProjectReference` et `PackageReference`?
2. Pourquoi Moq ne devrait-il pas être installé dans le projet de production?
3. Quelle commande compile tous les projets de la solution?
4. Quelle commande exécute les tests?

## Repère 2 — Un test vérifie un comportement observable

Avant d'écrire le test, complétez mentalement cette phrase :

> Lorsque la situation est donnée et que l'action se produit, quel résultat observable doit-on obtenir?

Un bon test ne reproduit pas l'algorithme interne. Il observe un résultat, un changement d'état ou une interaction qui fait partie du contrat utile.

### Diagnostic

Un test appelle trois méthodes du système dans la section Agir et vérifie seulement que le résultat n'est pas nul. Expliquez pourquoi l'intention du test reste ambiguë.

## Repère 3 — AAA et nom du test

- **Arranger** construit le sujet, les données et les collaborateurs.
- **Agir** exécute l'action principale du scénario.
- **Auditer** vérifie le comportement observable attendu.

Le nom `Methode_Cas_ResultatAttendu` raconte le scénario et facilite le diagnostic lorsqu'un test échoue.

### Questions de rappel actif

1. Quelle information devrait apparaître dans chacune des trois parties du nom?
2. Pourquoi Agir devrait-il normalement contenir une seule action principale?
3. Une assertion trop générale peut-elle laisser passer un mauvais comportement?

## Repère 4 — FIRST et choix des données

FIRST rappelle qu'un test utile devrait être rapide, isolé, reproductible, auto-validant et écrit au bon moment. Pour une règle numérique, choisissez au moins un cas normal, une frontière valide et un cas invalide.

Un `[Theory]` convient lorsque plusieurs jeux de données vérifient exactement la même règle. Utilisez `MemberData` lorsque les valeurs ne conviennent pas naturellement à `InlineData`, notamment pour certains `decimal` ou objets construits.

## Repère 5 — Tester une exception

Un test d'exception doit vérifier le type précis attendu. Pour une exception d'argument, `ParamName` permet aussi de confirmer quel paramètre a été refusé.

```csharp
ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
    () => new LigneCommande(produit, 0));

Assert.Equal("quantite", exception.ParamName);
```

Distinguez une situation anormale qui justifie une exception d'un refus métier attendu qui pourrait plutôt devenir un résultat d'opération.

## Repère 6 — Cycle TDD

1. **Rouge** : écrire un petit test qui échoue pour la bonne raison.
2. **Vert** : ajouter le minimum de code pour faire réussir ce scénario.
3. **Réusinage** : simplifier la conception pendant que tous les tests restent verts.

TDD ne signifie pas simplement ajouter des tests après avoir terminé l'implantation. Le prochain test influence la prochaine petite décision de conception.

## Repère 7 — Doubler un collaborateur

Conservez un objet réel lorsqu'il est simple, rapide et déterministe. Remplacez un collaborateur lorsqu'il rend le test lent, fragile, imprévisible ou difficile à observer.

Les rôles courants d'une doublure comprennent :

- fournir une réponse contrôlée;
- conserver un état simplifié;
- enregistrer un appel et ses arguments;
- vérifier qu'une interaction a eu lieu ou n'a pas eu lieu.

Ne doublez pas chaque objet. Trop de doublures couplent le test aux détails internes de l'implantation.

## Repère 8 — Simulacre manuel et Moq

Une doublure manuelle rend visibles l'interface implantée, les données enregistrées et les vérifications possibles. Moq automatise cette mécanique lorsque la configuration ou les interactions deviennent répétitives.

| Avec Moq | Rôle |
|---|---|
| `new Mock<T>()` | Créer la doublure configurable. |
| `.Object` | Fournir l'objet qui implante le contrat au système testé. |
| `Setup(...).Returns(...)` | Préparer une réponse. |
| `Verify(...)` | Auditer une interaction après l'action. |
| `Times.Once` ou `Times.Never` | Préciser le nombre d'appels attendu. |
| `VerifyNoOtherCalls()` | Refuser les interactions supplémentaires après les vérifications principales. |

### Questions de rappel actif

1. Quelle différence faites-vous entre `Setup` et `Verify`?
2. Quand une doublure manuelle est-elle plus claire que Moq?
3. Dans un scénario refusé, quelle absence d'interaction pourrait faire partie du comportement attendu?

## Confusions fréquentes

- Tester plusieurs comportements indépendants dans une même méthode.
- Vérifier l'algorithme interne plutôt que le contrat observable.
- Choisir des assertions trop vagues.
- Utiliser une théorie pour des scénarios qui n'ont pas la même règle.
- Oublier de confirmer l'échec rouge en TDD.
- Confondre le sujet du test avec sa doublure.
- Configurer une réponse avec `Setup` sans auditer l'effet important, ou appeler `Verify` avant Agir.

## Autoévaluation

- [ ] Je formule le comportement avant d'écrire le test.
- [ ] Je sépare clairement Arranger, Agir et Auditer.
- [ ] Je choisis des cas normaux, frontières et invalides.
- [ ] Je teste le type précis d'une exception et son paramètre lorsque pertinent.
- [ ] Je peux expliquer chaque étape du cycle TDD.
- [ ] Je garde réels les objets simples et déterministes.
- [ ] Je peux écrire une doublure manuelle courte.
- [ ] Je distingue `Setup`, `.Object` et `Verify` dans Moq.

## Lectures ciblées

- Chapitre 9 : tests unitaires, structure AAA, tests paramétrés, exceptions, TDD et doublures.

