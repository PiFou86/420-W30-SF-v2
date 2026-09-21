# Guide de révision active — Semaine 4

## DIP et qualité de conception

Ce guide sert à vérifier votre compréhension avant de revoir les diapositives ou de reprendre un exercice. Commencez par répondre de mémoire, puis consultez les notes pour corriger et préciser vos réponses.

## Capacités à vérifier

À la fin de votre étude, vous devriez pouvoir :

- dessiner le sens des dépendances entre un service, un contrat et une implantation;
- distinguer injection de dépendances, principe d’inversion des dépendances et conteneur;
- assembler le même graphe d’objets manuellement et avec le cadriciel;
- justifier une inscription `Scoped` et l’utilisation d’une portée;
- expliquer pourquoi une interface ne suffit pas à appliquer DIP;
- substituer une notification ou un dépôt sans modifier le service;
- expliquer ce qui reste stable lorsqu'un collaborateur interchangeable change;
- repérer plusieurs raisons de changer dans une même classe;
- séparer une commande d’une requête;
- remplacer une chaîne d’appels par un message métier;
- vérifier une interaction avec un espion manuel, puis avec Moq;
- choisir entre objet réel, test d’état et test d’interaction.

## Carte des notions

| Notion | Question centrale | Indice dans le code |
|---|---|---|
| Injection de dépendances | Comment l’objet reçoit-il son collaborateur? | Le collaborateur arrive généralement par le constructeur. |
| DIP | Dans quel sens les modules et les contrats dépendent-ils? | Le service dépend d’un contrat qui exprime son besoin. |
| Conteneur | Qui construit et assemble les objets? | Les associations entre contrats et implantations sont configurées à un point de composition. |
| Doublure manuelle | Quelle interaction le test doit-il rendre observable? | Une petite implantation du contrat mémorise les appels utiles. |
| Moq | Comment exprimer plus brièvement la même observation? | `Mock<T>`, `.Object`, `Verify` et `Times` décrivent l’interaction attendue. |
| SRP | Quelles raisons peuvent faire changer cette classe? | La classe mélange règle métier, stockage, notification ou présentation. |
| CQS | Cette opération modifie-t-elle l’état ou retourne-t-elle de l’information? | Une même méthode effectue les deux responsabilités. |
| Loi de Déméter | Combien de structures internes le client doit-il connaître? | Une chaîne parcourt plusieurs objets successifs. |
| Comportement interchangeable | Quelle partie peut varier sans modifier le service? | Le service délègue à un collaborateur reçu par contrat. |

## Repère 1 — DI, DIP et conteneur

Ces notions répondent à des questions différentes :

- **DI** rend la dépendance visible et la fournit de l’extérieur;
- **DIP** oriente les dépendances vers une abstraction qui décrit le besoin du client;
- le **conteneur** automatise l’assemblage des objets.

Une classe peut recevoir un objet concret par injection. Elle applique alors DI, mais pas nécessairement DIP. Pour appliquer DIP, le module de haut niveau et le détail technique dépendent d’un contrat stable qui n’expose pas le format ou le mécanisme du fournisseur.

### Questions de rappel actif

1. Pourquoi `ServiceReservation(DepotReservationsJson depot)` ne suffit-il pas à appliquer DIP?
2. Quel module devrait normalement définir `IDepotReservations`?
3. Où choisit-on entre `DepotReservationsMemoire` et une implantation technique?
4. Qu’est-ce qui devrait rester inchangé lorsque l’on substitue les deux dépôts?

### Deux assemblages, un même graphe

L’assemblage manuel rend visibles les appels de constructeurs. Le cadriciel
automatise ensuite ce même travail à partir des associations configurées dans
la racine de composition. Dans les deux cas, les objets métier ignorent qui les
a construits.

Pour le conteneur .NET, soyez capable de replacer dans l’ordre :

1. `Host.CreateApplicationBuilder(args)`;
2. `builder.Services.AddScoped<...>()`;
3. `builder.Build()`;
4. `host.Services.CreateScope()`;
5. `scope.ServiceProvider.GetRequiredService<...>()`.

Expliquez aussi pourquoi `IServiceProvider` ne doit pas être transmis à
`ServiceCommandes` et pourquoi un test unitaire construit directement son
sujet.

### Une interaction, deux styles de doublures

Un espion manuel implante le contrat et conserve explicitement les valeurs
reçues. Moq permet d’exprimer la même vérification avec `Mock<T>`, `Verify`,
`Times.Once`, `Times.Never` ou `VerifyNoOtherCalls`.

- utilisez un **objet réel** pour un calcul ou un objet du domaine simple et
  déterministe;
- utilisez un **test d’état** lorsque le résultat observable est une valeur ou
  un état final;
- utilisez un **test d’interaction** lorsqu’il faut vérifier un appel vers un
  port comme une notification ou une sauvegarde.

Questions de comparaison :

1. Quelle information l’espion doit-il mémoriser?
2. Quelle instruction Moq exprime la même attente?
3. Que cache Moq que la classe manuelle rend visible?
4. Pourquoi ne faut-il pas simuler tous les objets du graphe?

