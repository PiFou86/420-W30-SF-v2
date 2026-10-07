# Guide de révision active - Semaine 7

## Exceptions, collections et Repository en mémoire

Planification : plan de cours `PC_420-W30-SF_A2026-JFD-PFL_En_approbation.docx`, tableau hebdomadaire, semaine 7. Lectures indiquées : 7, 8 et 12.4. Cette semaine prépare le domaine, les tests et les dépôts mémoire du TP01. Les fichiers, la configuration et la journalisation seront étudiés en semaine 8.

## Charge des exercices

Le parcours obligatoire de S7 est allégé à environ **75 minutes en classe** : 15 minutes d’exceptions, 15 minutes de collections et 45 minutes de dépôt mémoire. Les prédictions et approfondissements de ce guide restent des ressources de révision; ils ne sont pas une nouvelle liste de tâches à terminer en plus du TP01. Le client, les traces et l’assemblage sont fournis dans le départ.

## Méthode

Fermer les notes, prédire un parcours ou un état, puis expliquer pourquoi. Vérifier avec une diapo, le code ou un test. Refaire une question différente après un délai. Cocher la grille sur la base d’une preuve; elle ne fournit pas de réponses. Trois courts passages de dix minutes peuvent servir à cibler les difficultés, sans ajouter une activité à remettre.

## 1. Précondition, refus attendu et incident

Classer sans notes : numéro 0, titre nul, titre blanc, plage occupée, objet annoncé connu mais absent et ressource technique inaccessible. Une précondition protège le contrat d’un appel; un refus métier normal utilise le mécanisme explicite introduit en S7, `Result<T>` (notes, section 7.2, pages 106 à 108). L’existence d’une exception personnalisée ne rend pas automatiquement un refus prévisible anormal.

- Pourquoi vérifier un paramètre avant de changer l’état de l’objet?
- Quand choisir ArgumentNullException, ArgumentException ou ArgumentOutOfRangeException?
- Pourquoi vérifier le type et ParamName dans un test plutôt que « une exception quelconque »?
- Un utilisateur qui cherche un numéro absent a-t-il violé une précondition?

Preuve : tests du constructeur de Reservation pour numéro 0/-1, titre nul/vide/blanc et cas valide. Nommer l’invariant protégé.

## 2. Tracer try, catch, finally et throw

Tracer l’extrait qui construit une réservation de numéro 0, puis affiche « Créée », traite ArgumentOutOfRangeException et affiche « Suite ». Refaire avec un numéro valide.

- À quelle instruction l’exécution normale s’arrête-t-elle?
- Comment le runtime cherche-t-il un catch compatible dans les appels?
- Pourquoi placer le catch précis avant le général?
- Quand finally s’exécute-t-il? Traite-t-il l’exception?
- Qu’arrive-t-il à une exception non traitée après finally?
- Pourquoi utiliser throw; pour relancer? Quel risque porte throw ex;?
- Que perd-on avec un catch vide ou une valeur qui suggère un faux succès?

Repère : finally accompagne la sortie du try, y compris par return ou par exception. Une interruption forcée du processus peut empêcher son exécution. Il n’est pas une garantie de transaction. Console.Error est un flux d’erreur visible, pas un canal privé.

Preuve : écrire les traces avant de lancer le Terminal, puis comparer avec les tests de ParcoursExceptions. Distinguer sortie normale et sortie d’erreur.

## 3. Justifier une exception personnalisée

Reconstituer ReservationIntrouvableException : héritage de Exception, propriété Numero, constructeur recevant un numéro strictement positif et appel au constructeur de base. Pourquoi un type standard ne suffit-il pas dans ce parcours précis?

- Quelle information peut lire l’appelant sans analyser le message?
- Quelle différence sépare Obtenir, qui retourne null normalement, et Exiger, qui exige un objet annoncé connu?
- À quel endroit le parcours Exiger devient-il pertinent? Pourquoi éviter ce parcours pour une recherche utilisateur ordinaire?
- Que préserve le constructeur avec une cause dans InnerException?
- Dans notre surcharge, que signifie une cause nulle?

Preuve : tests du numéro conservé, du numéro invalide et de l’identité de la cause. Tester séparément null retourné par le dépôt et exception levée par ConsultationReservations.Exiger.

## 4. Choisir une collection

Pour chaque choix, nommer l’opération prioritaire, l’ordre et la politique de doublons.

| Situation | Choix à justifier |
| --- | --- |
| Titres dans l’ordre d’ajout, doublons permis | Liste |
| Réservation recherchée par numéro | Dictionnaire |
| Numéros distincts, sans tri requis | Ensemble par hachage |
| Numéros distincts, toujours parcourus dans l’ordre | Ensemble trié |
| Réservations parcourues par clés triées | Dictionnaire trié |
| Demandes dans l’ordre d’arrivée | File |
| Historique consulté depuis le dernier ajout | Pile |
| Insertion près d’un nœud déjà connu | Liste chaînée |

