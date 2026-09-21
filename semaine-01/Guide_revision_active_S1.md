# Guide de révision active — Semaine 1

## Environnement .NET, Git, révision POO et diagrammes de classes

Ce guide relie les outils du cours aux notions fondamentales de programmation orientée objet. Commencez par répondre sans notes. Consultez ensuite les exemples pour corriger votre vocabulaire, votre code et vos diagrammes.

## Capacités à vérifier

À la fin de votre étude, vous devriez pouvoir :

- expliquer la structure d'une solution .NET;
- relier les commandes `dotnet` aux fichiers et projets qu'elles utilisent;
- distinguer les principaux états locaux de Git;
- expliquer classe, objet, état, comportement, identité et invariant;
- distinguer les quatre piliers de la POO;
- comparer classe abstraite, interface, héritage et composition;
- lire un diagramme de classes UML;
- passer d'un petit modèle UML à son implantation C# et inversement.

## Carte des notions

| Notion | Question centrale | Repère |
|---|---|---|
| Solution .NET | Comment plusieurs projets sont-ils organisés? | La solution regroupe; chaque projet produit ou teste une partie du logiciel. |
| Fichier `.csproj` | De quoi le projet dépend-il et quelle cible utilise-t-il? | Il contient notamment les références et `TargetFramework`. |
| Git | Quel état de mon travail la commande consulte-t-elle ou modifie-t-elle? | Répertoire de travail, index et historique local. |
| Encapsulation | Qui peut modifier l'état et par quelles opérations? | L'objet protège ses invariants derrière son interface publique. |
| Abstraction | Quel contrat utile le client doit-il connaître? | Le client ignore les détails qui ne servent pas à son travail. |
| UML | Quelle structure et quelles relations le modèle communique-t-il? | Visibilités, types, relations et cardinalités. |

## Repère 1 — Solution, projet et environnement .NET

Une solution Visual Studio organise plusieurs projets. Un projet contient ses sources, ses références et sa configuration de compilation. Une référence de projet exprime qu'un projet utilise le code produit par un autre.

Vous devriez distinguer :

- le **SDK**, qui permet notamment de créer et de compiler;
- le **runtime**, qui exécute l'application;
- le **framework cible**, qui détermine les API disponibles au projet;
- la **version du langage**, qui concerne les fonctionnalités C# acceptées.

### Questions de rappel actif

1. Quelle information centrale trouve-t-on dans un fichier `.csproj`?
2. Pourquoi un projet de tests référence-t-il le projet testé?
3. Que font respectivement `dotnet restore`, `dotnet build`, `dotnet run` et `dotnet test`?
4. Pourquoi `bin/` et `obj/` n'appartiennent-ils généralement pas aux sources suivies?
5. Quelle différence faites-vous entre `Console.Out` et `Console.Error`?

## Repère 2 — Git de base

Reliez toujours la commande à l'état qu'elle consulte ou modifie :

```text
répertoire de travail → index → commit local
```

- `git status` observe la situation courante;
- `git add` sélectionne une version des changements pour le prochain commit;
- `git commit` enregistre un instantané local;
- `git log --oneline` consulte l'historique;
- `.gitignore` exclut les fichiers régénérables ou locaux qui ne doivent pas être suivis.

### Questions de rappel actif

1. Pourquoi `git add` ne crée-t-il pas encore un commit?
2. Que devrait expliquer le message d'un commit?
3. Quelle différence faites-vous entre `git fetch` et `git pull`?
4. Comment vérifier qu'un fichier généré n'est pas suivi par erreur?

## Repère 3 — Objet, état, comportement et invariant

Une classe décrit une forme d'objet. Un objet possède une identité, un état courant et des comportements. Un invariant exprime une condition qui doit rester vraie pour que l'objet demeure cohérent.

Pour analyser une classe :

1. nommez les données qui forment son état;
2. repérez les opérations qui peuvent modifier cet état;
3. formulez les invariants à protéger;
4. vérifiez que l'interface publique empêche les modifications incohérentes.

