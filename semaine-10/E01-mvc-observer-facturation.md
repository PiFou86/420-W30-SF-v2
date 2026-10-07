# Exercice 1

## Mission et progression

Réusiner une prise de commande WinForms pour séparer vue, contrôleur et modèle manipulé, puis actualiser une facture de consultation avec Observer. Cette activité ramassée est EG02 : **huit heures aux séances 19 (3 h), 20 (2 h) et 21 (3 h)**. La semaine 10 contient les cinq premières heures; la consolidation se poursuit en semaine 11. Les échéances, modalités et pondération sont sur la plateforme d’enseignement.

Le départ `S10E01_Facturation` est autonome. Il fournit la présentation fonctionnelle de semaine 9, les services, le domaine, les dépôts et les tests antérieurs, ainsi que la règle de facture et ses tests. Les rôles MVC et les abonnements sont à construire. Ne pas modifier votre remise d’EG01 ni dépendre d’un projet de coéquipier. La comparaison MVVM des diapos est un repère; aucune implantation MVVM n’est demandée.

## Ce qui est fourni

- Projets Domaine / Application / Infrastructure, console et tests de la semaine 9 conservés sous les nouveaux noms.
- Projet `.Presentation` portable : interfaces `IVueCommande` et `IObservateurCommande`, sortie `EtatCommandeDto`, canevas des classes `ModeleCommande` et `ControleurCommande`.
- Projet WinForms : formulaire de commande à réusiner, canevas de la fenêtre de facture.
- Classe `Facture` et classe `ServiceFacturation` : calcul fourni et déterministe. **Frais pédagogiques de 10 %, sans modèle fiscal réel.** Sous-total arrondi à deux décimales, frais arrondis ensuite; AwayFromZero. Sous-total négatif refusé; zéro accepté. Un dépassement de decimal n’est pas masqué.

La facture de sortie est provisoire tant que la commande n’est pas enregistrée. Soupe × 2 et Sandwich × 1 donnent un sous-total de 22,00, des frais de 2,20 et un total de 24,20. Les calculs ne sont pas recopiés dans les fenêtres.

## Contrats à respecter

Les dépendances du modèle et du contrôleur, les observateurs et les composantes de `EtatCommandeDto` sont non nuls. Commande et facturation partagent le **même objet ServiceCommandes**. Les interfaces ne dépendent pas de Form. Les préconditions du domaine et des dépôts de S9 restent protégées et testées.

La classe `ModeleCommande` expose :

- `Consulter()` et `ConsulterProduits()` : sorties stables sans mutation;
- `Attacher(IObservateurCommande)` : observateur non nul, même instance déjà attachée refusée, résultat IDisposable; pas de notification initiale automatique;
- `Nouvelle(int)`, `Ajouter(int, int)` et `Enregistrer()` : demander au service et publier seulement après succès; les refus ne mutent pas l’état et les incidents techniques se propagent vers la frontière de présentation.

La classe `ControleurCommande` reçoit modèle, vue et journal non nuls. Elle possède l’abonnement de la vue principale et demande un affichage initial. Ses méthodes Nouvelle/Ajouter/Enregistrer coordonnent les demandes, les refus et les incidents. Après Dispose, elles refusent l’appel par ObjectDisposedException. Un second Dispose ne fait rien. Si l’affichage initial échoue, aucun abonnement ne reste attaché.

L’observateur reçoit un instantané contenant commande et facture. Le tableau des abonnés stabilise le parcours : un nouvel abonné commence au changement suivant; un retrait ne modifie pas la liste déjà capturée. Dispose retire l’instance et libère les références qu’il possède. Un observateur ne déclenche pas une commande pendant sa notification; cette réentrée est refusée.

## Séance 19 — Séparer MVC (3 h)

1. Créer depuis `dev` la branche Git `fonctionnalite/mvc-facturation`. Compiler et tester le départ; lancer la console et la fenêtre initiale sous Windows. Identifier les responsabilités à déplacer, sans réécrire le domaine ni les dépôts.
2. Dans le projet `.Presentation`, construire la classe `ControleurCommande` autour du modèle et du contrat de vue. Dans le projet WinForms, faire implanter `IVueCommande` par `FormCommande`. Remplacer le constructeur injecté de S9 par le constructeur sans paramètres et la méthode `Relier(ControleurCommande, IReadOnlyCollection<ProduitDto>)`, collaborateurs non nuls et lien unique. Adapter le fichier Program du projet WinForms : créer le formulaire, le contrôleur, puis les relier avant Application.Run.
3. Garder conversion de saisie, focus et confirmation d’abandon dans la vue. Déplacer les appels aux opérations et les messages de refus/incident dans le contrôleur. Conserver l’événement Click unique, la grille en lecture seule et l’état des boutons.
4. Adapter le projet Terminal avec une classe `VueTerminal` derrière le même contrat, pour démontrer le parcours sans fenêtre. Une vue espion manuelle enregistre ses sorties pour les tests; le conteneur ne résout pas le sujet testé.

