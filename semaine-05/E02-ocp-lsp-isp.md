# Exercice 2

## Mission et durée

En 60 minutes, remplacez une hiérarchie fragile par un contrat de calcul
étroit et par la composition. Vous préparez des calculateurs substituables;
la classe `ServiceLivraisons` deviendra un contexte Strategy à l’exercice 3.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/solid-composition` depuis la branche `dev`.

## Travail demandé

Le projet de départ ne contient pas encore d’interface : il propose une
hiérarchie de classes où `CalculateurFrais` mélange le calcul des frais et une
description destinée à l’affichage. `CalculateurFraisGratuit` hérite donc de la
description « Tarification standard », qui ne lui convient pas.

Réusinez ce modèle afin que chaque classe porte seulement les responsabilités
utiles au calcul des frais.

1. Dans le projet principal, créez l’interface `ICalculateurFraisLivraison`
   avec la seule méthode de calcul. Renommez les deux classes fournies pour
   préciser qu’elles calculent des frais de livraison. Retirez leur relation
   d’héritage et la méthode `ObtenirDescription()` : aucun calculateur ni
   service n’a besoin de produire un texte destiné à l’affichage. Ce texte
   appartient au projet Terminal, si l’on souhaite l’afficher.
2. La classe du calculateur gratuit reçoit dans son constructeur un objet
   calculateur standard. Elle retourne zéro à partir d’un sous-total de
   `50 $`; en dessous, elle délègue le calcul à cet objet, sans recopier
   la formule `4 $ + 0,75 $` par kilomètre. Son constructeur refuse une
   dépendance `null` avec `ArgumentNullException`.
3. Ajoutez une classe de calculateur prioritaire qui applique la règle déjà
   présente dans la classe `ServiceLivraisons` : `2 $ + 1 $` par kilomètre.
   Ajoutez-la sans modifier les deux autres calculateurs.
4. Dans le projet de tests, conservez le test de la classe `Client` écrit à
   l’exercice 1. Ajoutez une méthode de test unitaire xUnit paramétrée
   (`[Theory]`) qui reçoit les trois calculateurs par l’interface
   `ICalculateurFraisLivraison` et vérifie leurs frais. Incluez un cas sous
   `50 $` et un cas à partir de `50 $` pour le calculateur gratuit. Ajoutez
   aussi un test du constructeur de ce dernier avec une dépendance `null`.

La classe `ServiceLivraisons` et le projet Terminal continuent de fonctionner
comme à l’exercice 1. Vous injecterez le contrat de calcul dans le constructeur
de `ServiceLivraisons` à l’exercice 3.

Limitez-vous à ces trois calculateurs. Le but du test est de vérifier leur
substitution, pas de couvrir toutes les distances possibles.

Dans `DECISIONS.md`, expliquez pourquoi `ICalculateurFraisLivraison`
n’expose que le calcul : le futur service qui utilisera ce contrat n’a pas
besoin de la méthode `ObtenirDescription()`. Justifiez aussi brièvement
OCP, LSP et la composition, puis expliquez pourquoi des objets polymorphes
ne constituent pas encore, à eux seuls, le patron Strategy. Réalisez
ensuite le parcours de branches Git `fonctionnalite/solid-composition`
→ `dev` → `main` dans le dépôt de l’exercice.
