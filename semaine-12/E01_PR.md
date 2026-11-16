# Exercice 1

**Point à pratiquer.** Donner au relecteur le contexte et un scénario vérifiable.

La PR simulée va de la branche `fonctionnalite/validation-remise` vers `dev`. Elle modifie la méthode `AppliquerRemise` de la classe `CalculateurRemise`, dans `CalculateurRemise.cs`. L’issue associée est `#42`.

Avant le changement, la méthode calculait directement `montant * (1m - taux)`. Elle pouvait donc produire un total négatif avec un taux de 1,2. Le contrat prévoit un montant positif ou nul et un taux compris entre 0 et 1.

Voici la version proposée.

```csharp
public decimal AppliquerRemise(decimal montant, decimal taux)
{
    ArgumentOutOfRangeException.ThrowIfNegative(montant);
    if (taux < 0m || taux > 1m)
    {
        throw new ArgumentOutOfRangeException(nameof(taux));
    }
    return montant * (1m - taux);
}
```

L’auteur a exécuté les essais suivants.

| Montant | Taux | Résultat obtenu |
| ---: | ---: | --- |
| 100 | 0 | 100 |
| 100 | 0,10 | 90 |
| 100 | 1 | 0 |
| 100 | −0,1 ou 1,2 | `ArgumentOutOfRangeException` |
| −1 | 0,10 | `ArgumentOutOfRangeException` |

1. Dans un court texte Markdown, rédiger un titre précis et les rubriques **Problème**, **Changements**, **Vérifications** et **Points à examiner**. Utiliser uniquement les faits fournis et mentionner l’issue liée.
2. Faire relire la description par un collègue. Celui-ci doit retrouver le besoin, le fichier modifié et un cas normal, un cas limite et un cas invalide.
3. Ajuster la description si une information manque.

**Fin observable.** Le collègue peut expliquer le changement et rejouer les essais à partir de la description.
