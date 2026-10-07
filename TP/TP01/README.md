# TP01 — Restaurant : lots individuels autonomes

## Intention

Chaque personne réalise une petite fonctionnalité complète traversant la présentation, Application, le domaine et Infrastructure. Le TP se fait en binôme; une équipe de trois ajoute un troisième lot de réservations. Chaque lot se compile, se teste et s'exécute **sans le code des autres personnes**. Une intégration incomplète ou l'abandon d'un membre ne bloque pas l'évaluation du travail individuel déjà réalisé.

> [!IMPORTANT]
> La génération de code, de tests ou de diagrammes avec une IA est interdite pour les étudiants. GitHub Copilot et IntelliCode doivent être désactivés. Toute aide inhabituelle doit être déclarée dans le journal individuel. Les échéances et modalités de remise sont celles de la plateforme d'enseignement.

## Ce que signifie « lot individuel »

Un **lot** est la fonctionnalité complète attribuée à **une personne** : Commandes, Menu ou Réservations. Pour réaliser cette fonctionnalité, cette personne possède sa propre solution et **ses cinq projets** : `.Domaine`, `.Application`, `.Infrastructure`, `.Terminal` et `.Tests`. Terminal fournit la présentation console; Tests regroupe les vérifications automatisées.

| Personne responsable | Fonctionnalité individuelle | Dossier et solution |
| --- | --- | --- |
| Une personne du binôme | Commandes | `lots/Commandes/TP01_Commandes.slnx` |
| L'autre personne du binôme | Menu | `lots/Menu/TP01_Menu.slnx` |
| Troisième personne, si l'équipe est à trois | Réservations | `lots/Reservations/TP01_Reservations.slnx` |

L'ensemble de ces cinq projets constitue le travail individuel de la personne et se compile, se teste et s'exécute isolément. L'attribution nominative se fait dans AUTHORS.md. Les documents et points communs restent ceux de l'équipe.

## Équipe, charge et attribution

- Équipe de deux, ou de trois lorsque l'effectif l'exige, proposée par les étudiants et approuvée par l'enseignant.
- Environ cinq heures hors classe par personne pour l'ensemble du TP, en plus de l'accompagnement prévu en classe.
- Binôme : une personne réalise **Commandes**, l'autre **Menu**.
- Équipe de trois : les deux mêmes lots, plus **Réservations**, attribué à la troisième personne. Aucun travail supplémentaire n'est imposé au binôme.
- Écrire dans `AUTHORS.md` le lot principal et le journal de chaque personne. L'attribution ne change qu'avec l'accord de l'enseignant.

Les [fiches des lots](LOTS.md) fixent le travail et les frontières; les [données](DONNEES.md) fixent des cas reproductibles. Les exemples des semaines 7 et 8 aident à comprendre les mécanismes, mais ne remplacent pas les règles propres au TP.

## Livrables individuels — 75 points par personne

Chaque lot possède sa propre solution `TP01_NomDuLot.slnx`, les projets `.Domaine`, `.Application`, `.Infrastructure`, `.Terminal` et `.Tests`, cible .NET 10/C# 14. Les projets de départ sont sous `lots/`; ils compilent mais sont à compléter et ne contiennent pas les tests métier attendus.

Ces livrables sont attendus **à la fin du TP**, selon les deux étapes S7/S8 précisées ci-dessous. Chaque personne livre :

1. sa solution individuelle autonome et un README donnant les commandes exactes de compilation, tests et lancement;
2. une console simple dans Terminal, avec classe `Program` et méthode `public static void Main(string[] args)`;
3. un cas d'utilisation dans Application, orchestrant par un contrat de dépôt et utilisant `Result<T>` pour un refus attendu;
4. un domaine comportemental qui protège ses invariants et porte l'algorithme de son lot;
5. un dépôt mémoire en **semaine 7**, puis un dépôt fichier JSON en **semaine 8**, dans Infrastructure, avec DTO de persistance et reconstruction par les invariants du domaine;
6. en **semaine 8**, un chemin configurable, un message utilisateur sûr pour les incidents de fichier et une journalisation technique simple;
7. des tests automatisés normaux, limites, refus attendus et incidents techniques, construisant directement leur sujet;
8. un journal individuel, les liens vers ses commits/PR et une capsule de cinq minutes maximum.

Pour les dépôts (persistance, ex. en JSON), un fichier absent dans un dossier
existant signifie dépôt vide; une liste vide est valide. Un document nul, mal
formé ou contenant des données métier invalides est un incident technique : pas
un refus métier et pas une collection vide silencieuse. Journal et données
désignent des fichiers distincts. Les limites de lecture/réécriture complète
sont documentées; aucune transaction ou gestion des écrivains concurrents n'est
exigée.

## Indépendance obligatoire

- Aucune référence de projet vers le lot d'un coéquipier. Le domaine ne dépend d'aucune autre couche applicative.
- Le lot Commandes reçoit directement ses libellés, prix et quantités; il ne demande pas au lot Menu de fournir ses objets ou son fichier.
- Menu et Réservations utilisent leurs propres données, contrats et dépôts. Réservations ne dépend pas du lot Commandes.
- Les tests de chaque personne fonctionnent avec de vrais objets et son propre dépôt mémoire ou une doublure locale. Aucun conteneur dans les tests unitaires.
- Une composition commune ou un échange entre lots peut rester facultatif, mais ne remplace jamais le parcours individuel autonome et n'ajoute pas de points.
- Un membre ne termine pas le lot d'un autre sans décision explicite de l'enseignant; toute aide est attribuée dans les journaux.

