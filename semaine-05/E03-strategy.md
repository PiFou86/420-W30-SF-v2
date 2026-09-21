# Exercice 3

## Mission et durée

En 55 minutes, rendez explicite le patron Strategy pour choisir la règle de
tarification d'une livraison.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/strategy-livraison` depuis la branche `dev`.

## Diagnostic avant de coder

Avant de modifier la classe `ServiceLivraisons`, notez dans le fichier
`DECISIONS.md` situé à la racine de la solution :

1. quelle cascade ou sélection rend la classe `ServiceLivraisons` instable;
2. quel comportement varie entre les types de tarification;
3. quelle responsabilité doit rester dans le contexte;
4. qui devrait choisir la stratégie concrète.

## Travail demandé

1. Dans le projet principal, reprenez l’interface `ICalculateurFrais` et les
   classes de calculateurs substituables de l’exercice 2 : l’interface devient
   le contrat Strategy et les classes deviennent les stratégies concrètes.
2. Ajoutez un paramètre de type `ICalculateurFrais` au constructeur de la
   classe `ServiceLivraisons` afin d’y injecter la stratégie choisie.
3. Dans la classe `ServiceLivraisons`, retirez la sélection par chaîne de
   caractères et la cascade conditionnelle. Sa méthode `CalculerFrais` doit
   déléguer le calcul à l’objet `ICalculateurFrais` reçu par le constructeur.
4. Conservez la méthode `[Theory]` de l’exercice 2 : elle prouve déjà que les
   calculateurs sont substituables et qu’ils produisent les frais attendus.
   Dans le projet de tests, ajoutez une seule nouvelle méthode de test du
   contexte avec une stratégie contrôlée. Elle doit prouver que
   `ServiceLivraisons` délègue le calcul au contrat reçu.
5. Dans le fichier `DECISIONS.md`, associez explicitement les rôles du patron
   aux classes et à l’interface du projet : contexte, contrat Strategy,
   stratégies concrètes et point de composition.
6. Expliquez brièvement :
   - comment OCP permet d'ajouter une tarification;
   - comment DIP oriente la dépendance du contexte;
   - pourquoi le polymorphisme est un mécanisme, tandis que Strategy organise
     la collaboration.

Le choix de la stratégie appartient au point de composition.
`ServiceLivraisons` délègue le calcul sans sélectionner le type concret.
L'exercice 2 a construit le mécanisme polymorphe; celui-ci organise maintenant
les rôles et la délégation qui caractérisent Strategy.

> [!TIP]
> **Test déjà présent :** la théorie de l’exercice 2 sur les calculateurs.
> **Test à ajouter ici :** un test du contexte avec une stratégie contrôlée.
> Ne dupliquez pas une seconde théorie sur les mêmes calculateurs.
Après l’exécution des tests, fusionnez dans le dépôt Git la branche
`fonctionnalite/strategy-livraison` dans la branche `dev`, puis la branche
`dev` dans la branche `main`.
