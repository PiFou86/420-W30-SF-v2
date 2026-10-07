# Départ - S07E03_Reservations

Solution commune aux trois exercices de S7, charge indicative **15 + 15 + 45 minutes en classe**. Ouvrir `S07E03_Reservations.slnx` dans ce dossier; ne pas créer trois solutions différentes.

## Repères des projets

| Emplacement dans ce dossier | Rôle et travail |
| --- | --- |
| `S07E03_Reservations/` | Bibliothèque principale : compléter le constructeur simple de ReservationIntrouvableException (E1) et DepotReservationsMemoire (E3) |
| `S07E03_Reservations.Terminal/` | Program et démonstrations fournis; lancer les modes demandés sans modifier ces fichiers |
| `S07E03_Reservations.Tests/` | Projet de tests : conserver ReservationFournieTests, créer les fichiers de tests demandés à E1 et E3 |
| `DECISIONS.md` | Réponses courtes et tableaux de prédictions, regroupés par exercice |

Reservation, IDepotReservations, ParcoursExceptions, ConsultationReservations et le constructeur d'exception avec cause sont fournis. Le nom de solution S07E03 est conservé, même pour E1 et E2.

## Exécuter les commandes au bon endroit

Toutes les commandes ci-dessous se lancent **dans ce dossier**, celui qui contient le fichier `.slnx`. Si votre terminal se trouve à la racine du dépôt Exercices, y entrer d'abord avec :

```bash
cd semaine-07/S07E03_Reservations
```

Puis choisir la commande utile :

```bash
dotnet build S07E03_Reservations.slnx
dotnet test S07E03_Reservations.slnx
dotnet run --project S07E03_Reservations.Terminal -- --exceptions
dotnet run --project S07E03_Reservations.Terminal -- --collections
dotnet run --project S07E03_Reservations.Terminal -- --depot
```

`--` sépare les options de dotnet des arguments envoyés au programme. Les modes exceptions et collections fonctionnent dès le départ; ils ne prouvent pas que les parties à compléter sont terminées. Le mode depot utilise vos trois méthodes et peut donc signaler NotImplementedException avant E3.

## Tests déjà fournis et tests à ajouter

Le fichier ReservationFournieTests contient trois méthodes de tests, dont une exécutée avec deux valeurs : **quatre cas** sont donc fournis sur Reservation. Ajouter deux cas pour la classe ReservationIntrouvableException à E1 et six pour le dépôt à E3. Au terme du parcours : **au moins douze cas réussis**. Les fichiers `.cs` ajoutés au dossier du projet Tests sont compilés automatiquement; les paquets xUnit nécessaires sont déjà présents.

Noms de classe et espace de noms : suivre le modèle fourni. Les tests construisent leurs sujets directement, sans conteneur. Aucun fichier de données ni paquet de persistance n'est requis.
