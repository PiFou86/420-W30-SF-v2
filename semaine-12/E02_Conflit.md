# Exercice 2

**Point à pratiquer.** Résoudre un conflit en conservant les intentions des deux branches.

Depuis le dossier `semaine-12`, exécuter dans Git Bash ou un terminal compatible les commandes suivantes. Le script prépare une copie de la solution et les branches de l’exercice.

```bash
bash preparer_git.sh bac-a-sable-s12-conflit
cd bac-a-sable-s12-conflit
git switch fonctionnalite/pagination
git merge dev
```

Le fichier `RequeteCatalogue.cs` du projet principal entre en conflit. La branche `dev` compare les noms sans tenir compte de la casse. La branche `fonctionnalite/pagination` trie par numéro avant de découper la page.

1. Ouvrir la solution `S12E01E03_Collaboration/S12E01E03_Collaboration.slnx` du bac à sable dans Visual Studio.
2. Dans `RequeteCatalogue.cs`, résoudre le conflit en conservant les deux changements. Filtrer les produits disponibles et leur nom, puis trier, ignorer les premiers résultats et limiter la page.
3. Dans `Program.cs` du projet Terminal, ajouter les vérifications suivantes après l’affichage initial.

```csharp
IReadOnlyList<Produit> premierePage = produits.Rechercher("sOU", 0, 2);
IReadOnlyList<Produit> suite = produits.Rechercher("sOU", 1, 2);
Console.Out.WriteLine($"Première page : {string.Join(", ", premierePage.Select(produit => produit.Numero))}");
Console.Out.WriteLine($"Suite : {string.Join(", ", suite.Select(produit => produit.Numero))}");
```

Générer et lancer le Terminal. Il doit afficher `Première page : 2, 3` et `Suite : 3`. Après vérification, terminer la fusion depuis le dossier `bac-a-sable-s12-conflit`.

```bash
git add S12E01E03_Collaboration/S12E01E03_Collaboration/RequeteCatalogue.cs
git add S12E01E03_Collaboration/S12E01E03_Collaboration.Terminal/Program.cs
git commit -m "Conserver la recherche sans distinction de casse et la pagination"
git status
```

**Fin observable.** Les deux résultats sont corrects, la fusion est enregistrée et `git status` indique un répertoire de travail propre.

<details>
<summary>Rappel Git</summary>

Voir [la fiche de commandes](../git/README.md). Dans cet exercice, la branche courante au début de la fusion est `fonctionnalite/pagination`.

</details>
