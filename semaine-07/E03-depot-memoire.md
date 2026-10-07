# Exercice 3

## But et durée - 45 minutes

Compléter un dépôt mémoire, puis vérifier son contrat. Continuer dans la même [solution S07E03_Reservations.slnx](S07E03_Reservations/S07E03_Reservations.slnx). Garder les projets, leurs noms et leurs références tels qu'ils sont fournis.

## Fichiers à utiliser

Les chemins partent du dossier de solution `semaine-07/S07E03_Reservations/`.

| Fichier | Action |
| --- | --- |
| `S07E03_Reservations/DepotReservationsMemoire.cs` | Ajouter la variable d'objet privée et compléter les trois méthodes |
| `S07E03_Reservations.Tests/DepotReservationsMemoireTests.cs` | Créer ce fichier pour les six tests du dépôt |
| `DECISIONS.md` | Compléter la section Exercice 3 |

Lire l'interface IDepotReservations fournie. Le domaine Reservation, le client ConsultationReservations et le fichier Program.cs du Terminal sont déjà fournis et restent inchangés.

## 1. Implanter le stockage et les trois opérations

Dans la classe DepotReservationsMemoire, ajouter une variable d'objet **privée**, de type `Dictionary<int, Reservation>`, initialisée avec un dictionnaire vide pour chaque nouveau dépôt. Son nom doit commencer par `m_`. La clé est le numéro de la réservation; la valeur est l'objet Reservation.

Remplacer les trois lignes `throw new NotImplementedException();` par les comportements suivants, en gardant les signatures de l'interface :

| Opération et situation | Comportement attendu |
| --- | --- |
| `Ajouter(reservation)` avec un objet non nul et un numéro encore absent | Stocker cet objet sous son numéro |
| `Ajouter(null)` | Lever ArgumentNullException, sans modifier le dépôt |
| `Ajouter(reservation)` avec un numéro déjà présent | Lever InvalidOperationException, sans remplacer le premier objet |
| `Obtenir(numero)` avec `numero <= 0` | Lever ArgumentOutOfRangeException |
| `Obtenir(numero)` avec un numéro positif présent | Retourner le même objet que celui ajouté |
| `Obtenir(numero)` avec un numéro positif absent | Retourner null |
| `ObtenirToutes()` | Retourner un **tableau de copie Reservation[]** contenant les objets du dépôt, sous l’interface IReadOnlyCollection<Reservation> |

Ne pas exposer le dictionnaire interne. Aucun ordre de parcours n'est garanti. La copie est celle des cases de la collection : les références vers les réservations sont conservées, et ces objets sont immuables. Un dépôt vide rend un tableau vide.

## 2. Écrire les six tests du dépôt

Créer la classe `DepotReservationsMemoireTests` dans le fichier indiqué. Reprendre les imports, l'espace de noms et la structure `[Fact]`/AAA du fichier fourni `ReservationFournieTests.cs`. **Chaque test construit son propre DepotReservationsMemoire**, pour que son résultat ne dépende pas des autres tests.

| Cas | Préparation et action | Vérification attendue |
| --- | --- | --- |
| 1. Ajout retrouvé | Ajouter une réservation 17 « Réunion », puis appeler Obtenir(17) | Le résultat est le même objet ajouté (`Assert.Same`) |
| 2. Absence normale | Dans un dépôt vide, appeler Obtenir(99) | Le résultat vaut null |
| 3. Objet nul | Appeler Ajouter(null) | ArgumentNullException est levée |
| 4. Numéro invalide | Appeler Obtenir(0) | ArgumentOutOfRangeException est levée |
| 5. Doublon | Ajouter une première réservation 17, puis essayer d'ajouter un second objet portant aussi le numéro 17 | InvalidOperationException est levée; Obtenir(17) rend encore le premier objet |
| 6. Copie indépendante | Ajouter seulement une réservation 17; obtenir le tableau de copie et remplacer sa première case par une réservation 18 | Le dépôt contient toujours le premier objet sous 17 et ne contient pas 18 |

Pour le cas 6, la signature retourne **IReadOnlyCollection<Reservation>**, qui ne permet pas directement l'affectation par index. Dans ce test, `Assert.IsType<Reservation[]>(collection)` vérifie le type concret de la copie et renvoie le tableau; c'est ce tableau que vous pouvez modifier. Réinterroger ensuite **le dépôt**, pas seulement le tableau, pour prouver qu'il est intact.

Pour les cas 3 à 5, utiliser Assert.Throws avec le type exact. Au cas 3, passer `null!`, comme dans le modèle de tests fourni : le `!` supprime l’avertissement de nullabilité, mais la valeur transmise reste null. Conserver les tests fournis et ceux de l'exercice 1; ils ne remplacent pas ces six tests. Aucun conteneur, fichier ni Moq n'est nécessaire.

## 3. Lancer la démonstration fournie

Depuis le dossier contenant `S07E03_Reservations.slnx` :

```bash
dotnet build S07E03_Reservations.slnx
dotnet test S07E03_Reservations.slnx
dotnet run --project S07E03_Reservations.Terminal -- --depot
```

Le Terminal construit le dépôt et ajoute les données; aucune saisie n'est demandée. Une fois votre dépôt complété, ses trois lignes doivent être :

```text
Réservation 17 : Réunion
Recherche 99 absente : True
Nombre de réservations : 2
```

Dans la section Exercice 3 de `DECISIONS.md`, écrire une phrase sur le devenir de ces données après l'arrêt, puis noter le résultat de compilation et le nombre de cas réussis.

## Quand l'exercice est terminé

Les trois méthodes sont complétées, les six tests du dépôt passent et la démonstration affiche les valeurs attendues. La suite contient alors **au moins douze cas** : quatre cas déjà fournis sur Reservation, deux cas ajoutés à l'exercice 1 et six cas ajoutés ici. L'exercice 2 ne demande aucun nouveau test automatisé.

Ne pas recréer le client, la trace ou l'assemblage. Les vérifications supplémentaires du corrigé complet ne sont pas de nouvelles tâches obligatoires. Ce parcours de 75 minutes prépare l'amorce du [TP01](../TP/TP01/README.md); sa réalisation se poursuit ensuite dans le lot propre à chaque personne.