Point de contrôle : les règles antérieures et le cas console restent identiques; le total n’est jamais recalculé dans la vue ou le contrôleur.

## Séance 20 — Relier les observateurs (2 h)

1. Dans la classe `ModeleCommande`, implanter d’abord la liste explicite des observateurs, la méthode Attacher et l’objet de désabonnement. Nommer son propriétaire. Initialiser chaque nouvelle vue séparément avec Consulter.
2. Après une opération réussie, construire un seul `EtatCommandeDto` puis parcourir une copie des abonnés. La vue principale et la vue de facture reçoivent le même instantané une fois chacune. Ne pas ajouter un second Actualiser dans le contrôleur.
3. Dans le projet WinForms, compléter `FormFacture` : le constructeur exige un modèle non nul, affiche l’état courant et conserve l’abonnement. Son Dispose le libère. Ouvrir cette fenêtre avec Show(owner), pour continuer à saisir dans la fenêtre principale. La racine de composition crée la fenêtre secondaire; la vue de commande annonce seulement la demande d’ouverture.
4. Écrire les premiers tests de présentation : deux observateurs, ajout réussi, refus sans notification et instantané de facture à 24,20. Construire directement les collaborateurs avec un dépôt mémoire.

Point de contrôle : ouvrir la facture après une saisie affiche l’état courant; les ajouts suivants la mettent à jour. MVC et Observer restent deux problèmes distincts : responsabilités et diffusion du changement.

## Suite à la séance 21 — Consolider (3 h en semaine 11)

1. Vérifier fermeture, réouverture, Dispose répété et absence de doublons. Tester le refus d’un abonnement nul ou dupliqué et les dépendances nulles. Vérifier qu’une initialisation ratée retire son abonnement et qu’un contrôleur disposé ne traite plus d’action.
2. Tester un incident du dépôt et le brouillon conservé. Intercepter IO / accès refusé / JSON / données invalides dans le contrôleur, journaliser opération/type et afficher un message sûr sans détail technique. Un refus n’est pas une panne et une vue ne reçoit pas un état enregistré avant réussite.
3. Compléter la politique de notification : une vue défaillante est journalisée à la frontière d’affichage, les autres vues continuent, l’opération réussie n’est pas annulée. Refuser la réentrée d’une commande pendant Actualiser. Le cas reste synchrone, sur un seul fil d’exécution.
4. Après la version explicite, compléter le petit objet `SignalCommande` avec event et comparer `+=`, `-=` et Invoke. Conserver le même délégué lors du retrait. Cette variante isolée ne remplace pas obligatoirement le graphe MVC; expliquer sa politique d’exception. Reconnaître IObservable/IObserver sans développer un cadriciel de flux.
5. Conserver les modes manuel et cadriciel dans Program. Dans le mode DI, une portée conserve les mêmes services et la même vue principale; le contrat IVueCommande désigne le formulaire déjà créé. Garder la portée vivante pendant Application.Run.
6. Compléter [le protocole Windows](VERIFICATION_WINDOWS.md), puis compiler/tester avant fusion dans `dev` et recommencer sur `dev`.

## Livrables et critères observables

- Solution complète autonome, mêmes noms que le départ; code de présentation, tests et commandes reproductibles.
- `DECISIONS.md` : responsabilités MVC/couches, mécanisme Observer, propriétaire/durée des abonnements, politique d’erreur, réentrée et comparaison avec event.
- Tests observables : état, nombre de notifications, refus/incident et durée des abonnements; vrais services et doublure manuelle de vue.
- Protocole Windows complété et deux captures : commande/facture cohérentes, puis saisie invalide ou incident sûr.
- Aucune règle métier ou lecture de fichier dans les fenêtres; Application et domaine sans référence WinForms. Les préconditions des nouvelles méthodes de vue (état non nul, messages non blancs, Relier unique) sont vérifiées sous Windows dans le protocole.

<details>
<summary>Rappels utiles</summary>

Revoir [WinForms en S9](../semaine-09/E01-prise-commande-winforms.md), [DTO et erreurs en S8](../semaine-08/E03-viewmodel-erreurs.md) et la fiche Git de semaine 3 pour les branches. API à réinvestir : Result, DTO, IDisposable, injection par constructeur. Un changement d’affichage est-il un nouveau calcul métier?

</details>
