# Exercice 1

**Deux façons de traiter le même catalogue**

Ouvrir [la solution de départ](S11E01E03_Catalogue/S11E01E03_Catalogue.slnx) dans Visual Studio avec .NET 10. Générer puis démarrer le projet Terminal. Il annonce **14 produits et 3 catégories**.

Dans le projet principal, créer une classe avec une méthode qui applique une `Action<Produit>` à chaque élément d’une séquence et une autre qui transforme chaque produit en montant avec une `Func<Produit, decimal>`. Écrire les parcours pour observer quand le délégué est appelé. Conserver l’ordre de la séquence et laisser intactes les données de la classe `DonneesCatalogue`.

Depuis le fichier `Program.cs` du projet Terminal, faire les essais suivants.

| Essai | Résultat attendu |
| --- | --- |
| Une action affiche les noms, une autre recueille les codes | 14 noms et 14 codes dans l’ordre fourni. `EAU` est le premier code, `CRO` le dernier. `CAF` apparaît deux fois. |
| Une fonction retourne le prix unitaire, une autre calcule la valeur du stock de chaque produit | 14 montants dans chaque série. Pour le Bol, le prix unitaire est de 12,00 $ et la valeur du stock est de 24,00 $. La valeur totale du stock est de 161,00 $. |
| Réutiliser les mêmes méthodes sur les plats en stock | Bol puis Sandwich. Leurs valeurs de stock sont de 24,00 $ et de 36,00 $. |
| Réutiliser les méthodes sur une séquence vide | L’action est appelée zéro fois et la série des valeurs est vide. |

Montrer où le code choisit le comportement et où il l’exécute.
