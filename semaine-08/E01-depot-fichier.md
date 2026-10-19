# Exercice 1

## Mission et durée

**15 minutes.** Enregistrer une réservation puis la relire avec un nouvel objet dépôt.
Le dépôt fichier est une nouveauté de semaine 8. La structure en projets est fournie.

## Fichiers et commandes

Ouvrir le dossier `semaine-08/S08E01E03_ReservationsFichier`. Toutes les commandes ci-dessous
se lancent depuis ce dossier. Modifier uniquement
`S08E01E03_ReservationsFichier.Infrastructure/DepotReservationsJson.cs`.
Les entités, DTO, contrôles de données et tests sont déjà fournis.

```bash
dotnet test S08E01E03_ReservationsFichier.slnx --filter FullyQualifiedName~DepotReservationsJsonTests
```

Le départ compile, mais certains tests échouent : c'est attendu avant les deux ajouts.

## Travail demandé

1. Dans la méthode `Ajouter`, au repère **E1-A**, remplacer le commentaire et le
   `throw new NotImplementedException(...)` par une écriture dans `m_chemin`.
   Utiliser `JsonSerializer.Serialize(donnees, s_options)` pour produire le texte,
   puis `File.WriteAllText` pour l'enregistrer. La liste a déjà été lue et vérifiée.
2. Dans la méthode `Lire`, au repère **E1-B**, remplacer `null` dans l'affectation de
   `donnees` par `JsonSerializer.Deserialize<List<ReservationDonnees?>>(json, s_options)`.
   Garder l'appel à `ReservationDonnees.Convertir(donnees)` : il valide les données
   et reconstruit les entités. Ne pas ajouter de `catch` qui retourne une liste vide.
3. Relancer la commande. Ouvrir le test fourni
   `Ajouter_PersistePuisRelitReservation` du fichier `DepotReservationsJsonTests.cs`.
   Identifier oralement les deux objets dépôts et le chemin partagé.

## Terminé quand

Les **7 tests ciblés passent**, dont l'aller-retour par une deuxième instance.
Le dossier temporaire et son nettoyage sont gérés par les tests. Aucun test à écrire,
aucun redémarrage du programme et aucun fichier du TP01 à utiliser.
