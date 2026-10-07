# Exercice 1

## Mission et cadre

Construire une interface Windows de prise de commande sans réécrire les règles du domaine. Cette activité ramassée, repérée comme EG01, occupe les séances 17 (3 h) et 18 (2 h). Les échéances, modalités et pondération sont sur la plateforme d'enseignement.

La solution `S09E01_Commandes` fournit un domaine, un service Application, des dépôts mémoire/JSON, un journal, une console et leurs tests. Votre travail porte sur la présentation WinForms : formulaires, événements, conversion de saisie, affichage, messages et ergonomie. Ce départ est distinct du TP01 et ne dépend pas du travail d'un coéquipier.

## Préalables et contrats fournis

Sous Windows, utiliser Visual Studio avec le développement desktop .NET et le SDK .NET 10. Les bibliothèques ciblent `net10.0`; le projet `.WinForms` cible `net10.0-windows` avec C# 14. La solution `.Portable.slnx` compile les couches et la console sans WinForms.

La classe `ServiceCommandes` reçoit les contrats `IDepotCommandes` et `ICatalogueProduits`, non nuls. Ses opérations sont :

- `Consulter()` : DTO stable du numéro courant, des lignes, du total et de l'état enregistré;
- `ConsulterProduits()` : liste de produits de sortie;
- `Creer(int numero)` : nouvelle commande, numéro positif ou refus attendu;
- `AjouterProduit(int numeroProduit, int quantite)` : ajout si produit connu et quantité positive; sinon refus attendu sans mutation;
- `Enregistrer()` : refuse une commande vide ou un numéro déjà enregistré; un succès intervient après l'ajout effectif; un incident technique se propage et conserve le brouillon.

Le domaine protège aussi ses invariants lors d'un appel direct : numéro positif, nom non nul/non blanc, prix non négatif, quantité positive, confirmation non vide. Les collections et copies fournies empêchent les accès de la vue de modifier directement l'état. `Result<T>` conserve EstSucces, Valeur, MessageErreur, Succes et Echec; Valeur n'est lue qu'après succès.

## Séance 17 — Construire et relier

1. Depuis `dev`, créer la branche Git `fonctionnalite/winforms`. Compiler la solution et exécuter les tests fournis. Lancer la console : deux Soupes à 6,50 et un Sandwich à 9,00 donnent **22,00**. Aucun conteneur n'est utilisé dans ces tests.
2. Dans le designer du projet `S09E01_Commandes.WinForms`, construire la classe partielle `FormCommande`. Conserver le constructeur sans paramètres pour le designer, puis celui recevant `ServiceCommandes` et `IJournalIncidents`, tous deux non nuls au lancement. Appeler `InitializeComponent()` avant d'utiliser les contrôles.
3. Présenter les zones suivantes : numéro proposé pour une **nouvelle** commande, numéro **courant**, choix du produit, quantité, bouton Ajouter, grille de lignes en lecture seule, total, bouton Enregistrer et message d'état. Utiliser des Name explicites et des libellés Text compréhensibles. Les contrôles restent des données membres privées préfixées `m_`.
4. Dans le point de composition `Program`, construire d'abord le service et le formulaire manuellement; conserver le mode `--assemblage=cadriciel` pour comparer les mêmes objets avec AddScoped, Build, CreateScope et GetRequiredService. La portée reste vivante pendant Application.Run. Le programme fourni utilise une classe Program et Main, avec STAThread et ApplicationConfiguration.Initialize.
5. Associer chaque événement Click **une seule fois**. Le gestionnaire Ajouter vérifie la sélection et convertit le texte de quantité avec TryParse. Un texte invalide affiche un message, rétablit le focus et ne demande aucun ajout. Une valeur entière est transmise à la méthode AjouterProduit : le formulaire ne remplace pas la règle métier.
6. À partir du résultat, afficher le refus sans lire Valeur, ou rafraîchir lignes, total et état depuis le DTO. La grille ne calcule pas le total. Elle interdit l'ajout/suppression/édition directe de lignes. Le bouton Nouvelle appelle Creer après confirmation éventuelle de l'abandon du brouillon.

Point de contrôle : un clic produit une seule ligne; la sortie console et la sortie graphique du même cas montrent le même total. Les tests fournis restent verts.

## Séance 18 — Messages, état et ergonomie

1. Le gestionnaire Enregistrer n'annonce une réussite qu'après un résultat de succès. Un refus conserve la saisie et les lignes. Intercepter les incidents IO, accès refusé, JSON et données invalides à la frontière; journaliser opération/type sans message brut, puis afficher un message sûr sans trace, chemin complet ni secret. Aucun accès fichier dans le gestionnaire.
2. Désactiver Enregistrer pendant l'appel, puis rétablir son état selon le DTO dans un finally. Après succès, empêcher un ajout à la commande confirmée et proposer une nouvelle commande. Après incident, conserver le brouillon et permettre un nouvel essai. Les traitements ici sont courts et synchrones; async/await n'est pas exigé.
3. Protéger les lignes non enregistrées lors de Nouvelle et de FormClosing. Une annulation doit laisser le brouillon intact; la fermeture refusée utilise `e.Cancel = true`. Distinguer Show et ShowDialog dans `DECISIONS.md`; une fenêtre secondaire supplémentaire n'est pas obligatoire.
4. Organiser les zones avec des conteneurs. Vérifier Dock, Anchor, MinimumSize, marges, échelle et redimensionnement. Prévoir TabIndex dans les conteneurs, une touche d'accès, AcceptButton et des noms accessibles. Un message ne dépend pas uniquement de la couleur.
5. Appliquer le [protocole Windows](VERIFICATION_WINDOWS.md) avec une capture du cas normal et d'une erreur de saisie, puis consigner les observations. Ajouter un cas de non-régression ciblant la création d'une commande après enregistrement : commande neuve vide, commande précédente toujours consultable au dépôt. Construire le service directement avec un dépôt mémoire.
6. Expliquer dans `DECISIONS.md` le parcours clic → service → domaine → DTO, la distinction entre saisie/refus/incident, le propriétaire de l'assemblage et un choix ergonomique vérifié. Compiler/tester avant la fusion vers `dev`, puis recommencer sur `dev`.

## Livrables et critères observables

- Solution complète, noms inchangés entre départ et remise; code de présentation et test ajouté.
- `DECISIONS.md`, résultats de tests et protocole Windows complété avec commit de référence.
- Deux captures utiles, sans données sensibles, et commandes reproductibles.
- Interface utilisable : ajout unique, total correct, refus compréhensibles, incident distinct, état préservé et navigation/redimensionnement cohérents.
- Responsabilités respectées : pas de total métier, invariant, dépôt concret ou lecture JSON dans les événements; pas de WinForms dans Application ou domaine.

<details>
<summary>Rappels des semaines précédentes</summary>

Revoir les [fichiers et incidents en semaine 7](../semaine-07/E03-depot-memoire.md) et l'[architecture en semaine 8](../semaine-08/E03-viewmodel-erreurs.md). API : Result, DTO, injection par constructeur, port de dépôt. Une erreur d'enregistrement peut-elle autoriser l'écran à annoncer un succès?

</details>
