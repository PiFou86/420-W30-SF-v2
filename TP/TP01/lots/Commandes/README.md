# Lot Commandes — départ individuel

Le lot possède sa propre solution et ses cinq projets. Aucun autre lot ni noyau commun n'est requis pour le compiler et lancer sa console de départ.

```bash
dotnet build TP01_Commandes.slnx
dotnet run --project TP01_Commandes.Terminal
# Après avoir écrit les tests métier :
dotnet test TP01_Commandes.slnx
```

Les classes du domaine, le cas d'utilisation et le dépôt sont à compléter; le dépôt mémoire contient volontairement des `NotImplementedException`. Les projets de départ compilent, mais ne constituent pas une solution au TP. Voir la fiche individuelle dans `../../LOTS.md` et les données dans `../../DONNEES.md`.

À la remise, fournir ces mêmes commandes avec les arguments et fichiers nécessaires dans ce README. Le lot doit encore compiler et fonctionner après retrait des autres lots. Ne pas ajouter de référence de projet vers le lot d'un coéquipier.
