# Guide de révision active — Semaine 5

## SOLID, composition et Strategy

Ce guide organise les notions autour d’une même question : comment faire évoluer un modèle sans disperser ses règles ni fragiliser ses contrats?

Répondez d’abord de mémoire. Vérifiez ensuite vos réponses dans les notes et les diapositives.

## Capacités à vérifier

À la fin de votre étude, vous devriez pouvoir :

- replacer une décision dans l’objet qui possède les données nécessaires;
- reconnaître une variation qui justifie un point d’extension;
- repérer un sous-type qui surprend les clients du type de base;
- scinder une interface selon les besoins de ses clients;
- justifier le choix de l’héritage ou de la composition;
- reconnaître et implanter les rôles du patron Strategy;
- distinguer Strategy du seul mécanisme de polymorphisme;

## Carte des notions

| Notion | Question centrale | Correction recherchée |
|---|---|---|
| Tell, Don’t Ask | Quel objet possède les données nécessaires à la décision? | Donner une intention métier à cet objet. |
| OCP | Quelle variation réelle oblige à modifier du code stable? | Prévoir un point d’extension ciblé. |
| LSP | Le sous-type respecte-t-il les attentes du type de base? | Choisir une abstraction honnête, l’immutabilité ou la composition. |
| ISP | Le client dépend-il d’opérations qu’il n’utilise pas? | Définir des contrats cohérents selon les clients. |
| Composition | Le comportement doit-il être assemblé, combiné ou remplacé? | Fournir un collaborateur au lieu de forcer une hiérarchie. |
| Strategy | Quelle famille d'algorithmes doit varier indépendamment du contexte? | Injecter un contrat et déléguer à une stratégie choisie à l'assemblage. |

## Repère 1 — Tell, Don’t Ask

Lorsque le client extrait des données pour reconstruire une règle, plusieurs clients risquent d’interpréter cette règle différemment. Demander à l’objet d’agir place la décision près des données et permet à l’objet de protéger ses invariants.

```csharp
if (commande.Statut == Statut.Brouillon
    && commande.Lignes.Count > 0)
{
    commande.Statut = Statut.Confirmee;
}
```

Questions à poser :

1. Quelle règle le client reconstruit-il?
2. Quel objet possède les données nécessaires?
3. Quel message exprimerait l’intention plutôt que la structure?
4. Quelle validation doit rester dans l’objet?

Tell, Don’t Ask et la loi de Déméter sont proches, mais distincts. Le premier replace une décision près des données. La seconde limite les objets qu’un client doit connaître.

## Repère 2 — OCP

OCP vise les variations réelles. Il ne demande pas de créer une abstraction pour chaque détail.

Une cascade conditionnelle qui grandit pour chaque nouvelle politique révèle souvent un point d’extension. Le code stable dépend alors d’un contrat; une nouvelle variation ajoute une implantation sans réécrire les cas existants.

### Questions de rappel actif

1. Quel changement ferait grandir la cascade de rabais?
2. Quel comportement commun peut former le contrat?
3. Quelle partie du code devrait rester stable?
4. Pourquoi faut-il éviter d’anticiper des variations imaginaires?

## Repère 3 — LSP

Un sous-type doit respecter les attentes promises par son type de base. Le problème classique du carré et du rectangle apparaît lorsqu’un client de `Rectangle` s’attend à modifier largeur et hauteur indépendamment, alors qu’un `Carre` force les deux valeurs à rester égales.

Pour vérifier la substitution :

- les entrées acceptées restent-elles compatibles?
- les résultats respectent-ils les attentes annoncées?
- les invariants du type de base restent-ils vrais?
- le sous-type ajoute-t-il une exception surprenante?
- le client doit-il tester le type concret avant d’agir?

Une meilleure conception peut utiliser `IForme`, des objets immuables ou la composition. Le choix dépend du contrat réellement utile au client.

## Repère 4 — ISP

Le nombre de méthodes ne détermine pas à lui seul la qualité d’une interface. Une interface devient problématique lorsqu’un client dépend de membres inutiles ou ne peut pas respecter honnêtement tout le contrat.

### Diagnostic

Une interface `IEmploye` impose `Travailler`, `LivrerCommande` et `Cuisiner` à tous les employés.