### Diagnostic

Une commande expose publiquement sa liste modifiable de lignes. Quel invariant un client pourrait-il contourner? Quelle responsabilité devrait revenir à `Commande`?

## Repère 4 — Les quatre piliers

| Pilier | Question à poser |
|---|---|
| Encapsulation | Comment l'objet contrôle-t-il l'accès à son état? |
| Abstraction | Quel contrat utile cache les détails inutiles au client? |
| Héritage | Le sous-type exprime-t-il une spécialisation et une vraie substitution? |
| Polymorphisme | Plusieurs objets peuvent-ils répondre différemment au même message? |

L'héritage ne sert pas seulement à réutiliser du code. Il promet qu'un objet du sous-type peut remplacer honnêtement un objet du type de base.

## Repère 5 — Classe abstraite, interface et composition

- Une **classe abstraite** peut fournir un état et une implantation partielle commune.
- Une **interface** décrit un contrat que plusieurs classes peuvent implanter.
- La **composition** construit un comportement en collaborant avec d'autres objets.

### Questions de comparaison

1. Le client a-t-il besoin d'un contrat ou d'une base partiellement implantée?
2. La relation exprime-t-elle vraiment une substitution?
3. Le comportement devrait-il pouvoir être remplacé ou combiné?
4. Une classe doit-elle pouvoir remplir plusieurs rôles distincts?

## Repère 6 — Lire une classe UML

Dans un compartiment de classe :

- `+` signifie `public`;
- `-` signifie `private`;
- `#` signifie `protected`;
- `~` signifie `internal`;
- le type suit `:`;
- les parenthèses indiquent une opération.

Une propriété C# peut représenter une porte d'accès vers une donnée membre, une valeur calculée ou un alias d'autres membres. Les stéréotypes `«get»`, `«set»` et `«private set»` rendent ses accès visibles dans le diagramme.

## Repère 7 — Choisir une relation UML

| Relation | Question centrale |
|---|---|
| Association dirigée | Quel objet connaît l'autre? |
| Agrégation | Le tout regroupe-t-il des parties qui peuvent exister seules? |
| Composition | Le tout possède-t-il des parties dont le cycle de vie dépend de lui? |
| Héritage | Le sous-type est-il substituable au type de base? |
| Réalisation d'interface | Quelle classe implante le contrat? |
| Dépendance | Quel type est utilisé temporairement? |

Les cardinalités décrivent le nombre d'objets liés dans une association, une agrégation ou une composition. Elles ne s'ajoutent pas à l'héritage ou à la réalisation d'interface.

## Confusions fréquentes

- Confondre solution et projet.
- Mémoriser les commandes Git sans comprendre leurs effets sur l'état.
- Employer « attribut » comme terme principal pour une variable d'objet C#.
- Présenter l'encapsulation comme le simple fait de rendre les données privées.
- Utiliser l'héritage uniquement pour éviter une duplication.
- Choisir une flèche UML avant de nommer la relation du modèle.
- Ajouter des cardinalités à une relation qui ne représente pas des objets liés.

## Autoévaluation

- [ ] Je peux expliquer la structure d'une solution .NET.
- [ ] Je relie chaque commande Git à l'état qu'elle modifie.
- [ ] Je formule un invariant et j'explique comment l'objet le protège.
- [ ] Je distingue les quatre piliers avec des exemples C#.
- [ ] Je compare classe abstraite, interface, héritage et composition.
- [ ] Je lis les visibilités, les types et les propriétés d'une classe UML.
- [ ] Je choisis une relation UML après avoir expliqué le sens du modèle.
- [ ] Je peux passer d'un petit diagramme au code et du code au diagramme.

## Lectures ciblées

- Introduction à C# et à l'environnement .NET.
- Vocabulaire et normes du cours.
- Quatre piliers de la POO.
- Classes abstraites, interfaces et polymorphisme.
- Diagrammes de classes.

