# Départ - S07E03_Reservations

Parcours S7 allégé : environ 15 + 15 + 45 minutes en classe. Le domaine Reservation, le contrat, les traces, le client et l'assemblage du Terminal sont fournis. Compléter uniquement le constructeur simple de ReservationIntrouvableException et les trois méthodes de DepotReservationsMemoire, puis écrire les cas essentiels annoncés dans les énoncés.

```bash
dotnet build S07E03_Reservations.slnx
dotnet test S07E03_Reservations.slnx
dotnet run --project S07E03_Reservations.Terminal -- --exceptions
dotnet run --project S07E03_Reservations.Terminal -- --collections
dotnet run --project S07E03_Reservations.Terminal -- --depot
```

Les modes exceptions et collections fonctionnent dès le départ. Le mode depot exige les méthodes complétées. Les quatre cas fournis vérifient seulement Reservation et ne prouvent pas le travail demandé; ajouter deux cas pour l'exception et six pour le dépôt.

Trois projets .NET 10/C# 14 autonomes. Aucun fichier de données, conteneur ou paquet de persistance. Ne pas ajouter un travail hors classe supplémentaire à ces exercices; les approfondissements des supports restent des ressources.
