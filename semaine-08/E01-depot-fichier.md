# Exercice 1

## Mission et durée

Environ 90 minutes. Réinvestissez `IDepotReservations` de la semaine 7 sans modifier ses clients.

## Travail demandé

1. Dans une nouvelle solution `S08E01_ReservationsFichier`, séparez les projets `Domaine`, `Application`, `Infrastructure`, `Terminal` et `Tests`. Le projet Terminal référence Application et Infrastructure; Infrastructure référence le projet qui porte `IDepotReservations`; le domaine ne référence aucun détail technique.
2. Dans Infrastructure, implantez `DepotReservationsJson` avec `System.Text.Json`. Son constructeur refuse un chemin vide. La méthode `Ajouter` conserve la règle de doublon; `Obtenir` et `ObtenirToutes` conservent le contrat de la semaine 7.
3. Décidez explicitement ce que signifie « fichier absent » et ce que signifie « fichier JSON illisible ». Testez ces deux cas séparément. Une option raisonnable est dépôt vide pour l'absence, exception technique conservée pour un contenu invalide.
4. Dans le fichier `Program.cs` du projet Terminal, la méthode statique `Main(string[] args)` lit le chemin de données depuis `appsettings.json` via `Host.CreateApplicationBuilder(args)` ou `IConfiguration`, puis assemble le dépôt. Gardez la résolution du conteneur à la racine de composition.
5. Dans le projet de tests, nommez la classe `DepotReservationsJsonTests`. Utilisez un répertoire temporaire propre à chaque test; testez l'aller-retour JSON, l'absence, le doublon et le contenu illisible. Ne testez pas le dépôt fichier avec Moq.
6. Dans `DECISIONS.md`, décrivez comment une implantation YAML pourrait fournir le même contrat. N'ajoutez une bibliothèque YAML que si vous implantez réellement cette variante; comparez les formats sans changer l'interface.

Point de contrôle : le même code client doit pouvoir utiliser `DepotReservationsMemoire` ou `DepotReservationsJson`.

<details>
<summary>Rappel des semaines précédentes</summary>

Revoir l'[assemblage par le cadriciel de la semaine 4](../semaine-04/README.md) et le [dépôt mémoire de la semaine 7](../semaine-07/E03-depot-memoire.md). API possibles : `Host.CreateApplicationBuilder`, `builder.Configuration`, `JsonSerializer`, `File.ReadAllText`. Où doit vivre le choix du chemin et de l'implantation?

</details>