**Vérification de remise :** copier seulement le dossier de son lot dans un dossier temporaire, puis compiler, tester et lancer depuis ce dossier. Les preuves et le hash du commit sont inscrits au journal. La note individuelle porte sur ce lot et ses preuves; une panne d'un autre lot ne retire aucun point individuel. L'enseignant peut utiliser ce dossier isolé pour l'évaluation.

## Livrables communs — 25 points

L'équipe fournit `AUTHORS.md`, un diagramme Mermaid et la procédure d'intégration. Les solutions `TP01_Equipe2.slnx` et `TP01_Equipe3.slnx` regroupent les lots requis, tout en conservant les solutions individuelles. Aucun noyau métier commun n'est requis; ne pas déplacer les entités des lots vers `commun/`.

Les normes de nommage, de code et d'organisation imposées dans le cours s'appliquent directement; aucun document de conventions n'est à rédiger. Leur respect est vérifié dans le code et la structure des projets. Consigner la procédure d'intégration dans ce README commun : commandes de compilation et de tests de la solution d'équipe, PR relues et commit final. Les interfaces des dépôts constituent les contrats de chaque lot; aucun document de contrats séparé n'est demandé.

Le diagramme montre les lots requis et leurs quatre couches. Chaque personne indique la partie qu'elle a préparée/revue. Le code des autres lots n'est pas nécessaire pour démontrer les règles de dépendance de son propre lot. La grille commune et la grille individuelle sont séparées dans [GRILLE.md](GRILLE.md).

## Jalons des semaines 7 et 8

| Étape | Travail et vérifications à réaliser |
| --- | --- |
| **Semaine 7 — mémoire** | Attribution du lot, invariants et algorithme du domaine, contrat et dépôt mémoire, cas normaux/limites/refus testés avec les propres données de la personne |
| **Semaine 8 — fichiers** | Après l'introduction de JSON/YAML et des couches : dépôt JSON, DTO de persistance et conversions, configuration, incidents/journalisation et validation de sauvegarde/relecture |

**La validation « sauvegarder, puis relire avec une nouvelle instance du dépôt » concerne le dépôt JSON et est à réaliser en semaine 8. Elle n'est pas demandée comme résultat de la semaine 7.** Les vérifications de fichiers et de journalisation de DONNEES.md suivent la même étape S8. Le dépôt mémoire permet déjà de tester en S7 les règles du domaine et les opérations métier; une nouvelle instance de ce dépôt mémoire commence vide.

Pour la validation JSON, enregistrer avec un premier objet dépôt, créer un **deuxième objet dépôt JSON configuré sur le même fichier**, puis lui faire relire les données. Vérifier les valeurs et l'état des objets reconstruits. Les deux objets peuvent être créés successivement dans un même test automatisé; aucun redémarrage du programme n'est exigé. Les étapes précises et les données attendues sont dans [DONNEES.md](DONNEES.md).

1. **Semaine 7 — contrats et attribution** : affecter les lots, vérifier les départs individuels, appliquer les règles Git du cours et préciser les contrats de dépôt de chaque lot dans les interfaces fournies. La semaine 7 étudie exceptions, collections et Repository en mémoire. La persistance, la configuration et la journalisation seront introduites en semaine 8.
2. **Semaine 7 — premier comportement testé** : implanter le domaine, l'algorithme et le dépôt mémoire de son lot; préparer les données de ses cas de test. Couvrir un cas normal, une borne et un refus.
3. **Semaine 8 — fichiers et structure complète** : introduire la persistance et la configuration, réinvestir le découpage présentation / Application / domaine / Infrastructure, implanter JSON, les conversions et la journalisation simple, puis conserver les comportements avec les tests.
4. **Semaine 8 — présentation et intégration** : relier la console, vérifier son lot isolément et fusionner ses changements par une PR relue. La revue ne transfère pas la responsabilité du lot.

Utiliser `dev` pour l'intégration et `fonctionnalite/nom-court` depuis `dev`. Compiler/tester avant et après fusion. Les sorties `bin/`, `obj/`, journaux et données générées ne sont pas suivies; la configuration sans secret et les petits exemples nécessaires le sont.

## Barème et remise

| Composante | Nature | Points |
|---|---|---:|
| Attribution, contrats et règles de dépendance | Commune | 10 |
| Diagramme Mermaid | Commune | 5 |
| Procédure et preuves d'intégration | Commune | 10 |
| Lot vertical fonctionnel et autonome | Individuelle | 40 |
| Tests et contrats du lot | Individuelle | 15 |
| Git, attribution et revue | Individuelle | 10 |
| Capsule individuelle | Individuelle | 10 |
| **Total par personne** | **25 communs + 75 individuels** | **100** |

La capsule est non répertoriée sur YouTube; son lien et le commit présenté figurent au journal et sont remis sur Léa 48 heures avant le code final. La vidéo demeure accessible six mois après la fin du TP. Aucun contenu après cinq minutes n'est évalué. Le dépôt final et les auteurs déclarés doivent correspondre.
