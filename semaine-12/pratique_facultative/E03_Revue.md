# Exercice 3

**45 min · Relire une pull request de pagination**

Dans le bac à sable de l’exercice 2, comparer la branche `revue/pagination` à sa cible `dev` avec `git diff dev...revue/pagination`. Ce diff représente la PR préparée. Si un dépôt GitHub de pratique est disponible, publier ces deux branches et ouvrir la PR dans ce dépôt. Sinon, utiliser une fiche de revue locale avec les mêmes preuves.

Contrat annoncé par l’auteur · « Retourner jusqu’à deux soupes disponibles par page, triées par numéro. Le fragment ignore la casse. »

1. En binôme, attribuer les rôles d’auteur et de relecteur. Le relecteur suit le diff dans `RequeteCatalogue.cs` et compare le comportement annoncé à un résultat reproduit. Il écrit une demande de changement située, avec impact et scénario attendu.
2. L’auteur corrige la branche de revue et ajoute un cas reproductible. Le relecteur valide le nouveau diff et consigne le scénario, le résultat et le commit relu.
3. Fusionner la branche revue vers `dev` après validation, puis compiler et rejouer le scénario sur `dev`. Consigner la décision et le résultat après fusion.

À la fin, chacun doit pouvoir expliquer pourquoi son commentaire permet à l’auteur de vérifier le correctif. Utiliser une PR distincte du conflit résolu dans l’exercice précédent.
