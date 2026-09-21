# Exercice 3

## Mission et durée

En 60 minutes, rendez explicite le patron Strategy pour choisir la règle de
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

1. Dans le projet principal, reprenez l’interface
   `ICalculateurFraisLivraison` et les trois classes de calculateurs de
   l’exercice 2 : l’interface devient le contrat Strategy et les classes
   deviennent les stratégies concrètes.
2. Ajoutez un paramètre de type `ICalculateurFraisLivraison` au constructeur de la
   classe `ServiceLivraisons` afin d’y injecter la stratégie choisie.
   Refusez une valeur `null` avec `ArgumentNullException`.
3. Dans la classe `ServiceLivraisons`, retirez la sélection par chaîne de
   caractères et la cascade conditionnelle. Retirez les paramètres `Client`
   et `mode` de sa méthode `CalculerFrais` : elle reçoit désormais seulement
   `sousTotal` et `distanceKm`, puis délègue le calcul à l’objet reçu par le
   constructeur. Adaptez les appels dans la méthode `Program.Main` du projet
   Terminal.
4. Dans la classe `Program` du projet Terminal, créez une méthode statique
   `ChoisirCalculateur(string mode, Client client)` qui retourne un
   `ICalculateurFraisLivraison`. Elle choisit entre le calculateur gratuit,
   prioritaire ou standard selon le mode demandé; pour le mode prioritaire,
   elle consulte la méthode d’admissibilité de `Client` construite à
   l’exercice 1. Pour le mode gratuit, elle construit le calculateur gratuit
   avec son calculateur standard de repli. La règle du seuil de 50 $ demeure
   dans le calculateur gratuit. Appelez `ChoisirCalculateur` depuis
   `Program.Main`, puis construisez le service avec le résultat.
5. Conservez les tests des exercices 1 et 2, dont les deux méthodes
   `[Theory]`. Celle de l’exercice 2 prouve déjà que les calculateurs sont
   substituables.
   Dans le projet de tests, ajoutez deux méthodes de test de la classe
   `ServiceLivraisons` : l’une utilise une stratégie contrôlée pour vérifier
   la délégation, l’autre vérifie que le constructeur refuse `null`.
6. Dans le fichier `DECISIONS.md`, associez explicitement les rôles du patron
   aux classes et à l’interface du projet : contexte, contrat Strategy,
   stratégies concrètes et point de composition.
7. Expliquez brièvement :
   - comment OCP permet d'ajouter une tarification;
   - comment DIP oriente la dépendance du contexte;
   - pourquoi le polymorphisme est un mécanisme, tandis que Strategy organise
     la collaboration.

Les conditions de choix sont regroupées dans `Program.ChoisirCalculateur`,
au point de composition. Elles ne disparaissent pas de tout le programme.
`ServiceLivraisons` délègue le calcul sans sélectionner le type concret.
L'exercice 2 a construit le mécanisme polymorphe; celui-ci organise maintenant
les rôles et la délégation qui caractérisent Strategy.

> [!TIP]
> **Tests déjà présents :** les théories des exercices 1 et 2, ainsi que le
> test de précondition du calculateur gratuit.
> **Tests à ajouter ici :** la délégation du contexte et le refus d’une
> stratégie `null`.
> Ne dupliquez pas la théorie sur les calculateurs.

Après l’exécution des tests, fusionnez dans le dépôt Git la branche
`fonctionnalite/strategy-livraison` dans la branche `dev`, puis la branche
`dev` dans la branche `main`.
