# Exercice 5

## Mission et durée

En 35 minutes, répartissez un cas d'utilisation entre la présentation,
Application et le domaine, puis corrigez une dépendance inversée entre une
entité et son DTO.

## Situation

Une commande doit être confirmée, puis son numéro et son statut doivent être
affichés. Le code actuel lit la saisie, vérifie les lignes, modifie le statut,
construit le texte d'affichage et retourne un `CommandeDto` dans une même
classe. L'entité `Commande` possède aussi une méthode qui retourne ce DTO.

## Travail demandé

1. Dans votre fichier Markdown de réponse, classez les responsabilités
   suivantes entre la couche présentation, la couche Application et le
   domaine :
   - lire la saisie et afficher un message;
   - charger une commande et orchestrer sa confirmation;
   - refuser la confirmation d'une commande vide;
   - transporter le numéro et le statut vers la présentation.
2. Dans le même fichier Markdown, dessinez le sens des dépendances
   `présentation → Application → domaine`.
3. Proposez la signature minimale de la classe de service applicatif
   `ConfirmerCommandeService`, de la méthode `Confirmer()` de l’entité
   `Commande` et de la classe de transfert `CommandeDto`.
4. Proposez un nouvel emplacement pour le code de conversion afin que l’entité
   `Commande` ne dépende pas de la classe `CommandeDto`.
5. Expliquez quand vous préféreriez une classe de conversion dédiée à un
   constructeur de DTO recevant l'entité.

Terminez par une justification de quatre phrases : une phrase par couche ou
objet concerné. Aucun cadriciel d'interface graphique ni accès fichier n'est
nécessaire.
