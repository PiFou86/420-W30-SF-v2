# Grille d'évaluation du TP01

## Partie commune — 25 points

| Critère | Points | Preuves |
|---|---:|---|
| Attribution, contrats et règles de dépendance | 10 | AUTHORS, solutions individuelles autonomes, interfaces de dépôt et références de projets conformes aux règles de dépendance |
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

Les normes du cours sont vérifiées directement dans le code et la structure des projets, au sein des critères concernés. Aucun document de conventions ni point supplémentaire n'est demandé.

**Note d'une personne = points communs + points de son propre lot.** Le lot Commandes, le lot Menu et, s'il existe, le lot Réservations utilisent cette même grille. Un binôme n'est pas évalué sur Réservations. Le troisième membre bénéficie d'un lot complet de même nature et du même barème; il n'est pas seulement responsable de documentation ou d'intégration.

Une incapacité de compilation dans **son propre lot** limite les preuves exécutables de cette personne; les éléments observables restants sont appréciés selon leur critère. L'enseignant ne doit pas réparer le lot d'un coéquipier pour pouvoir examiner le travail d'une autre personne.
