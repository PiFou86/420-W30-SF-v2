# Exercice 3

## Mission et durée

Environ 45 minutes. Dans la solution de l'exercice 1, préparez un affichage sans donner au domaine la responsabilité de l'écran.

## Travail demandé

1. Définissez un `ReservationDto` de sortie dans Application : numéro et titre. La conversion peut utiliser un constructeur recevant l'entité `Reservation`; l'entité ne connaît pas le DTO.
2. Définissez un `ReservationViewModel` dans le projet Terminal : libellé d'affichage et texte d'état adapté à la vue. Expliquez pourquoi ce type n'est ni l'entité ni le DTO de l'Application.
3. À la frontière du projet Terminal, interceptez une erreur technique lors de la lecture du dépôt JSON. Affichez avec `Console.Error.WriteLine` un message sûr, sans chemin complet ni trace d'appel. Journalisez séparément le type de l'exception et l'opération tentée; n'incluez pas de secret.
4. Dans `DECISIONS.md`, classez les cinq responsabilités suivantes : invariant de `Reservation`, recherche par numéro, lecture JSON, construction du DTO, formatage du libellé pour l'écran.
5. Dans le projet de tests, vérifiez la conversion DTO et le ViewModel avec de vrais objets. Nommez les classes `ReservationDtoTests` et `ReservationViewModelTests`.

<details>
<summary>Rappel des semaines précédentes</summary>

Revoir le [classement des responsabilités](./E02-couches-dto.md). L'entité doit-elle connaître la forme de l'écran ou le contenu du fichier?

</details>
