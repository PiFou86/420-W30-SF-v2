# Vérification Windows — prise de commande

Ce protocole exige l'exécution native sous Windows. La compilation croisée et les tests sans fenêtre ne remplacent pas ces observations. Remplir les résultats, sans les présumer réussis.

- Windows / version .NET / Visual Studio :
- Commit vérifié :
- Mode d'assemblage et format du dépôt :
- Taille de fenêtre et échelle d'affichage :

| Vérification | Résultat attendu | Observation / preuve |
|---|---|---|
| Numéro courant distinct de la saisie de nouvelle commande | Modifier la saisie ne renomme pas la commande courante | À vérifier |
| Ajouter Soupe × 2, Sandwich × 1 | Deux lignes et total 22,00 | À vérifier |
| Un seul clic Ajouter | Un seul ajout | À vérifier |
| Quantité abc | Message de saisie et focus; lignes inchangées | À vérifier |
| Quantité 0 ou négative | Refus attendu; lignes inchangées | À vérifier |
| Commande vide / Enregistrer | Aucun ajout; action désactivée ou refus explicite | À vérifier |
| Sauvegarde réussie | Statut enregistré après réussite; pas de second ajout | À vérifier |
| Même numéro déjà au dépôt | Refus attendu, brouillon conservé | À vérifier |
| Dépôt JSON indisponible ou corrompu lors de l’enregistrement | Message sûr, journal, brouillon conservé et nouvel essai possible | À vérifier |
| Journal indisponible | Message sûr encore visible | À vérifier |
| Annuler Nouvelle / fermeture avec brouillon | Lignes conservées; fermeture annulée | À vérifier |
| Tabulation, touche d'accès, Entrée | Parcours compréhensible; action prévue | À vérifier |
| Redimensionnement / échelle 150 % | Contrôles et messages accessibles | À vérifier |
| Manuel puis cadriciel | Même comportement, portée correctement conservée | À vérifier |

Pour provoquer un incident avec le dépôt fourni, sélectionner le format JSON et un chemin dans un dossier d'entraînement. Utiliser un dossier absent, ou placer un JSON invalide à ce chemin avant Enregistrer. Le dépôt lit le fichier dans Obtenir et Ajouter, pas dans son constructeur : l'incident se produit pendant le cas d'utilisation et doit conserver les lignes. Corriger le dossier ou le contenu, puis réessayer. Le choix du dépôt appartient au point de composition. Ne pas altérer les fichiers d'une autre activité.

Captures attendues : un cas normal et une saisie invalide. Noter les écarts et la correction avant de marquer la vérification réussie.
