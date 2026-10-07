# Données de validation du TP01

Ces cas sont propres à chaque lot et doivent fonctionner sans les autres projets. Ajouter ses fichiers d'exemple sans secret; isoler les fichiers générés et les répertoires temporaires de tests. Les données valides et les données invalides servent des vérifications distinctes.

## Commandes

Commande 101, lignes « Soupe » (6,50 × 2) et « Sandwich » (9,00 × 1) : total attendu **22,00**. Sauvegarder puis reconstruire avec une nouvelle instance du dépôt; le total reste 22,00. Ces prix sont fournis par le lot Commandes, sans demander un menu à un autre lot.

Tester : quantité 0 et -1; prix -0,01; libellé nul/blanc; numéro 0; confirmation d'une commande vide; numéro 101 déjà présent. Les refus ne provoquent aucun ajout. Vérifier aussi la protection des lignes retournées.

## Menu

Éléments : 1 « Soupe » à 6,50; 2 « Sandwich » à 9,00; 3 « Eau » à 0,00. Recherche « sOu » : un résultat, « Soupe ». Recherche « Pizza » : aucun résultat. Une nouvelle instance relit les mêmes trois éléments; `[]` produit un menu vide.

Tester : prix zéro accepté et -0,01 refusé; nom nul/blanc; numéro 0; ajout du numéro 1 déjà présent. Les résultats de recherche ne permettent pas d'ajouter un élément à la collection interne.

## Réservations — équipe de trois

Même journée, ressource « Salle A », réservation 301, 4 personnes, 10 h–11 h. Les intervalles sont `[début, fin[`.

| Nouvelle demande | Résultat attendu |
|---|---|
| Salle A, 10 h 30–11 h 30 | Refus : chevauchement partiel |
| Salle A, 10 h 15–10 h 45 | Refus : période incluse |
| Salle A, 9 h 30–11 h 30 | Refus : période englobante |
| Salle A, 11 h–12 h | Acceptée : périodes contiguës |
| Salle B, 10 h–11 h | Acceptée : ressource différente |

Tester également : 1/12 personnes acceptées, 0/13 refusées; début égal ou postérieur à fin; ressource nulle/blanche; numéro 0 et numéro déjà utilisé. Après rechargement JSON, les conflits de la Salle A sont encore détectés.

## Fichiers et incidents — chaque lot

- Fichier absent dans un dossier existant → dépôt vide; `[]` → collection vide valide.
- JSON mal formé, `null`, entrée nulle, données violant un invariant ou numéro répété → incident technique, pas une absence silencieuse.
- L'échec de lecture ou de conversion ne remplace pas le fichier par des données nouvelles.
- Message utilisateur sans chemin complet, secret ni trace; journal avec opération et type, distinct du fichier des données. Journal inaccessible : message sûr encore affiché.
- Tester au moins un incident du port simulé avec une doublure locale : il ne devient pas un refus métier attendu.
