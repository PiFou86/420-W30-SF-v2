# Exercice 3

## Mission et durée

Environ 35 minutes. Dans la solution cumulative de l'exercice 1, orchestrez un cas d'utilisation sans déplacer les règles du domaine vers l'écran.

## Travail demandé

1. Dans Application, créer `ReservationDto` de sortie (numéro, titre) avec un constructeur recevant une entité `Reservation` non nulle. L'entité ne connaît pas ce DTO. Son constructeur de valeurs refuse aussi un numéro non positif et un titre nul/vide/blanc.
2. Dans Application, créer `ServiceReservations` recevant un `IDepotReservations` non nul. La méthode `Obtenir(int numero)` conserve une précondition de numéro positif et retourne `ReservationDto?`. Ajouter `TenterAjouter(int numero, string titre)` : numéro non positif, titre invalide et numéro déjà présent retournent `Result<ReservationDto>.Echec`, **sans appeler** la méthode `Ajouter` du dépôt; un ajout valide retourne `Succes` avec le DTO. Réutiliser les membres `EstSucces`, `Valeur`, `MessageErreur`, `Succes(...)` et `Echec(...)` du mécanisme déjà étudié. Une erreur technique du dépôt se propage jusqu'à la frontière prévue. Les fabriques du résultat refusent une valeur nulle pour `Succes` et un message nul/vide/blanc pour `Echec`.
3. Dans Terminal, créer `ReservationViewModel` recevant un DTO non nul, avec `Libelle` et `EtatAffiche` adaptés à la vue. Adapter les deux modes d'assemblage pour construire le même service, puis convertir son DTO en ViewModel. Conserver le message sûr et le journal de semaine 7. L'écran ne lit pas JSON et ne décide pas d'un invariant.
4. Ajouter des tests avec de vrais objets pour les conversions et le ViewModel, puis une doublure manuelle de `IDepotReservations` pour observer les appels du service : succès = un ajout; refus = aucun ajout; incident technique ≠ refus métier. Vérifier les frontières publiques et conserver tous les tests antérieurs, y compris mémoire, JSON et YAML. Construire directement le sujet testé.
5. Dans `DECISIONS.md`, identifier une responsabilité à réusiner dans le TP01 : invariant, cas d'utilisation, accès fichier ou affichage. Avant/après, vérifier le même comportement. Un dépôt qui relit/réécrit le fichier n'offre ni transaction ni protection contre deux écrivains concurrents; ne pas promettre ces garanties.

Point de contrôle : tests verts, affichage identique dans les deux modes et aucune dépendance du domaine vers DTO, fichier ou interface utilisateur. Le résultat expose une valeur seulement en cas de succès; la consultation de `Valeur` en échec doit lever `InvalidOperationException`.

<details>
<summary>Rappel : couche et objet de transport</summary>

Revoir le [classement des responsabilités](./E02-couches-dto.md). Pourquoi un objet de transfert ne doit-il pas rendre une entité invalide possible, même si le service accepte une saisie invalide comme refus attendu?

</details>