### Pont vers la semaine suivante

La substitution prépare une idée plus générale : un service peut déléguer une partie variable de son comportement à un collaborateur interchangeable.

Pour vérifier ce préalable, répondez sans nommer encore de patron :

1. Quel comportement est délégué au collaborateur (dépôt dans ce guide,
   notification dans l’atelier)?
2. Qu'est-ce qui devient interchangeable?
3. Qui choisit l'implantation concrète?
4. Quelle partie du service reste stable lorsque cette implantation change?

## Repère 2 — Le dépôt en mémoire

Le dépôt en mémoire démontre que le service dépend d’un besoin plutôt que d’un détail technique. Il permet aussi de tester une règle sans dépendre d’un chemin de fichier, d’un format de sérialisation ou des permissions du système.

Le dépôt en mémoire n’est pas automatiquement une doublure. Il peut être une implantation simple utilisée par l’application ou par les tests. Son rôle dépend du contexte dans lequel on l’emploie.

### Diagnostic de code

```csharp
public sealed class ServiceReservation
{
    private readonly DepotReservationsJson m_depot = new();

    public void Reserver(Reservation reservation)
    {
        m_depot.Ajouter(reservation);
    }
}
```

Sans écrire immédiatement la correction, identifiez :

1. le module de haut niveau;
2. le détail technique connu par ce module;
3. le besoin minimal à exprimer dans un contrat;
4. le changement nécessaire au constructeur;
5. la façon de vérifier la substitution avec un dépôt en mémoire.

## Repère 3 — Responsabilité unique

SRP ne signifie pas « une seule méthode par classe ». Une classe peut offrir plusieurs opérations cohérentes si elles changent pour la même raison.

Pour diagnostiquer une responsabilité trop large :

1. nommez les acteurs ou décisions qui pourraient provoquer un changement;
2. regroupez les comportements qui évoluent ensemble;
3. séparez les détails qui suivent des rythmes de changement différents;
4. laissez au service l’orchestration qui lui appartient.

### Question de justification

Une méthode `FermerJournee()` calcule les ventes, écrit un fichier JSON, envoie un courriel et affiche un rapport. Nommez les raisons de changer distinctes avant de proposer de nouvelles classes.

## Repère 4 — CQS

- Une **commande** modifie l’état.
- Une **requête** retourne de l’information sans modifier l’état observable.

CQS est un guide de conception. Certaines conventions .NET, comme `TryGetValue`, retournent plusieurs informations sans modifier la collection. Elles ne contredisent pas l’intention centrale du principe.

### Questions de rappel actif

1. Pourquoi `AjouterEtRetournerTotal` est-il ambigu?
2. Quelles signatures permettraient de séparer l’ajout et la consultation du total?
3. Quel effet observable rendrait une méthode de lecture suspecte?

## Repère 5 — Loi de Déméter

Une méthode devrait collaborer avec son propre objet, ses paramètres, les objets qu’elle crée et ses collaborateurs directs. Une longue chaîne d’appels révèle souvent que le client connaît trop de détails internes.

```csharp
bool locale = commande.Client.Adresse.CodePostal.StartsWith("G1");
```

Avant de corriger ce code, demandez-vous :

- quel objet possède l’information utile;
- quelle intention métier le client essaie d’exprimer;
- quel message permettrait de cacher la structure interne.

## Confusions fréquentes

- Croire que toute interface applique automatiquement DIP.
- Confondre injection et utilisation obligatoire d’un conteneur.
- Utiliser le conteneur comme service locator dans Application.
- Résoudre le sujet testé depuis le conteneur au lieu de l’instancier directement.
- Utiliser Moq pour remplacer un petit objet déterministe.
- Découper une classe uniquement parce qu’elle contient plusieurs méthodes.
- Considérer toute méthode qui retourne une valeur comme une requête pure.
- Déplacer une chaîne d’appels dans une méthode utilitaire sans réduire la connaissance du graphe d’objets.

## Autoévaluation

Cochez seulement si vous pouvez expliquer votre réponse avec un exemple.

- [ ] Je distingue DI, DIP et conteneur.
- [ ] Je peux assembler les mêmes objets manuellement et avec le cadriciel.
- [ ] Je peux justifier `Scoped`, la portée et le point de composition.
- [ ] Je peux traduire un espion manuel en vérification Moq équivalente.
- [ ] Je distingue test d’état, test d’interaction et objet réel.
- [ ] Je peux dessiner les dépendances avant et après l’introduction d’un contrat.
- [ ] Je peux expliquer le gain offert par un dépôt en mémoire.
- [ ] Je peux nommer ce qui varie, ce qui reste stable et l'endroit où l'implantation est choisie.
- [ ] Je nomme les raisons de changer avant de découper une classe.
- [ ] Je sépare commande et requête sans appliquer CQS mécaniquement.
- [ ] Je transforme une chaîne d’appels en intention métier.

## Lectures ciblées

- Chapitre 8 : inversion de contrôle, injection de dépendances et DIP.
- Chapitre 11 : SRP, CQS et loi de Déméter.
