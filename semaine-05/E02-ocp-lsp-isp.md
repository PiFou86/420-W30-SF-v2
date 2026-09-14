# Exercice 2

## Mission et durée

En 60 minutes, remplacez une hiérarchie fragile et une interface trop large par
des contrats honnêtes. Vous préparez des calculateurs substituables sans encore
transformer `ServiceLivraisons` : ce sera le rôle de l'exercice 3.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/solid-composition` depuis la branche `dev`.

## Travail demandé

1. Dans le projet principal, séparez l’interface décrivant la capacité de
   calculer des frais de celle décrivant la capacité d’afficher une
   description.
2. Vérifiez qu’aucune classe de calculateur n’est forcée d’implanter une
   méthode qu’elle ne peut pas honorer.
3. Retirez la relation d’héritage qui permet à une classe dérivée de briser les
   attentes associées à la méthode de calcul des frais.
4. Faites implanter la nouvelle interface étroite de calcul par les classes de
   calculateurs compatibles, sans encore injecter cette interface dans le
   constructeur de la classe `ServiceLivraisons`.
5. Ajoutez une nouvelle classe de calculateur sans modifier les classes de
   calculateurs existantes.
6. Dans le projet de tests, écrivez **une seule méthode de test paramétrée** qui
   reçoit successivement les objets calculateurs compatibles et leur résultat
   attendu.

Limitez-vous aux trois calculateurs demandés. Le but est de vérifier leur
substitution, pas de couvrir toutes les distances possibles.

Justifiez brièvement OCP, LSP et ISP dans `DECISIONS.md`. Expliquez aussi
pourquoi des objets polymorphes ne constituent pas encore, à eux seuls, le
patron Strategy. Réalisez ensuite le
parcours de branches Git `fonctionnalite/solid-composition` → `dev` → `main`
dans le dépôt de l’exercice.
