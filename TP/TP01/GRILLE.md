# Grille d'évaluation du TP01

## Partie commune — 25 points

| Critère | Points | Preuves |
|---|---:|---|
| Attribution, conventions, contrats et règles de dépendance | 10 | AUTHORS, conventions, solutions individuelles autonomes, contrats propres à chaque lot |
| Diagramme Mermaid | 5 | Lots requis, relations pertinentes, dépendances des quatre couches et contributions attribuées |
| Intégration et traçabilité commune | 10 | Procédure reproductible, solution d'équipe, PR/revues et état des lots au commit final |

## Partie individuelle — 75 points, appliquée séparément à chaque lot

| Critère | Points | Preuves individuelles |
|---|---:|---|
| Domaine et algorithme du lot | 15 | Invariants, comportements, cas normal et limites propres à sa fiche |
| Cas d'utilisation et autonomie | 10 | Orchestration par contrat, `Result<T>` pour refus attendu, aucun projet d'un autre lot requis |
| Infrastructure | 10 | Mémoire et JSON, conversion validée, configuration, fichier absent/invalide et message sûr/journal |
| Présentation et parcours utilisable | 5 | Console simple, composition, README de lancement et DTO adaptés |
| Tests et contrats | 15 | Tests propres au lot : domaine, refus sans ajout, dépôt mémoire, nouvelle instance fichier, limites et incident technique |
| Git, attribution et revue | 10 | Journal individuel, commits/PR identifiés, tests avant/après fusion, revue expliquée et aide attribuée |
| Capsule individuelle | 10 | Parcours dans son lot, algorithme, limite, choix de conception, test et commit; maximum cinq minutes |
| **Sous-total individuel** | **75** | |

**Note d'une personne = points communs + points de son propre lot.** Le lot Commandes, le lot Menu et, s'il existe, le lot Réservations utilisent cette même grille. Un binôme n'est pas évalué sur Réservations. Le troisième membre bénéficie d'un lot complet de même nature et du même barème; il n'est pas seulement responsable de documentation ou d'intégration.

## Procédure d'évaluation indépendante

1. Lire AUTHORS et le journal pour identifier la personne, son lot et le commit de référence.
2. Copier uniquement `lots/NomDuLot` dans un dossier temporaire; compiler sa solution, exécuter ses tests et lancer son parcours avec ses données. Aucun fichier d'un autre lot ni dossier `commun/` n'est requis.
3. Vérifier les cas de sa fiche et de DONNEES, puis la correspondance avec sa capsule et ses contributions. Une trace de commit contribue à l'attribution mais ne prouve pas seule la compréhension; le test expliqué et la capsule servent aussi de preuves.
4. Attribuer ses 75 points sur ces preuves. Une erreur d'un autre lot ou de la solution d'équipe ne retire pas de points individuels à une solution isolée qui fonctionne.
5. Évaluer séparément les 25 points communs. En cas de membre absent ou de lot non livré, conserver l'évaluation individuelle; apprécier les critères communs sur les éléments effectivement remis et les preuves attribuées, sans transférer automatiquement la charge d'un absent.

Une incapacité de compilation dans **son propre lot** limite les preuves exécutables de cette personne; les éléments observables restants sont appréciés selon leur critère. L'enseignant ne doit pas réparer le lot d'un coéquipier pour pouvoir examiner le travail d'une autre personne.
