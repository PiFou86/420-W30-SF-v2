# Guide de révision active — Semaine 3

## Branches Git, fabriques, injection et conteneur .NET

Ce guide relie deux formes de changement : faire évoluer l'historique avec des branches et faire varier les collaborateurs d'un objet sans le coupler à leur création. Répondez d'abord de mémoire, puis vérifiez l'état Git ou le code correspondant.

## Capacités à vérifier

À la fin de votre étude, vous devriez pouvoir :

- expliquer qu'une branche est un nom qui pointe vers un commit;
- relier les commandes Git à l'état qu'elles consultent ou modifient;
- créer, publier, récupérer et fusionner une branche;
- résoudre un conflit en construisant le contenu final;
- repérer une création directe qui produit un couplage fort;
- distinguer Abstract Factory de Factory Method;
- injecter une dépendance par constructeur;
- distinguer DI, inversion de contrôle et conteneur;
- expliquer les cycles `Transient`, `Scoped` et `Singleton`.

## Carte des notions

| Notion | Question centrale | Repère |
|---|---|---|
| Branche Git | Quelle intention de changement veut-on isoler? | Le nom de branche pointe vers un commit et avance avec les nouveaux commits. |
| Fusion | Comment combiner deux historiques? | Se placer sur la destination, puis fusionner la source. |
| Conflit | Quel contenu final doit réunir les intentions compatibles? | Les marqueurs signalent une décision humaine à prendre. |
| Factory | Qui choisit ou redéfinit la création d'objets? | La création est séparée de l'utilisation. |
| DI | Comment l'objet reçoit-il son collaborateur? | Le constructeur annonce la dépendance. |
| IoC | Qui contrôle la création et l'assemblage? | `Program.cs` ou le conteneur construit le graphe. |

## Repère 1 — Les états Git avant la branche

Avant d'agir, distinguez :

- le répertoire de travail;
- l'index;
- le commit local courant;
- la branche locale;
- la référence distante connue localement.

`git status` indique notamment la branche active, les changements du répertoire de travail et ce qui se trouve dans l'index. `git fetch` met à jour les références distantes connues sans modifier directement votre travail.

## Repère 2 — Créer et publier une branche

Parcours typique :

```bash
git status
git switch main
git pull --ff-only
git switch -c fonctionnalite/nom-court
# modifier et tester
git add chemin/du/fichier
git commit -m "message qui explique l'intention"
git push -u origin fonctionnalite/nom-court
```

Chaque commande répond à un état précis. Ne mémorisez pas seulement la suite : expliquez ce que la commande consulte ou modifie.

### Questions de rappel actif

1. Pourquoi vérifier `main` avant de créer la branche?
2. Que fait l'option `-u` lors du premier `push`?
3. Comment récupérer une branche créée sur le dépôt distant?
4. Pourquoi une branche courte devrait-elle raconter une seule intention?

## Repère 3 — Fusion et conflit

Pour fusionner une fonctionnalité dans `main`, placez-vous d'abord sur `main`. La branche active constitue la destination de `git merge`.

Une avance rapide déplace simplement le nom de branche lorsque l'historique n'a pas divergé. Un commit de fusion réunit deux lignées de commits lorsque les deux branches ont avancé.

Un conflit ne signifie pas qu'une branche est fausse. Il signifie que Git ne peut pas choisir seul le contenu final. Il faut :

1. lire les deux intentions;
2. construire le contenu final cohérent;
3. retirer les marqueurs de conflit;
4. ajouter le fichier résolu à l'index;
5. terminer le commit et exécuter les tests.

## Repère 4 — Reconnaître une dépendance concrète

```csharp
public sealed class ServiceCommande
{
    private readonly NotificationConsole m_notification = new();
}
```

Le service choisit ici le type concret et son moment de création. Ce choix rend la substitution et le test plus difficiles.

### Diagnostic

1. De quelle capacité le service a-t-il réellement besoin?
2. Quel contrat étroit pourrait exprimer ce besoin?
3. Qui devrait choisir l'implantation concrète?
4. Comment le test pourrait-il observer l'interaction sans vraie console?

