# Vérification Windows — Observer .NET

Exécuter dans Visual Studio sous Windows. Les tests sans fenêtre et la
compilation croisée ne remplacent pas ces observations. Aucun résultat
n’est présumé avant l’essai.

| Parcours | Résultat attendu | Observation |
| --- | --- | --- |
| Ouvrir Facture, ajouter Soupe × 2 puis Sandwich × 1 | Les deux vues évoluent; facture à 24,20, provisoire | À vérifier |
| Essayer un numéro de commande invalide | Message de refus; les lignes restent présentes | À vérifier |
| Fermer Facture, saisir une ligne, puis la rouvrir | État courant immédiat; ajouts suivants reçus une fois | À vérifier |
| Ouvrir deux factures, en fermer une, puis ajouter | La facture restante et la commande continuent de fonctionner | À vérifier |
| Enregistrer une commande valide | Statut enregistré après réussite du dépôt | À vérifier |
| En mode JSON, rendre le fichier invalide, enregistrer, corriger puis réessayer | Message sûr, lignes conservées, puis réussite | À vérifier |

Pour le dernier essai, choisir JSON dans appsettings.json et travailler dans un
dossier d’entraînement. Le chemin relatif est résolu depuis l’exécutable.
Conserver une copie du fichier avant de le rendre invalide; ne pas utiliser
les données d’une autre activité. Les autres essais utilisent mémoire par défaut.

Les quantités invalides restent refusées dans les couches fournies; le
NumericUpDown facilite la saisie. Aucun changement du designer n’est demandé.
