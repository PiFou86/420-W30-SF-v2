# Exercice 2

## Mission et durée

Environ 25 minutes. Répartissez un cas d'utilisation entre les quatre couches et corrigez une conversion qui inverse une dépendance.

## Situation

Une classe lit la saisie, charge une réservation depuis un fichier JSON, vérifie la disponibilité d'une plage horaire, modifie son statut, construit un texte d'affichage et retourne `ReservationDto`. L'entité `Reservation` possède aussi une méthode qui retourne ce DTO. Il s'agit d'une activité d'analyse avec horaires; elle n'exige pas d'ajouter les conflits de disponibilité au dépôt numéro/titre des exercices 1 et 3. Aucun code de départ n'est nécessaire.

## Travail demandé

1. Dans `DECISIONS.md`, classer : lire la saisie et afficher; charger et orchestrer la confirmation; protéger une plage valide et les règles de disponibilité; lire JSON et journaliser un incident technique; transporter numéro et statut; formater le libellé pour une vue. Employer les noms complets présentation / Application / domaine / Infrastructure avant une abréviation.
2. Tracer les références de compilation : présentation → Application; Application → domaine et ses abstractions; Infrastructure → contrats et domaine. Ajouter la dépendance technique de la racine de composition vers Infrastructure et la distinguer de la dépendance du cas d'utilisation.
3. Proposer les signatures du service `ConfirmerReservationService`, de la méthode `Confirmer()` de l'entité et de `ReservationDto`. Réinvestir `Result<ReservationDto>` pour une plage occupée ou une réservation absente. Préciser quel objet connaît les autres plages : une réservation isolée ne peut pas découvrir seule un chevauchement. Le domaine protège ses invariants lorsqu'il est appelé directement.
4. Déplacer la conversion entité → DTO hors de l'entité. Comparer un constructeur du DTO recevant l'entité et une classe de conversion dédiée, utile si la transformation est complexe ou réutilisée. Une conversion DTO → entité doit repasser par les invariants du domaine; l'entité ne dépend pas du DTO.
5. Distinguer entité, POCO, DTO de sortie Application, DTO de persistance Infrastructure et ViewModel de présentation. Un objet C# simple n'est pas automatiquement un modèle métier.

Point de contrôle : tableau de responsabilités, schéma de dépendances et justification de conversion. Utiliser ensuite cette analyse pour examiner une responsabilité mal placée dans votre TP01; conserver son comportement avec un test ou une vérification observable avant de la déplacer.
