# Exercice 2

## Mission et durée

En 60 minutes, remplacez une hiérarchie fragile par un contrat de calcul
étroit et par la composition. Vous préparez des calculateurs substituables;
la classe `ServiceLivraisons` deviendra un contexte Strategy à l’exercice 3.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/solid-composition` depuis la branche `dev`.

## Travail demandé

Le projet de départ ne contient pas encore d’interface : il propose une
hiérarchie de classes où `CalculateurFrais` mélange le calcul des frais et une
description destinée à l’affichage. `CalculateurFraisGratuit` hérite donc de la
description « Tarification standard », qui ne lui convient pas.

Réusinez ce modèle afin que chaque classe porte seulement les responsabilités
utiles au calcul des frais.

1. Dans le projet principal, créez l’interface `ICalculateurFraisLivraison`
   avec la seule méthode de calcul. Renommez les deux classes fournies pour
   préciser qu’elles calculent des frais de livraison. Retirez leur relation
   d’héritage et la méthode `ObtenirDescription()` : aucun calculateur ni
   service n’a besoin de produire un texte destiné à l’affichage. Ce texte
   appartient au projet Terminal, si l’on souhaite l’afficher.
2. La classe du calculateur gratuit reçoit dans son constructeur un calculateur
   de repli par l’interface `ICalculateurFraisLivraison`. Elle retourne zéro à
   partir d’un sous-total de `50 $`; en dessous, elle délègue le calcul à cette
   dépendance, sans recopier la formule `4 $ + 0,75 $` par kilomètre. Lors de
   l’assemblage, fournissez-lui une instance du calculateur standard. Son
   constructeur refuse une dépendance `null` avec `ArgumentNullException`.
3. Ajoutez une classe de calculateur prioritaire qui applique la règle déjà
   présente dans la classe `ServiceLivraisons` : `2 $ + 1 $` par kilomètre.
   Ajoutez-la sans modifier les deux autres calculateurs.
4. Fixez le même contrat d'entrée pour la méthode de calcul de chacun des
   trois calculateurs : le sous-total doit être positif ou nul et la distance
   doit être positive ou nulle et finie. Refusez un sous-total négatif ainsi
   qu'une distance négative, `NaN` ou infinie avec
   `ArgumentOutOfRangeException`, y compris lorsque le calculateur gratuit
   retournerait zéro sans avoir besoin de la distance. Une distance de zéro
   reste valide.
5. Dans le projet de tests, conservez le test de la classe `Client` écrit à
   l’exercice 1. Ajoutez une méthode de test unitaire xUnit paramétrée
   (`[Theory]`) qui reçoit les trois calculateurs par l’interface
   `ICalculateurFraisLivraison` et vérifie leurs frais. Incluez un cas sous
   `50 $` et un cas à partir de `50 $` pour le calculateur gratuit, ainsi
   qu'un cas à distance zéro. Ajoutez aussi une méthode `[Theory]` qui
   vérifie, pour chacun des trois calculateurs, le refus d'un sous-total
   négatif et d'une distance négative, `NaN` ou infinie. Ajoutez un test du
   constructeur du calculateur gratuit avec une dépendance `null`.

<details>
<summary>Transmettre un objet avec <code>MemberData</code></summary>

`MemberData` permet de fournir à une théorie des valeurs qui ne peuvent pas
être construites directement dans un attribut. Dans cet exemple indépendant
de l’exercice, une méthode fournit chaque cas sous forme de tableau. Chaque
tableau contient exactement deux valeurs : un objet `DateOnly` et le jour
attendu.

```csharp
public static IEnumerable<object[]> CasJours()
{
    yield return new object[]
    {
        new DateOnly(2024, 1, 1),
        DayOfWeek.Monday
    };

    yield return new object[]
    {
        new DateOnly(2024, 1, 2),
        DayOfWeek.Tuesday
    };
}

[Theory]
[MemberData(nameof(CasJours))]
public void DayOfWeek_DateDonnee_RetourneJourAttendu(
    DateOnly date,
    DayOfWeek jourAttendu)
{
    DayOfWeek jourObtenu = date.DayOfWeek;

    Assert.Equal(jourAttendu, jourObtenu);
}
```

Les deux valeurs de chaque tableau correspondent, dans le même ordre, aux deux
paramètres de la méthode de test. Consultez au besoin le rappel sur les
[tests paramétrés de la semaine 2](../semaine-02/E02-tests-sans-dependance.md#2-tests-paramétrés).

Pour adapter cette structure au test demandé dans l’exercice 2, quels types et
quels paramètres votre théorie devra-t-elle recevoir?

</details>

La classe `ServiceLivraisons` et le projet Terminal continuent de fonctionner
comme à l’exercice 1. Vous injecterez le contrat de calcul dans le constructeur
de `ServiceLivraisons` à l’exercice 3.

Limitez-vous à ces trois calculateurs. Le but des tests est de vérifier leur
substitution et leur contrat d'entrée commun, pas de couvrir toutes les
distances possibles.

Dans `DECISIONS.md`, expliquez pourquoi `ICalculateurFraisLivraison`
n’expose que le calcul : le futur service qui utilisera ce contrat n’a pas
besoin de la méthode `ObtenirDescription()`. Justifiez aussi brièvement
OCP, LSP et la composition, puis expliquez pourquoi des objets polymorphes
ne constituent pas encore, à eux seuls, le patron Strategy. Réalisez
ensuite le parcours de branches Git `fonctionnalite/solid-composition`
→ `dev` → `main` dans le dépôt de l’exercice.
