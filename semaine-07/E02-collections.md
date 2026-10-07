# Exercice 2

## Mission - 15 minutes

Choisir une collection et vérifier quelques prédictions. La démonstration est fournie dans le fichier DemonstrationCollections.cs du projet Terminal du [départ](S07E03_Reservations/README.md). Aucune nouvelle classe ni nouveau test automatisé n'est demandé ici.

## Travail essentiel

1. Dans le fichier DECISIONS.md, choisir et justifier en une phrase chacun de ces besoins : titres dans l'ordre d'ajout avec doublons; réservation recherchée par numéro; numéros uniques sans tri requis. Comparer List<T>, Dictionary<TKey,TValue> et HashSet<T>.
2. Prédire le nombre d'éléments d'une liste et d'un ensemble contenant 17,18,17; avec 17,18, prédire le premier retrait d'une Queue<T> et d'une Stack<T>. Lancer la démonstration avec `--collections` et comparer. Ne pas utiliser l'ordre d'un ensemble ou d'un dictionnaire comme garantie de tri.
3. À l'oral, expliquer pourquoi une copie protège les cases de la collection, mais pas l'état d'éventuels objets mutables contenus. La classe Reservation fournie est immuable; le test de copie sera écrit à l'exercice 3.

Point de contrôle : trois choix justifiés et prédictions comparées aux sorties.

```bash
dotnet run --project S07E03_Reservations.Terminal -- --collections
```

Exécuter depuis le dossier de la solution. Collections triées, liste chaînée, égalité personnalisée et parcours LINQ restent dans les supports de cours; aucune implantation de ces mécanismes n'est à ajouter ici.
