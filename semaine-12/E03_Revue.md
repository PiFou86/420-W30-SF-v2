# Exercice 3

**Point à pratiquer.** Formuler un commentaire précis à partir d’un défaut reproduit.

Depuis le dossier `semaine-12`, préparer une copie dédiée avec les commandes suivantes dans Git Bash ou un terminal compatible.

```bash
bash preparer_git.sh bac-a-sable-s12-revue
cd bac-a-sable-s12-revue
git switch revue/pagination
git diff dev...revue/pagination
```

Le diff représente une petite proposition de changement. Son contrat annoncé est « Retourner jusqu’à deux soupes disponibles par page, triées par numéro. La recherche ignore la casse. »

1. Ouvrir la solution `S12E01E03_Collaboration/S12E01E03_Collaboration.slnx` du bac à sable dans Visual Studio. Lire le changement dans la méthode `Rechercher` du fichier `RequeteCatalogue.cs`, dans le projet principal.
2. Pour reproduire le comportement, ajouter le code suivant dans `Program.cs` du projet Terminal après l’affichage initial, puis générer et lancer.

```csharp
IReadOnlyList<Produit> page = produits.Rechercher("sOU", 0, 2);
Console.Out.WriteLine($"Page reçue : {string.Join(", ", page.Select(produit => produit.Numero))}");
```

Le résultat attendu est `2, 3`. Le produit 1 est indisponible, les produits 2 et 3 sont disponibles.

3. Rédiger un commentaire de deux ou trois phrases qui situe le défaut, décrit son effet avec ces données et précise le résultat attendu. Montrer le commentaire à un collègue pour qu’il puisse retrouver et comprendre le problème.
4. Corriger l’ordre des opérations dans `RequeteCatalogue.cs`, puis rejouer le même scénario pour vérifier le commentaire.

**Fin observable.** Un collègue peut reproduire le défaut à partir du commentaire, et le correctif permet au Terminal d’afficher `Page reçue : 2, 3`.
