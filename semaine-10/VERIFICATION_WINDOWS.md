# Vérification Windows — MVC, Observer et facturation

Exécuter nativement sous Windows. La compilation croisée et les tests portables ne prouvent pas le comportement réel des formulaires. Conserver « À vérifier » tant que l’observation n’a pas été réalisée.

- Windows / .NET / Visual Studio :
- Commit et mode manuel ou cadriciel :
- Format du dépôt, taille de fenêtre et échelle :

| Vérification | Résultat attendu | Observation / preuve |
|---|---|---|
| Ouvrir la facture après ajout de lignes | État courant immédiat | À vérifier |
| Soupe × 2, Sandwich × 1 avec facture ouverte | Sous-total 22,00; frais 2,20; total 24,20 | À vérifier |
| Un clic Ajouter | Une ligne ajoutée; une notification par vue | À vérifier |
| Deux fenêtres de facture ouvertes | Même état, aucune connaissance mutuelle | À vérifier |
| Quantité abc puis 0 | Saisie puis refus distincts, aucune ligne ajoutée | À vérifier |
| Enregistrement réussi | Statut provisoire retiré après sauvegarde | À vérifier |
| JSON invalide ou dossier absent lors d’Enregistrer | Message sûr, brouillon conservé, facture provisoire | À vérifier |
| Réessayer après correction du fichier/dossier | Réussite, état partagé cohérent | À vérifier |
| Fermer la facture, puis ajouter | Ancienne fenêtre détachée, commande utilisable | À vérifier |
| Réouvrir la facture | État courant et abonnement unique | À vérifier |
| Annuler Nouvelle / fermeture avec brouillon | Lignes et facture inchangées | À vérifier |
| Fermer la fenêtre propriétaire | Fenêtres secondaires fermées et abonnements terminés | À vérifier |
| Clavier, raccourcis et échelle 150 % | Affichages et messages accessibles | À vérifier |
| Manuel puis cadriciel | Même comportement et un seul modèle partagé | À vérifier |
| Frontières des méthodes de vue, dans un test Windows ou le débogueur | Null/blanc refusés : FormFacture(null), Actualiser(null), PresenterRefus/Incident(blanc), Relier(null/null) et second Relier | À vérifier |

Les contrôles de frontière sont faits dans un environnement d’essai, pas dans les boutons du produit. Pour simuler le dépôt, utiliser JSON dans un dossier d’entraînement avec un document invalide ou un dossier absent au moment d’Enregistrer. Le dépôt lit dans Obtenir/Ajouter, pas dans son constructeur. Corriger le dossier/contenu puis réessayer; ne pas altérer les données d’une autre activité.

Captures : fenêtre de commande et facture cohérentes; puis une erreur sûre. Le compteur des notifications dans les tests fournit la preuve portable; le débogueur peut compléter l’observation d’une fenêtre fermée.
