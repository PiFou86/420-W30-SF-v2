# Exercice 3

## Mission - 45 minutes

Compléter le dépôt de réservations en mémoire dans la solution [S07E03_Reservations](S07E03_Reservations/README.md). Les trois projets .NET 10/C# 14, le contrat, la classe Reservation, le client et le Terminal sont fournis. Garder les noms et références de projets.

## Travail essentiel

1. Dans la bibliothèque principale, compléter les trois méthodes de la classe DepotReservationsMemoire avec un Dictionary<int,Reservation> privé nommé avec le préfixe m_. Le contrat IDepotReservations est fourni; ne pas exposer le dictionnaire.
2. Respecter ce contrat : Ajouter refuse null avec ArgumentNullException et un doublon avec InvalidOperationException, sans remplacer le premier objet; Obtenir refuse un numéro non positif avec ArgumentOutOfRangeException et retourne null pour un numéro positif absent; ObtenirToutes rend une copie indépendante de la structure interne, sans ordre garanti. Les réservations sont immuables.
3. Dans le projet Tests, conserver les cas de l'exercice 1 et couvrir **six cas du dépôt** : ajout retrouvé; numéro positif absent; ajout nul refusé; numéro 0 refusé; doublon refusé avec premier objet conservé; modification d'une case de la copie sans changement du dépôt. Construire directement de vrais objets, sans conteneur ni Moq.
4. Lancer le mode `--depot` du Terminal fourni. Il construit manuellement le dépôt, ajoute deux réservations et montre une recherche trouvée et une recherche absente. En une phrase dans DECISIONS.md, expliquer ce qui reste après l'arrêt du programme.

```bash
dotnet build S07E03_Reservations.slnx
dotnet test S07E03_Reservations.slnx
dotnet run --project S07E03_Reservations.Terminal -- --depot
```

Exécuter depuis le dossier de la solution. Point de contrôle : contrat vérifié, six cas du dépôt réussis et Terminal exécuté. Le constructeur simple de l'exception, ses deux cas et les quatre cas fournis sur Reservation donnent douze cas essentiels au total.

La classe ConsultationReservations, la trace et l'assemblage sont fournis : ne pas les reconstruire ni produire une deuxième démonstration. Tests de cause et de copie après ajout ultérieur, comparaison de deux dépôts et démonstrations Git supplémentaires ne sont pas demandés. Les notions plus avancées restent disponibles dans le corrigé de référence.

Ce parcours prépare l'amorce du [TP01](../TP/TP01/ENONCE.md), avec un lot autonome par personne. Continuer ensuite le domaine et le dépôt mémoire de son lot. Réinvestir Git dans ce travail selon les règles déjà connues, sans créer une activité Git distincte pour cet exercice. JSON/YAML et les quatre couches sont prévus en S8.