- Que produisent Add, TryAdd et l’affectation par clé pour une clé déjà présente?
- Pourquoi TryGetValue est-il utile lorsque la clé peut manquer?
- Deux objets ayant le même numéro sont-ils forcément égaux pour HashSet<Reservation>?
- Quel comparateur décide du tri et des doublons dans les collections triées?
- Que font Peek, Dequeue et Pop? Comment traiter une collection vide?
- Pourquoi le nœud connu est-il important pour le coût de LinkedList<T>?

Preuve : prédire et exécuter List, HashSet, Queue et Stack avec les valeurs 17, 18, 17. Ne pas utiliser l’ordre d’un HashSet ou d’un Dictionary comme garantie de tri.

## 5. Coûts, parcours et encapsulation

O(1) : coût qui ne grandit pas proportionnellement à la taille. O(n) : parcours linéaire. O(log n) : croissance plus lente associée ici aux collections triées. L’ajout à une liste est O(1) amorti; le redimensionnement occasionnel coûte davantage. La recherche par hachage est O(1) en moyenne, avec limites liées aux collisions. Ces repères ne promettent pas une durée exacte pour chaque appel.

- Quelle différence entre accéder à un index et rechercher un objet dans une liste?
- Un dictionnaire recherche-t-il toutes ses valeurs en O(1)?
- IEnumerable<T> promet-il un index ou la possibilité d’ajouter?
- Une requête LINQ différée voit-elle un ajout réalisé avant son parcours?
- Que fixe ToArray() au moment où on l’appelle?
- Que protège une copie de la collection? Que ne protège-t-elle pas avec des objets mutables?
- Une interface IReadOnlyCollection suffit-elle à rendre les objets immuables?

Preuve : conserver une copie du dépôt, ajouter ensuite une réservation et modifier une case de la copie. Comparer les résultats. Reservation est immuable dans l’exemple : ses valeurs ne changent plus après construction.

## 6. Repository spécifique en mémoire

Partir du client qui connaît directement Dictionary<int, Reservation>. Quels détails et quelles règles peut-il contourner? Reconstruire ensuite IDepotReservations et son implantation.

Contrat de l’exemple : Ajouter refuse null et le doublon sans remplacer l’ancien objet; Obtenir refuse un numéro inférieur ou égal à zéro, mais retourne null pour un numéro strictement positif absent; ObtenirToutes retourne une copie de structure, sans ordre garanti. Une nouvelle instance du dépôt commence vide.

- Pourquoi l’interface doit-elle décrire des opérations utiles plutôt qu’exposer le dictionnaire?
- Pourquoi Repository n’est-il pas un des 23 patrons GoF?
- Comment construire le client avec une abstraction déjà configurée?
- Quels tests prouvent que le refus d’un doublon conserve le premier objet?
- Qu’observent deux clients du même dépôt? Et deux dépôts différents?
- Pourquoi les tests construisent-ils leurs sujets sans conteneur ni fichier?
- Que restera-t-il après l’arrêt de cette application?

Preuve : cas valide, null, numéro 0/-1, absence, doublon, dépôt vide, copie indépendante et deux instances distinctes. Le dépôt mémoire n’offre ni persistance après arrêt ni contrat de concurrence. Commencer spécifique; la généralisation IRepository<T> vient plus tard.

## TP01 et suite

Lire l’[énoncé officiel](../../TPs/420-W30-SF-v2-TP01/README.md), les fiches de lots et la grille. Binôme : Commandes et Menu; à trois, ajouter Réservations. Chacun garde une solution compilable et testable isolément. Cette semaine : attribution, invariants, algorithme, tests et dépôt mémoire. Semaine 8 : fichiers, configuration, journalisation et structure complète de l’application. Modalités et échéances : plateforme d’enseignement.

## Lectures à faire avant la semaine 8

Repères du plan de cours :

- **Chapitre 7** : sections sur les incidents techniques et la journalisation.
- **Chapitre 10** : JSON/YAML, dépôt fichier et configuration avec appsettings.json.
- **Section 12.4** : Repository, passage d’un dépôt mémoire à un dépôt fichier.
- **Chapitre 13** : couches, entités, DTO, ViewModels et conversions.

Le prochain cours introduit configuration et persistance JSON/YAML, dépôt fichier simple, incidents/journalisation et structure complète présentation / Application / domaine / Infrastructure. Ces lectures préparent les sujets; elles ne constituent pas une nouvelle activité à remettre.

Références de contenu : notes, Exceptions; Collections courantes dans C# moderne; Repository dans Patrons de conception. Les repères 7, 8 et 12.4 sont ceux du plan de cours. Ils n’imposent pas une renumérotation silencieuse des fichiers LaTeX.

Documentation : [exceptions personnalisées](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions), [choisir une collection](https://learn.microsoft.com/en-us/dotnet/standard/collections/selecting-a-collection-class), [collections et structures](https://learn.microsoft.com/en-us/dotnet/standard/collections/). Les exemples sont adaptés aux conventions C# du cours.
