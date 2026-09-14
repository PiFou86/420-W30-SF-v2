# Décisions

## Exercice 1 — Substitution et DIP

À compléter :

- collaborateur devenu interchangeable;
- emplacement qui choisit l'implantation concrète;
- partie de la classe `ServiceCommandes` qui reste stable lors d'une
  substitution;
- différence entre l’assemblage manuel et celui du cadriciel;
- justification du cycle de vie de chaque service;
- comparaison entre l’espion manuel et Moq.

## Exercice 2 — SRP et CQS

À compléter :

- raisons de changer distinctes des classes `ServiceCommandes` et
  `CalculateurTaxe`;
- état modifié par la méthode de commande `Creer` de la classe
  `ServiceCommandes`;
- absence d'effet observable de la méthode de requête
  `ObtenirDerniereCommande` de la classe `ServiceCommandes`;
- mise à jour des assemblages manuel et avec le cadriciel.

## Exercice 3 — Loi de Déméter

À compléter :

- chaîne d’appels supprimée et message métier choisi;
- état observé par l’espion manuel;
- interaction équivalente vérifiée avec Moq.
