# Exercice 1

## Mission et durée

En 45 minutes, retirez du service la décision concernant l'admissibilité à la
livraison prioritaire.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/tell-dont-ask` depuis la branche `dev`.

## Travail demandé

1. Dans la méthode `CalculerFrais` de la classe `ServiceLivraisons`, repérez le
   code qui consulte le statut et les points de l’objet `Client` avant de
   prendre la décision d’admissibilité.
2. Ajoutez à la classe `Client` une méthode qui exprime directement la décision
   métier concernant la livraison prioritaire.
3. Rendez non publiques les propriétés de la classe `Client` qui ne sont plus
   nécessaires à ses appelants.
4. Modifiez la méthode `CalculerFrais` de la classe `ServiceLivraisons` afin
   qu’elle demande cette réponse métier à l’objet `Client`, sans reconstruire
   la règle à partir de ses données.
5. Dans le projet de tests, écrivez **une seule méthode de test paramétrée**
   avec `[Theory]` pour couvrir un client admissible et deux clients non
   admissibles.

Il n'est pas nécessaire de tester de nouveau le calcul complet des frais dans
cet exercice.

Dans `DECISIONS.md`, expliquez en deux ou trois phrases la différence entre
Tell, Don't Ask et une simple multiplication des getters.

Dans le dépôt Git de l’exercice, fusionnez la branche de fonctionnalité dans la
branche `dev`, exécutez les tests, puis fusionnez la branche `dev` dans la
branche `main`.
