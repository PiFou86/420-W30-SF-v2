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
   constructeur. Avant la délégation, appliquez le contrat d'entrée établi à
   l'exercice 2 : refusez un sous-total négatif et une distance négative ou
   non finie avec `ArgumentOutOfRangeException`. Adaptez les appels dans la
   méthode `Program.Main` du projet Terminal.
4. Dans la classe `Program` du projet Terminal, créez une méthode statique
   `ChoisirCalculateur(string mode, Client client)` qui retourne un
   `ICalculateurFraisLivraison`. Elle choisit entre le calculateur gratuit,
   prioritaire ou standard selon le mode demandé; pour le mode prioritaire,
   elle consulte la méthode d’admissibilité de `Client` construite à
   l’exercice 1. Pour le mode gratuit, elle construit le calculateur gratuit
   avec son calculateur standard de repli. La règle du seuil de 50 $ demeure
   dans le calculateur gratuit. Appelez `ChoisirCalculateur` depuis
   `Program.Main`, puis construisez le service avec le résultat.
5. Conservez les quatre méthodes de test des exercices 1 et 2, dont les
   trois méthodes `[Theory]`. Les deux théories de l’exercice 2 vérifient la
   substitution des calculateurs et leur contrat d'entrée commun. Dans le
   projet de tests, ajoutez trois méthodes de test de la classe
   `ServiceLivraisons` : l’une utilise une stratégie contrôlée pour vérifier
   la délégation, l’autre vérifie que le constructeur refuse `null`, et une
   méthode `[Theory]` vérifie le refus d'un sous-total négatif et d'une
   distance négative ou non finie avant l'appel à la stratégie.
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
> **Tests déjà présents :** la théorie de l’exercice 1, les deux théories de
> l'exercice 2 et le test de précondition du calculateur gratuit.
> **Tests à ajouter ici :** la délégation du contexte, le refus d’une
> stratégie `null` et les préconditions de `ServiceLivraisons.CalculerFrais`.
> Ne dupliquez pas les théories sur les calculateurs.

Après l’exécution des tests, fusionnez dans le dépôt Git la branche
`fonctionnalite/strategy-livraison` dans la branche `dev`, puis la branche
`dev` dans la branche `main`.
