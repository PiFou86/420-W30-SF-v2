# S04E01E03_Restaurant_Qualite

Cette solution .NET cumulative sert aux exercices 1 à 3 de la semaine 4. Le
projet principal `S04E01E03_Restaurant_Qualite` contient
volontairement une dépendance concrète, plusieurs responsabilités et une chaîne
d’appels à réusiner. Dans le projet Terminal, conservez l’assemblage manuel et
ajoutez l’assemblage avec le cadriciel. Dans le projet de tests, comparez une
doublure manuelle avec son équivalent utilisant Moq.

```bash
dotnet test S04E01E03_Restaurant_Qualite.slnx
dotnet run --project S04E01E03_Restaurant_Qualite.Terminal
dotnet run --project S04E01E03_Restaurant_Qualite.Terminal -- --manuel
```

Sans argument transmis au projet Terminal, le programme doit employer
l’assemblage réalisé avec le cadriciel. Avec l’argument `--manuel`, il doit
exécuter la fonction d’assemblage manuel conservée comme point de comparaison.