1. Quels clients utilisent réellement chaque capacité?
2. Quels contrats cohérents pourraient être séparés?
3. Une même classe pourrait-elle implanter plusieurs de ces contrats?

## Repère 5 — Héritage ou composition

L’héritage affirme une relation de substitution. La composition assemble des capacités à l’aide de collaborateurs.

| Question | Relation à examiner |
|---|---|
| Le sous-type peut-il remplacer honnêtement le type de base? | Héritage possible |
| Le comportement doit-il changer pendant la vie de l’objet? | Composition |
| Plusieurs capacités doivent-elles se combiner? | Composition |
| Le client doit-il ignorer le type concret? | Abstraction |

Ne choisissez pas l’héritage uniquement pour réutiliser du code. Vérifiez d’abord le contrat de substitution.

## Repère 6 — Strategy

Strategy organise une famille de comportements interchangeables. Le contexte ne choisit pas une variante avec une chaîne ou une cascade conditionnelle : il reçoit une stratégie par son contrat et lui délègue le calcul.

### Démarche de conception

1. **Problème** — repérer la cascade qui grandit lorsqu'une règle change.
2. **Comportement variable** — isoler l'algorithme qui diffère entre les cas.
3. **Contrat** — définir l'opération dont le contexte a besoin.
4. **Stratégies** — créer une implantation par règle cohérente.
5. **Contexte** — injecter le contrat et déléguer sans tester le type concret.
6. **Point de composition** — choisir l'implantation lors de l'assemblage.

```csharp
public interface IPolitiqueRabais
{
    decimal Appliquer(decimal sousTotal);
}

public sealed class CalculateurPrix
{
    private readonly IPolitiqueRabais m_politique;

    public CalculateurPrix(IPolitiqueRabais politique)
    {
        ArgumentNullException.ThrowIfNull(politique);
        m_politique = politique;
    }

    public decimal Calculer(decimal sousTotal)
    {
        return m_politique.Appliquer(sousTotal);
    }
}
```

### Liens à expliquer

- **OCP** : une nouvelle politique ajoute une implantation sans réécrire `CalculateurPrix`.
- **DIP** : le contexte dépend du contrat qui exprime son besoin.
- **Composition** : le calculateur reçoit une capacité au lieu d'hériter d'une variante.
- **Injection** : la stratégie choisie devient une dépendance visible.

Le polymorphisme est le mécanisme qui permet l'appel uniforme. Strategy est l'organisation où un contexte délègue intentionnellement un comportement variable à une stratégie choisie à l'assemblage.

### Questions de rappel actif

1. Quelle partie du calcul doit rester stable?
2. Quel rôle joue `CalculateurPrix`?
3. Où choisit-on entre deux politiques?
4. Pourquoi déplacer la cascade dans une classe auxiliaire ne suffit-il pas?
5. Dans quel cas une simple méthode demeure-t-elle préférable à Strategy?

## Confusions fréquentes

- Déplacer toutes les décisions dans un service et laisser les entités sans comportement.
- Créer une interface pour une variation hypothétique.
- Croire que l’héritage est approprié dès que deux classes partagent du code.
- Scinder une interface seulement selon sa longueur.
- Appeler Strategy toute utilisation d'une interface ou tout polymorphisme.
- Déplacer une cascade conditionnelle sans rendre les règles réellement interchangeables.
- Laisser le contexte sélectionner lui-même la stratégie concrète.

## Autoévaluation

- [ ] Je distingue Tell, Don’t Ask de la loi de Déméter.
- [ ] Je peux justifier un point d’extension avec une variation réelle.
- [ ] Je vérifie les attentes du type de base avant de proposer un héritage.
- [ ] Je définis les interfaces selon les besoins de leurs clients.
- [ ] Je justifie le choix entre héritage et composition.
- [ ] Je reconnais le contexte, la stratégie, les stratégies concrètes et le point de composition.
- [ ] Je distingue Strategy du mécanisme de polymorphisme.
- [ ] Je peux relier Strategy à OCP, DIP, composition et injection.

## Lectures ciblées

- Chapitre 11 : Tell, Don’t Ask, OCP, LSP et ISP.
- Chapitre 12, sections 12.1, 12.2.1 et 12.2.6 : patrons et Strategy.