## Repère 5 — Séparer la création avec Factory

Deux patrons de création sont distingués :

| Patron | Question |
|---|---|
| Abstract Factory | Comment créer une famille cohérente de produits liés sans exposer leurs classes concrètes? |
| Factory Method | Comment permettre à un sous-type de redéfinir une étape de création? |

Une simple condition qui choisit un type concret peut constituer un premier mécanisme de création, mais elle ne remplace pas automatiquement les rôles complets d'Abstract Factory ou de Factory Method.

### Questions de rappel actif

1. Quel objet utilise les produits créés?
2. La variation concerne-t-elle une famille de produits ou une méthode redéfinissable?
3. Le client connaît-il encore les classes concrètes?
4. Pourquoi Factory et injection de dépendances ne répondent-elles pas à la même question?

## Repère 6 — Injection par constructeur

```csharp
public ServiceCommande(INotificationCommande notification)
{
    ArgumentNullException.ThrowIfNull(notification);
    m_notification = notification;
}
```

Le constructeur rend la dépendance obligatoire et visible. `Program.cs` peut construire l'implantation, puis la fournir au service. Cette injection manuelle suffit souvent et n'exige pas de conteneur.

Dans un test, fournissez directement une implantation en mémoire ou une doublure. Le test unitaire ne devrait pas construire son sujet en demandant au conteneur de le résoudre.

## Repère 7 — IoC et conteneur .NET

- **DI** demande comment l'objet reçoit son collaborateur.
- **IoC** demande qui contrôle la création et l'assemblage.
- Le **conteneur** automatise cet assemblage à partir des associations enregistrées.

`HostApplicationBuilder` prépare notamment `builder.Configuration` et `builder.Services`. Le point de composition enregistre les services, appelle `Build`, crée une portée lorsque nécessaire et résout l'opération de départ.

Le conteneur reste à la frontière. Application et le domaine ne devraient pas recevoir `IServiceProvider`, car ils cacheraient alors leurs dépendances.

## Repère 8 — Cycles de vie

| Cycle | Repère mental |
|---|---|
| `Transient` | Une nouvelle instance à chaque résolution. |
| `Scoped` | Une instance partagée à l'intérieur d'une portée. |
| `Singleton` | Une instance partagée pendant la vie du conteneur. |

Le choix dépend de l'état porté par l'objet et de la durée pendant laquelle cet état peut être partagé. Une dépendance `Scoped` conservée par un `Singleton` devient captive et vit trop longtemps.

## Confusions fréquentes

- Créer une branche sans vérifier son point de départ.
- Confondre branche locale et référence distante.
- Exécuter `git merge` depuis la mauvaise destination.
- Conserver automatiquement une version complète lors d'un conflit sans reconstruire l'intention finale.
- Croire qu'une interface supprime à elle seule tout couplage.
- Confondre Abstract Factory et Factory Method.
- Croire que l'injection exige toujours un conteneur.
- Utiliser `IServiceProvider` comme un service locator dans le domaine.
- Injecter une dépendance `Scoped` dans un `Singleton`.

## Autoévaluation

- [ ] Je relie chaque commande Git à l'état qu'elle modifie.
- [ ] Je peux créer, publier, récupérer et fusionner une branche.
- [ ] Je construis le contenu final d'un conflit et j'exécute les tests.
- [ ] Je repère une création directe qui nuit à la substitution.
- [ ] Je distingue Abstract Factory de Factory Method.
- [ ] J'injecte un collaborateur par constructeur et je refuse `null`.
- [ ] Je distingue DI, IoC et conteneur.
- [ ] Je justifie le cycle de vie choisi et je reconnais une dépendance captive.

## Lectures ciblées

- Chapitre 8 : inversion de contrôle et injection de dépendances.
- Chapitre 12 : Abstract Factory et Factory Method.
- Chapitre 15 : branches Git, fusion et conflits.

