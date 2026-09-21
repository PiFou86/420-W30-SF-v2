# Exercice 2

## Mission et durée

En 60 minutes, remplacez une hiérarchie fragile et une interface trop large par
des interfaces claires et des responsabilités bien séparées. Vous préparez des calculateurs substituables sans encore
transformer `ServiceLivraisons` : ce sera le rôle de l'exercice 3.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/solid-composition` depuis la branche `dev`.

## Travail demandé

Le projet de départ ne contient pas encore d’interface : il propose une
hiérarchie de classes où `CalculateurFrais` mélange le calcul des frais et une
description destinée à l’affichage. `CalculateurFraisGratuit` hérite donc de la
description « Tarification standard », qui ne lui convient pas.

Réusinez ce modèle afin que chaque classe porte seulement les responsabilités
qu’elle peut réellement honorer.

1. Créez un contrat pour calculer des frais de livraison et un autre contrat
   pour fournir une description destinée à l’affichage. Remplacez l’héritage
   entre les deux calculateurs fournis par ces contrats. Tous les calculateurs
   doivent pouvoir calculer des frais, mais seul un calculateur qui possède une
   description pertinente doit implanter le contrat de description. Choisissez
   des noms qui indiquent clairement qu’ils concernent la livraison.
2. N’injectez pas encore le contrat de calcul dans le constructeur de
   `ServiceLivraisons` : ce sera le rôle de l’exercice 3.
3. Ajoutez un calculateur de livraison prioritaire. Il applique la règle déjà
   présente dans `ServiceLivraisons` : ses frais sont de `2 $ + 1 $` par
   kilomètre. Ajoutez cette classe sans modifier les calculateurs standard et
   gratuit existants.
4. Dans le projet de tests, écrivez **un test unitaire xUnit paramétré**
   (`[Theory]`) qui reçoit successivement les calculateurs standard, gratuit et
   prioritaire à travers le contrat de calcul, puis vérifie le frais attendu.
   Il ne s’agit pas de lancer le projet Terminal.

Limitez-vous à ces trois calculateurs. Le but du test est de vérifier leur
substitution, pas de couvrir toutes les distances possibles.

Justifiez brièvement OCP, LSP et ISP dans `DECISIONS.md`. Expliquez aussi
pourquoi des objets polymorphes ne constituent pas encore, à eux seuls, le
patron Strategy. Réalisez ensuite le
parcours de branches Git `fonctionnalite/solid-composition` → `dev` → `main`
dans le dépôt de l’exercice.
