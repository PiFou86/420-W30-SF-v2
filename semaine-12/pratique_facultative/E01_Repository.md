# Exercice 1

**40 min · Deux dépôts, un contrat commun**

Ouvrir [la solution de départ](../S12E01E03_Collaboration/S12E01E03_Collaboration.slnx). Générer et démarrer le projet Terminal. Il annonce 3 produits et 2 catégories.

Dans le projet principal, faire évoluer `Contrats.cs` pour que les deux interfaces de dépôt réutilisent un contrat `IRepository<T>`. Conserver la recherche spécialisée sur le port produits. Vérifier que les deux dépôts en mémoire restent utilisables par leurs ports et par le contrat générique. Adapter la classe `DepotProduitsEspion` pour fournir un résultat choisi et retenir le numéro demandé. Depuis `Program.cs`, démontrer un appel via l’abstraction et vérifier ce que l’espion a reçu. Le code doit compiler et conserver l’affichage initial.
