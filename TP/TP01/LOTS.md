# Fiches individuelles du TP01

Un lot désigne **le travail fonctionnel d'une personne, avec sa solution et ses cinq projets**, selon l'[énoncé](ENONCE.md). Les trois lots portent sur des dimensions différentes du restaurant. Chaque personne réalise un domaine, un algorithme, un cas d'utilisation, ses dépôts et une console. Tous suivent les mêmes règles de dépendance, de tests, de persistance et de message sûr de l'énoncé. Une méthode publique reçoit une dépendance non nulle; ses autres préconditions sont documentées et testées. Aucun lot n'utilise les projets d'un coéquipier.

## Lot A — Commandes

Départ : [lots/Commandes](lots/Commandes/README.md). Une commande a un numéro positif et des lignes avec libellé non nul/non blanc, prix unitaire `decimal` non négatif et quantité entière positive. Une commande en préparation peut être vide; une confirmation vide est refusée. Une ligne et une commande protègent leurs invariants; une collection retournée ne permet pas de contourner leurs comportements.

- Construire une commande à partir des **propres données d'entrée du lot** : libellé, prix et quantité, sans classe `ElementMenu` ni appel au lot Menu.
- Ajouter des lignes, calculer le total, confirmer une commande non vide et retrouver une commande par numéro. Le calcul ne contient ni taxes ni pourboire.
- Le cas d'utilisation traduit une saisie invalide, une confirmation vide ou un numéro déjà utilisé en refus attendu `Result<T>`, sans persister un état invalide. Un appel direct invalide au domaine conserve une précondition/invariant.
- **S7 :** ajouter et retrouver les commandes dans le dépôt mémoire. **S8 :** enregistrer la commande et ses lignes dans JSON, puis les relire avec un deuxième objet dépôt JSON utilisant le même fichier; la commande reconstruite conserve le total et l’état enregistrés.
- Algorithme à expliquer : calcul du total à partir de plusieurs lignes. Les prix sont des valeurs d'entrée figées dans les lignes; ils ne changent pas si le menu d'un autre lot évolue.

Preuves : cas de plusieurs lignes, quantité 0/-1 refusée, prix négatif refusé, libellé blanc refusé, commande vide non confirmée, doublon non enregistré et aller-retour fichier.

## Lot B — Menu

Départ : [lots/Menu](lots/Menu/README.md). Un élément possède un numéro positif, un nom non nul/non blanc et un prix `decimal` non négatif. Le prix zéro est valide.

- Ajouter et consulter des éléments; rechercher les noms contenant un fragment non blanc, sans tenir compte de la casse (`StringComparison.OrdinalIgnoreCase`). Une recherche sans correspondance retourne une liste vide.
- Le cas d'utilisation retourne un refus attendu pour une saisie invalide ou un numéro déjà utilisé, sans appeler l'ajout du dépôt. Les entités protègent aussi leurs invariants en cas d'appel direct.
- **S7 :** ajouter, consulter et rechercher les éléments avec le dépôt mémoire. **S8 :** enregistrer les éléments dans JSON, puis les relire avec un deuxième objet dépôt JSON utilisant le même fichier et reconstruire les entités; un fichier valide vide reste un menu vide.
- Algorithme à expliquer : parcours de recherche et choix de la collection, avec le contrat de protection des résultats. Aucun appel au lot Commandes n'est nécessaire.

Preuves : trois éléments, recherche insensible à la casse, absence, prix zéro accepté/négatif refusé, nom blanc refusé, doublon, menu vide et aller-retour JSON.

## Lot C — Réservations, seulement pour l'équipe de trois

Départ : [lots/Reservations](lots/Reservations/README.md). Une réservation a un numéro positif, une ressource non nulle/non blanche, entre 1 et 12 personnes, et une période `DateTime` avec début strictement antérieur à fin. Toutes les heures des données du TP sont des heures locales cohérentes; aucun calcul de fuseau horaire n'est demandé.

- Créer et consulter une réservation. Refuser un numéro déjà présent et un chevauchement pour **la même ressource**; une autre ressource peut être réservée à la même heure.
- Les intervalles sont semi-ouverts `[début, fin[` : une réservation finissant à 11 h et une autre commençant à 11 h ne se chevauchent pas.
- Le domaine protège ses valeurs et porte le comportement de comparaison des périodes; le cas d'utilisation récupère les réservations par le port et orchestre le refus attendu avec `Result<T>`. Une réservation isolée ne lit pas le dépôt ni le fichier pour connaître les autres.
- **S7 :** ajouter/consulter les réservations et vérifier les conflits avec le dépôt mémoire. **S8 :** enregistrer dans JSON, relire avec un deuxième objet dépôt JSON utilisant le même fichier et vérifier que les conflits restent détectés. Le lot ne dépend ni des commandes ni du menu.
- Algorithme à expliquer : détection d'un chevauchement en comparant les périodes des réservations d'une même ressource.

Preuves : 1 et 12 personnes acceptées, 0 et 13 refusées, fin égale/antérieure au début refusée, chevauchement partiel et inclusion refusés, périodes contiguës et ressources différentes acceptées, doublon et aller-retour fichier.

## Frontières communes aux trois lots

Les opérations du contrat `Ajouter`, `Obtenir` et `ObtenirToutes` ne révèlent pas le format. `Ajouter` refuse `null` et un numéro déjà présent; `Obtenir` refuse un numéro non positif et retourne `null` pour l'absence. Le cas d'utilisation détecte les refus ordinaires avant un ajout. Un document invalide n'est pas réécrit. Choisir ses DTO de sortie et de persistance sans donner au domaine une dépendance vers eux. Les tests de refus observent l'absence d'ajout; les tests d'incident observent une exception technique ou le message sûr à la frontière, distincts d'un refus métier.

Les preuves d'aller-retour fichier/JSON et les incidents techniques de fichier sont à réaliser en **S8**. En **S7**, utiliser les données de son propre lot pour les cas du domaine, les refus et le dépôt mémoire. Cette répartition s'applique séparément à chaque personne.
