# Exercice 1

## Mission et durée

Environ 55 minutes. Réusinez la solution de la semaine 7 en conservant ses comportements. On ne découvre pas Infrastructure ni les fichiers cette semaine.

## Travail demandé

1. Copier votre état validé de `S07E03_Reservations` dans la solution cumulative `S08E01E03_ReservationsFichier`. Créer les projets `.Domaine`, `.Application`, `.Infrastructure`, `.Terminal` et `.Tests`, toujours .NET 10/C# 14. Conserver le numéro et le titre des réservations, l'API du dépôt, JSON/YAML, les décisions d'erreur et **tous** les tests antérieurs.
2. Déplacer la classe `Reservation` dans le projet Domaine. Déplacer l'interface `IDepotReservations` dans Application, qui référence Domaine. Déplacer les dépôts mémoire, JSON et YAML, le DTO de persistance `ReservationDonnees` et la classe `JournalIncidents` dans Infrastructure, qui référence Application et Domaine. Déplacer la dépendance YamlDotNet dans Infrastructure seulement.
3. Faire référencer Application et Infrastructure par Terminal, qui constitue la présentation et la racine de composition. Adapter les espaces de noms et le code appelant après chaque déplacement. Domaine ne référence aucun autre projet applicatif. Tests peut référencer les projets nécessaires à ses vérifications, y compris Terminal pour le ViewModel.
4. Dans `DECISIONS.md`, distinguer les références nécessaires à la compilation, le choix des objets dans la racine de composition et les appels à l'exécution. Expliquer pourquoi le projet Terminal connaît Infrastructure pour construire les objets, alors que le service Application reçoit uniquement `IDepotReservations`.
5. Conserver les deux modes d'assemblage manuel et cadriciel, la configuration et ses priorités. Réexécuter les tests après chaque déplacement puis comparer la sortie des deux modes avec le même fichier de données.

Point de contrôle : `dotnet test S08E01E03_ReservationsFichier.slnx` réussit avant d'ajouter des comportements. Le même contrat est fourni par mémoire, JSON et YAML. Les tests construisent directement leur sujet; ils ne le résolvent pas avec le conteneur.

<details>
<summary>Rappel : persistance déjà pratiquée</summary>

Revoir le [dépôt fichier et Git de la semaine 7](../semaine-07/E03-depot-memoire.md). API : `ProjectReference`, `RootNamespace`, `JsonSerializer`, `IDepotReservations`. Une référence de compilation signifie-t-elle que le service Application appelle une classe concrète d'Infrastructure?

</details>
