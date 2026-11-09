# Exercice 2

**35 min · Intégrer deux changements**

Depuis le dossier `semaine-12` dans Git Bash ou un terminal compatible, lancer `bash preparer_git.sh`. Le script crée le dossier indépendant `bac-a-sable-s12`. Ouvrir sa solution `S12E01E03_Collaboration.slnx` dans le dossier `S12E01E03_Collaboration`. Les branches de pratique restent dans ce bac à sable.

Se placer sur la branche `fonctionnalite/pagination` et intégrer `dev`. Le fichier `RequeteCatalogue.cs` du projet principal entre en conflit. La version de `dev` reconnaît les majuscules et minuscules. L’autre branche trie par numéro avant de découper la page. Résoudre le conflit en gardant les deux intentions. Ajouter une vérification dans le fichier `Program.cs` du projet Terminal ou dans un projet de tests.

La requête de fragment `sOU`, début 0 et taille 2, doit retourner 2 puis 3. Avec début 1 elle donne seulement 3. Compiler et exécuter, puis inscrire la résolution dans un commit de fusion. Vérifier `git status` et expliquer ce que l’index a enregistré.

<details>
<summary>Rappel Git</summary>

Voir [la fiche de commandes](../git/README.md). Quel côté de la fusion est la branche courante?

</details>
