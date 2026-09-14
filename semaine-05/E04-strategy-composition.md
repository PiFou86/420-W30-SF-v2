# Exercice 4

> [!IMPORTANT]
> **Évaluation individuelle — 2,5 %. Niveau 0 : zéro IA.** La génération de
> code, de tests ou de documentation par une IA est interdite.

Consultez la plateforme d'enseignement pour l'échéance et les modalités de remise.

## Mission et durée

Prévoyez de 90 à 100 minutes. Remplacez le calcul conditionnel des rabais par
des stratégies composables, puis démontrez le comportement avec des tests.

Le départ se trouve dans le répertoire de solution
`S05E04_Restaurant_Rabais`.

## Parcours Git obligatoire

1. Dans le dépôt Git de l’exercice, créez la branche d’intégration `dev` depuis
   la branche `main`, puis publiez `dev` sur GitHub.
2. Créez la branche de fonctionnalité
   `fonctionnalite/exercice-4-strategy` depuis la branche `dev`.
3. Dans cette branche de fonctionnalité, réalisez au moins deux commits Git
   cohérents.
4. Fusionnez la branche de fonctionnalité dans la branche `dev`, exécutez les
   tests de la solution, puis publiez `dev`.
5. Fusionnez la branche `dev` dans la branche `main`, exécutez de nouveau les
   tests, puis publiez `main`.

Revoyez les commandes Git de la semaine 3 au besoin.

## Travail demandé

1. Dans le projet principal `S05E04_Restaurant_Rabais`, créez une interface
   étroite nommée `IStrategieRabais`.
2. Dans le projet principal, créez trois classes qui implantent cette interface
   pour représenter un rabais nul, un rabais fidélité de 10 % et un rabais fixe
   de 5 $.
3. Un rabais ne peut jamais rendre le total négatif.
4. Ajoutez un paramètre de type `IStrategieRabais` au constructeur de la classe
   `CalculateurFacture` et refusez une valeur `null`.
   `CalculateurFacture` doit déléguer le calcul sans sélectionner une
   implantation concrète.
5. Ajoutez dans le projet principal une nouvelle classe de stratégie composée
   qui reçoit deux objets `IStrategieRabais` et les applique successivement,
   sans modifier les classes de stratégies existantes.
6. Dans le projet de tests, écrivez exactement deux méthodes de test :
   - un `[Theory]` pour les valeurs limites du rabais fixe;
   - un test du calculateur avec une stratégie contrôlée.
7. Complétez le fichier `DECISIONS.md` situé à la racine de la solution : rôles
   du patron Strategy, OCP, préférence pour la composition et résultat de
   `git log --oneline --graph --decorate --all`. Comme la solution de départ ne
   contient pas de projet Terminal, écrivez également dans `DECISIONS.md` un
   exemple de code que le fichier `Program.cs` d’un éventuel projet Terminal
   pourrait utiliser comme point de composition pour choisir et assembler la
   stratégie.

## Critères observables

- aucune sélection par chaîne de caractères dans la classe
  `CalculateurFacture`;
- la classe contexte délègue le calcul et le fichier `DECISIONS.md` situe le
  point de composition;
- interfaces étroites et substituables;
- normes C# du cours respectées;
- tests structurés avec Arranger, Agir et Auditer;
- branches Git `dev` et `main` testées après les fusions.
