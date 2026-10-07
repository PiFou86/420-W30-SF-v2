# Données de validation du TP01

Ces cas sont propres à chaque **lot individuel**, c'est-à-dire la fonctionnalité et la solution de cinq projets attribuées à une personne. Ils doivent fonctionner sans les projets des coéquipiers. Ajouter ses fichiers d'exemple sans secret; isoler les fichiers générés et les répertoires temporaires de tests. Les données valides et invalides servent des vérifications distinctes.

## Quand utiliser ces cas

- **S7 :** vérifier les calculs, invariants, cas normaux/limites/refus et les opérations du dépôt **mémoire**.
- **S8 :** après l'introduction des fichiers, vérifier la sauvegarde/relecture **JSON**, la reconstruction des objets, la configuration et les incidents/journalisation de fichier.

Une « nouvelle instance du dépôt » dans une vérification de persistance signifie **un deuxième objet dépôt JSON indépendant du premier et configuré sur le même fichier**. Ce deuxième objet doit obtenir les données du fichier et reconstruire les objets du domaine. Les deux dépôts peuvent être créés dans un même test, sans redémarrer l'application. Une nouvelle instance d'un dépôt uniquement en mémoire commence vide; elle ne valide pas la persistance JSON.

## Commandes

### S7 — calcul, règles et mémoire

Commande 101, lignes « Soupe » (6,50 × 2) et « Sandwich » (9,00 × 1) : total attendu **22,00**. Ajouter et retrouver la commande dans le dépôt mémoire. Ces prix sont les propres données d'entrée du lot Commandes; aucun appel au lot Menu d'un coéquipier n'est nécessaire.

Tester : quantité 0 et -1; prix -0,01; libellé nul/blanc; numéro 0; confirmation d'une commande vide; numéro 101 déjà présent. Les refus ne provoquent aucun ajout. Vérifier aussi la protection des lignes retournées.

### S8 — sauvegarde et relecture JSON

1. Enregistrer la commande 101 et ses lignes avec un premier objet dépôt JSON.
2. Créer un **deuxième objet dépôt JSON** en lui donnant **le même chemin de fichier**.
3. Relire la commande 101 au moyen de ce deuxième dépôt; reconstruire la commande et ses lignes à partir des données JSON.
4. Vérifier le numéro, les libellés, prix et quantités des deux lignes, l'état de confirmation enregistré et le total, qui reste **22,00**.

L'objet commande lu doit provenir des données enregistrées, sans réutiliser la référence de la commande conservée par le premier dépôt.

## Menu

### S7 — données, recherche et mémoire

Éléments : 1 « Soupe » à 6,50; 2 « Sandwich » à 9,00; 3 « Eau » à 0,00. Recherche « sOu » : un résultat, « Soupe ». Recherche « Pizza » : aucun résultat. Ajouter et consulter les trois éléments dans le dépôt mémoire.

Tester : prix zéro accepté et -0,01 refusé; nom nul/blanc; numéro 0; ajout du numéro 1 déjà présent. Les résultats de recherche ne permettent pas d'ajouter un élément à la collection interne.

### S8 — sauvegarde et relecture JSON

Enregistrer les trois éléments avec un premier objet dépôt JSON. Créer un deuxième objet dépôt JSON utilisant le même fichier, puis lui faire relire les éléments : mêmes numéros, noms et prix. Vérifier séparément qu'un fichier contenant `[]` produit un menu vide.

## Réservations — équipe de trois

### S7 — règles, conflits et mémoire

Même journée, ressource « Salle A », réservation 301, 4 personnes, 10 h–11 h. Les intervalles sont `[début, fin[`.

| Nouvelle demande | Résultat attendu |
|---|---|
| Salle A, 10 h 30–11 h 30 | Refus : chevauchement partiel |
| Salle A, 10 h 15–10 h 45 | Refus : période incluse |
| Salle A, 9 h 30–11 h 30 | Refus : période englobante |
| Salle A, 11 h–12 h | Acceptée : périodes contiguës |
| Salle B, 10 h–11 h | Acceptée : ressource différente |

Tester également : 1/12 personnes acceptées, 0/13 refusées; début égal ou postérieur à fin; ressource nulle/blanche; numéro 0 et numéro déjà utilisé.

### S8 — sauvegarde, relecture et conflits

Enregistrer la réservation 301 dans JSON. Créer un deuxième objet dépôt JSON utilisant le même fichier et lui faire relire cette réservation. Vérifier ses valeurs, puis confirmer que les demandes conflictuelles pour la Salle A restent refusées après ce rechargement.

## Fichiers et incidents — S8, chaque lot individuel

- Fichier absent dans un dossier existant → dépôt vide; `[]` → collection vide valide.
- JSON mal formé, `null`, entrée nulle, données violant un invariant ou numéro répété → incident technique, pas une absence silencieuse.
- L'échec de lecture ou de conversion ne remplace pas le fichier par des données nouvelles.
- Message utilisateur sans chemin complet, secret ni trace; journal avec opération et type, distinct du fichier des données. Journal inaccessible : message sûr encore affiché.
- Tester au moins un incident du port simulé avec une doublure locale : il ne devient pas un refus métier attendu.

Ces vérifications complètent le travail du domaine et du dépôt mémoire commencé en S7. Les livrables complets de fin de TP restent ceux de l'[énoncé](README.md).
