# Départ commun - Semaine 8

Depuis ce dossier, qui contient `S08E01E03_ReservationsFichier.slnx` :

```bash
dotnet build S08E01E03_ReservationsFichier.slnx
dotnet test S08E01E03_ReservationsFichier.slnx --filter FullyQualifiedName~DepotReservationsJsonTests
```

Le départ compile. Certains tests JSON échouent volontairement avant E1 :
les repères **E1-A** et **E1-B** du dépôt JSON restent à compléter.
Les autres classes et les tests sont fournis; le dépôt YAML est déjà fonctionnel.

- Domaine : Reservation et ses invariants.
- Application : contrat de dépôt, service, Result<T> et DTO de sortie.
- Infrastructure : dépôts, conversion de persistance et journal.
- Terminal : Program, configuration et ViewModel.
- Tests : cas fournis avec fichiers temporaires; aucun test à écrire.

Les données d'exemple sont copiées dans le dossier de l'exécutable lors de la compilation.
Les manipulations ne modifient pas vos fichiers du TP01.

Suivre [E1](../E01-depot-fichier.md), [E2](../E02-couches-dto.md),
puis [E3](../E03-viewmodel-erreurs.md). Le modèle réduit numéro/titre
ne remplace pas les règles horaires du lot Réservations du TP01.
