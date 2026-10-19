# Exercice 3

## Mission et durée

**10 minutes.** Observer un incident technique et retrouver les responsabilités.
Utiliser le même départ après les exercices 1 et 2. Ne modifier aucun fichier C#.

## Travail demandé

Depuis `semaine-08/S08E01E03_ReservationsFichier` :

1. Prédire oralement la sortie avec le fichier fourni `donnees-exemples/invalide.json`,
   qui contient `{invalide`. Lancer :

```bash
dotnet run --project S08E01E03_ReservationsFichier.Terminal -- --Donnees:Chemin=invalide.json
```

2. Vérifier le message utilisateur : il signale l'impossibilité de charger les
   réservations sans afficher de trace d'appel ni de chemin complet. L'exécution
   termine avec le code 1 : c'est le résultat attendu de cette manipulation.
3. Ouvrir `reservations.log` dans
   `S08E01E03_ReservationsFichier.Terminal/bin/Debug/net10.0/`. Repérer la date,
   l'opération et la catégorie `JsonException`. Ouvrir `invalide.json` dans le même
   dossier : son contenu doit rester `{invalide`, pas être remplacé par `[]`.
4. Compléter les **quatre lignes** de `DECISIONS.md`, une par couche, en associant :
   l'entité `Reservation`; le service et son contrat; le dépôt JSON et son DTO de
   persistance; l'affichage et le ViewModel. Ajouter à chaque ligne son rôle en
   quelques mots. Aucun diagramme ni nouvelle classe à produire.
5. Relancer la commande JSON normale de l'exercice 2, puis les tests fournis :

```bash
dotnet test S08E01E03_ReservationsFichier.slnx
```

## Terminé quand

Le fichier invalide reste intact, le journal est distinct des données, le message
est sûr, les quatre responsabilités sont classées et les tests fournis passent.

Les **30 minutes d'exercices sont terminées**. Passer au dépôt JSON de votre propre
lot du TP01 : mêmes questions de sauvegarde/relecture, de configuration et d'incidents,
avec les règles et données de l'énoncé officiel.
