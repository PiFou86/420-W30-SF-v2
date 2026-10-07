# Exercice 2

## But et durée - 15 minutes

Justifier trois choix de collection et comparer quatre prédictions aux sorties du programme. Utiliser le même [départ](S07E03_Reservations/README.md). Aucune classe ni aucun test automatisé n'est à écrire dans cet exercice.

## Fichiers à utiliser

Depuis le dossier de solution `semaine-07/S07E03_Reservations/` :

- remplir la section **Exercice 2** du fichier `DECISIONS.md`;
- lire le fichier `S07E03_Reservations.Terminal/DemonstrationCollections.cs`, sans le modifier.

## 1. Choisir selon trois besoins

Remplir le premier tableau de la section Exercice 2 : pour chaque besoin, choisir une famille parmi `List<T>`, `Dictionary<TKey,TValue>` et `HashSet<T>`, puis donner une justification d'une phrase. Nommer la famille suffit; aucun code de collection n'est demandé.

| Besoin à analyser |
| --- |
| Conserver des titres dans leur ordre d'ajout, avec doublons permis |
| Retrouver une réservation à partir de son numéro |
| Conserver uniquement des numéros distincts, sans tri requis |

## 2. Prédire, lancer et comparer

**Avant de lancer**, remplir la colonne « Prévu » du second tableau de `DECISIONS.md` pour ces quatre valeurs :

- la propriété Count d'une liste créée avec 17,18,17;
- la propriété Count d'un ensemble HashSet créé avec les mêmes valeurs;
- la valeur retirée par le premier appel à Dequeue sur une file Queue créée avec 17,18;
- la valeur retirée par le premier appel à Pop sur une pile Stack créée avec 17,18.

Depuis le dossier contenant la solution :

```bash
dotnet run --project S07E03_Reservations.Terminal -- --collections
```

Reporter les quatre valeurs affichées dans la colonne « Observé » et signaler toute différence avec votre prédiction. La ligne « Numéro 17 » illustre l'accès par clé; elle n'ajoute pas une cinquième question. Ne pas utiliser l'ordre de parcours du HashSet ou du Dictionary comme une garantie de tri.

## 3. Expliquer la copie à l'oral

Expliquer à l'enseignant ou à un autre étudiant la différence entre **remplacer une case du tableau retourné** et **modifier un objet mutable référencé par une case**. La classe Reservation fournie est immuable. Aucune nouvelle classe mutable ou démonstration supplémentaire n'est à construire; la première situation sera testée à l'exercice 3.

## Quand l'exercice est terminé

Les trois choix sont justifiés dans le fichier, les quatre prédictions ont été comparées aux sorties et l'explication orale a été donnée. Collections triées, liste chaînée, égalité personnalisée et LINQ restent des ressources de cours, sans implantation à ajouter ici.
