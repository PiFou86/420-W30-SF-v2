# Exercice 2

## Mission et durée

**5 minutes.** Changer le stockage par configuration, sans changer le service.
Utiliser le départ commun après l'exercice 1. Aucun fichier à modifier ou créer.

## Travail demandé

Depuis `semaine-08/S08E01E03_ReservationsFichier` :

1. Lire le fichier `S08E01E03_ReservationsFichier.Terminal/appsettings.json` : noter **dans votre tête**
   le format par défaut et le chemin des données. Prédire le format sélectionné
   si un argument indique `yaml`.
2. Lancer les deux commandes :

```bash
dotnet run --project S08E01E03_ReservationsFichier.Terminal
dotnet run --project S08E01E03_ReservationsFichier.Terminal -- --Donnees:Format=yaml --Donnees:Chemin=reservations.yaml
```

3. Comparer les deux sorties. Les fichiers `reservations.json` et `reservations.yaml`
   fournis contiennent les mêmes réservations. Ils sont copiés à côté de l'exécutable.
   Les chemins relatifs sont résolus depuis ce dossier, et non le dossier du terminal.
4. Dans la méthode `CreerDepot` de `Program.cs`, repérer le choix de l'implantation.
   Expliquer oralement pourquoi la classe `ServiceReservations` reste inchangée.

## Terminé quand

Les deux exécutions affichent le même libellé pour la réservation 1.
Vous pouvez expliquer pourquoi les arguments remplacent la configuration du fichier.
Aucun compte rendu, aucune implantation YAML et aucun test supplémentaire à produire.
