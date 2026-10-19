# Guide de révision active - Semaine 8

## Fichiers, configuration et couches

En semaine 7 : exceptions, collections, dépôt mémoire et Result<T>.
Cette semaine : JSON/YAML, dépôt fichier, configuration, incidents techniques et
journalisation; première présentation explicite des couches et des conversions.

Choisissez deux questions, répondez sans notes, puis vérifiez dans les diapos.
Revenez plus tard aux questions hésitantes. Ce guide est facultatif et ne demande
aucun document à remettre; il ne s'ajoute pas au temps de travail prévu pour le TP01.

## 1. Sauvegarder et reconstruire

**Entité → DTO de persistance → texte → fichier**, puis le parcours inverse.
Sérialiser produit du texte. Désérialiser un texte syntaxiquement valide ne garantit
pas ses invariants : la reconstruction passe par le constructeur du domaine.
Dans l'exemple, le numéro est strictement positif et le titre non vide.

- Quelle étape serait absente si le programme sérialisait sans écrire?
- Pourquoi relire avec un deuxième dépôt configuré sur le même fichier?
- Pourquoi comparer les valeurs plutôt que les références des objets?
- Quels détails changent entre JSON et YAML? Quel contrat reste identique?

**Vérification courte** : enregistrer une réservation avec un premier dépôt,
la relire avec un second, puis comparer son numéro et son titre. Les deux objets
peuvent être créés dans le même test, sans redémarrer le programme.

## 2. Distinguer absence et incident

| Situation | Politique du dépôt de l'exemple |
|---|---|
| Fichier absent dans un dossier existant | Collection vide |
| Liste `[]` | Collection vide valide |
| Fichier vide, `null`, JSON mal formé | Incident technique |
| Entrée nulle, invariant violé, numéro répété | Incident technique |
| Dossier absent, accès refusé | Incident technique |

Un incident interrompt la lecture ou l'ajout. Le fichier invalide est conservé;
on ne le remplace jamais silencieusement par une liste vide.

- Pourquoi un `catch` général qui retourne une liste vide serait-il dangereux?
- Que doit afficher la présentation après un échec de sauvegarde?
- Quelle différence entre un numéro déjà utilisé dans une demande et un fichier illisible?
- Pourquoi séparer les diagnostics techniques du fichier de données?

Le refus attendu utilise Result<T> dans Application. L'incident technique se
propage jusqu'à une frontière qui affiche un message sûr et journalise un diagnostic
limité : date, opération et catégorie d'exception. Un journal indisponible ne doit
pas masquer le message utilisateur. Ne pas exposer secrets, contenu personnel,
chemins complets ou traces d'appel.

## 3. Configurer au démarrage

Dans les diapos, `appsettings.json` indique le format et le chemin des données,
ainsi que le seuil `Logging:LogLevel:Default` pour ILogger. Dans le départ des
exercices courts, `Journal:Chemin` conserve le journal fichier simple existant.
Le fichier est copié dans la sortie du projet Terminal; l'exemple
résout explicitement les chemins relatifs depuis le dossier de l'exécutable.

Priorité par défaut du Host, de la plus forte à la plus faible : arguments,
variables d'environnement, secrets utilisateur en Development, fichier
appsettings de l'environnement, appsettings.json, configuration chaînée.
Une source plus prioritaire remplace une valeur précédente.
Les secrets utilisateur sont locaux et hors du dépôt, mais non chiffrés; le
projet les associe par UserSecretsId. En production, les secrets sont fournis
séparément au déploiement, par variables injectées ou coffre de secrets.

- Où chercher `reservations.json` avec un chemin relatif dans cet exemple?
- Quel format choisir si le fichier indique json et l'argument indique yaml?
- Que représente `Donnees__Format` dans une variable d'environnement?
- Pourquoi le domaine ne lit-il pas la configuration?

La racine de composition construit le dépôt et injecte le contrat dans le service.
L'assemblage manuel précède sa variante par conteneur : inscriptions dans
builder.Services, Build, CreateScope puis GetRequiredService. Les tests construisent
directement les sujets, sans les résoudre dans le conteneur.

## 4. Situer les responsabilités et les objets

| Couche | Responsabilités et objets |
|---|---|
| présentation | Saisie, affichage, messages; ViewModel si la vue en a besoin |
| Application | Orchestration, contrat de dépôt, Result<T>, DTO de sortie |
| domaine | Entité Reservation, identité et invariants |
| Infrastructure | JSON/YAML, dépôts concrets, DTO de persistance, journal technique |

La présentation référence Application; Application référence le domaine;
Infrastructure référence les contrats d'Application et le domaine. Le domaine ne
référence aucune autre couche applicative. Program connaît Infrastructure pour
l'assemblage; ce rôle ne permet pas à chaque vue de construire un dépôt concret.
Un appel à l'exécution n'est pas une référence de compilation.

POCO décrit un objet C# simple, pas une couche. Une entité, un DTO ou un ViewModel
peuvent être des POCO. Le DTO de persistance appartient à Infrastructure, le DTO de
sortie à Application et le ViewModel à la présentation. Le domaine ne dépend d'aucun
DTO extérieur. Une conversion courte peut vivre dans le DTO : constructeur prenant
l'entité et VersEntite(). Une conversion partagée ou complexe mérite une classe dédiée.

- Où placer la conversion du DTO JSON vers Reservation? Pourquoi?
- Où construire le libellé « Réservation 1 : Réunion »?
- Quand peut-on utiliser le DTO de sortie directement dans la vue?
- Pourquoi créer quatre dossiers ne suffit-il pas à séparer les responsabilités?

## 5. Transférer au TP01

Les exercices durent environ **30 minutes au total**; ils préparent le travail
sur votre lot autonome. Utilisez ensuite l'énoncé officiel pour ses règles propres.

Vérifiez la sauvegarde/relecture de votre dépôt JSON, ses données invalides,
son chemin configurable et son message d'incident. Une nouvelle instance du dépôt
mémoire n'est pas une preuve de persistance. Un dépôt simple qui relit et réécrit
le fichier ne garantit ni transaction ni accès concurrents. Si la sauvegarde échoue
après une modification en mémoire, revérifiez l'état avant de réessayer.

[Énoncé du TP01](../../TPs/420-W30-SF-v2-TP01/README.md).

## Lectures ciblées

- Chapitre 7 : exceptions et incidents techniques; chapitre 10 : journalisation avec ILogger.
- Chapitre 10 : JSON/YAML, dépôt fichier et configuration.
- Section 12.4 : Repository; chapitre 13 : couches et objets aux frontières.
- Pour la semaine 9, chapitre 14 : rôle de WinForms, structure d'un formulaire,
  contrôles et événements, messages et ergonomie de base.
