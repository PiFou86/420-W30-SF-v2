# Exemple exécutable des diapositives de semaine 8

Prérequis : SDK .NET 10 (C# 14). Ouvrir `Reservations.slnx` dans l’IDE ou utiliser
un terminal. Cette démonstration complète les fragments des diapositives : entités,
DTO de persistance, conversions, dépôts JSON/YAML, configuration et ILogger.

## Démarrer

Depuis ce dossier :

```sh
dotnet build Reservations.slnx
dotnet run --project Reservations.Terminal
```

La première exécution enregistre **1, Réunion** et **2, Atelier**, puis les relit
avec une nouvelle instance du dépôt. Elle montre ensuite le refus attendu d’un
doublon et les deux placements de conversion (dans le DTO et dans une classe dédiée).
Une nouvelle exécution conserve les données : les créations déjà présentes sont refusées.

Le fichier est dans `Reservations.Terminal/bin/Debug/net10.0/reservations.json`.
Il est volontairement séparé des données exemples et du code. La configuration
est copiée auprès de l’exécutable. Les chemins relatifs de `Donnees:Chemin` partent
de ce même dossier, quel que soit le dossier courant du terminal.

## Essayer YAML et les opérations

```sh
# Même démonstration, avec un fichier YAML distinct.
dotnet run --project Reservations.Terminal -- --Donnees:Format=yaml --Donnees:Chemin=reservations.yaml

# Lister le fichier JSON existant.
dotnet run --project Reservations.Terminal -- --Scenario=lister

# Ajouter une réservation au fichier JSON.
dotnet run --project Reservations.Terminal -- --Scenario=ajouter --Reservation:Numero=3 --Reservation:Titre="Rencontre"

# Afficher aussi le message de journal de niveau Information à la fin du scénario.
dotnet run --project Reservations.Terminal -- --Scenario=lister --Logging:LogLevel:Default=Information
```

Pour relire le fichier YAML, fournir **les deux options** de format et de chemin
avec `--Scenario=lister`. Choisir YAML sans changer le chemin ne convertit pas
un ancien fichier JSON. Le fichier `appsettings.json` fixe les valeurs par défaut;
les options après `--` les remplacent. Une variable comme `Donnees__Format` est
également reconnue par le Host. Aucun secret n’est nécessaire à cette application.

Pour repartir de zéro sans supprimer un fichier, choisir un nouveau nom avec
`--Donnees:Chemin=autre-essai.json` (ou `.yaml`, avec le format correspondant).
Le dossier parent doit exister. Le programme ne le crée pas silencieusement.

## Observer un incident

Le dossier `donnees-exemples/` contient deux fichiers valides et un JSON invalide.
Pour tester la lecture invalide sans modifier ces fichiers, fournir leur chemin
absolu. Depuis le dossier de la solution, sur macOS/Linux :

```sh
dotnet run --project Reservations.Terminal -- --Scenario=lister --Donnees:Chemin="$PWD/donnees-exemples/invalide.json"
```

Le programme affiche un diagnostic de catégorie via ILogger, puis un message
utilisateur, et termine avec le code **1**. Il ne réécrit pas le fichier invalide.
Pour vérifier le code de sortie dans un terminal macOS/Linux, lancer `echo $?`
immédiatement après la commande. Le seuil `Critical` masque le journal de niveau
Error, mais le message utilisateur reste affiché.

Un fichier absent dans un dossier existant et `[]` donnent une liste vide.
Un fichier vide, `null`, une entrée nulle, un invariant violé, des numéros répétés,
un dossier absent ou un accès refusé provoquent un incident. Un numéro demandé
qui existe déjà donne un refus attendu sous forme de `Result<Reservation>`.

## Retrouver le code des diapositives

| Projet / fichier | Rôle et correspondance |
|---|---|
| `Reservations.Domaine/Reservation.cs` | Entité, numéro strictement positif et titre non vide (diapo 6) |
| `Reservations.Application/IDepotReservations.cs` et `ServiceReservations.cs` | Contrat et orchestration du cas d’utilisation (diapos 9 et 28) |
| `Reservations.Application/Result.cs` | Refus attendu, distinct d’un incident (diapo 22) |
| `Reservations.Infrastructure/ReservationDonnees.cs` | DTO de persistance avec constructeur sans paramètre et conversions (diapos 10–11) |
| `Reservations.Infrastructure/ConvertisseurReservations.cs` | Les trois méthodes de la classe dédiée (diapo 12) |
| `Reservations.Infrastructure/DepotReservationsJson.cs` | Options, sérialisation, lecture, ajout et recherche (diapos 15–19) |
| `Reservations.Infrastructure/ValidationReservations.cs` | Vérification complète avant reconstruction et écriture (diapos 17 et 21) |
| `Reservations.Infrastructure/DepotReservationsYaml.cs` | Même contrat, sérialiseur YAML (diapo 20) |
| `Reservations.Terminal/Program.cs` | Assemblage du même Host, ILogger, catch et commandes (diapos 23–28) |
| `Reservations.Tests/ReservationsTests.cs` | Nouveau dépôt, invariants et protection des fichiers (diapos 19 et 29) |

Les fragments sont assemblés en un programme complet; les contrôles absents des
extraits courts sont ici explicites. La ligne de lecture correcte est
`donnees.Select(dto => dto!.VersEntite()).ToList()`, après vérification des valeurs
nulles. `ReservationDonnees` est un type, pas la variable contenant la collection.
La conversion externe est montrée à côté de la conversion locale; les dépôts utilisent
la variante locale. Les DTO applicatifs et ViewModels du tableau général restent
ici des repères : cet exemple console simple affiche directement les entités.

Le catch du point d’entrée suit celui des diapos. Son message vise l’opération
entière, car ce scénario peut lire **et** écrire. Un programme complet distingue
les incidents pour adapter sa réaction; ce catch termine le scénario sans faux
succès. Il ne constitue pas un modèle à recopier dans chaque méthode.

ILogger vient du paquet `Microsoft.Extensions.Hosting` (10.0.0); YAML utilise
`YamlDotNet` (16.3.0). Les espaces de noms sont nommés dans les fichiers concernés.
La console est disponible; EventLog dépend de Windows. Aucun fournisseur de
fichier journal n’est ajouté. `Logging:LogLevel:Default` configure le seuil, pas
une destination supplémentaire. Les données de réservation restent des fichiers
JSON/YAML, distincts de la journalisation.

Cette implantation pédagogique n’offre ni transaction ni protection contre les
écritures concurrentes. Une exception lors de l’écriture ne garantit pas que le
fichier soit resté intact. La protection testée concerne une **lecture invalide**,
qui interrompt l’ajout avant toute écriture.

## Vérifier

```sh
dotnet test Reservations.slnx
```

23 cas automatisés couvrent JSON et YAML : persistance avec un nouveau dépôt,
doublon, fichier absent, liste vide, contenus invalides préservés, dossier absent
et conversions. Les fichiers temporaires des tests sont isolés et supprimés.
Les scénarios console ont aussi été exécutés : démonstrations dans les deux formats,
ajout et relecture, erreur de lecture et filtrage du journal par configuration.

## Injecter un journal dans un service

`ServiceReservations` reçoit `ILogger<ServiceReservations>` dans son constructeur,
avec `IDepotReservations`. Le Host fournit ce journal automatiquement lors de la
résolution du service : aucun appel à `CreateLogger` n’est nécessaire dans le service.
La méthode `Lire()` écrit « Lecture des réservations. » au niveau Information.

Le type générique nomme la catégorie `Reservations.Application.ServiceReservations`;
il ne désigne pas une instance du service à journaliser. Dans `appsettings.json`,
cette catégorie est réglée à `Information`, alors que le seuil général reste
`Warning`. Lancer `--Scenario=lister` permet de voir la catégorie et son message.
Pour masquer uniquement ce message, ajouter :

```sh
dotnet run --project Reservations.Terminal -- --Scenario=lister --Logging:LogLevel:Reservations.Application.ServiceReservations=Warning
```

Application référence le paquet `Microsoft.Extensions.Logging.Abstractions` et
utilise `Microsoft.Extensions.Logging`. Le domaine ne dépend pas de la
journalisation. Le logger de catégorie `Demarrage` reste dans `Program.Main`
pour traiter les incidents à la frontière console. Les tests qui construisent
le service directement fournissent `NullLogger<ServiceReservations>.Instance`
(`Microsoft.Extensions.Logging.Abstractions`) : ils ne nécessitent pas de Host.
