# Guide de révision active — Semaine 6

## Révision cumulative de l’examen 1

Ce guide aide à organiser la révision des semaines 1 à 5. Il ne remplace pas l’examen blanc. Gardez le corrigé étudiant fermé pendant la simulation, puis utilisez-le pour repérer les notions que vous pouvez expliquer sans notes et celles qui demandent encore du travail.

## Méthode de révision active

Pour chaque question :

1. répondez sans consulter les notes;
2. nommez la notion qui justifie votre réponse;
3. décrivez le comportement observable ou le risque;
4. proposez ensuite une correction;
5. vérifiez avec les notes et réécrivez toute réponse imprécise.

Une bonne justification relie trois éléments : **le problème**, **son effet** et **la correction proposée**.

## Matière cumulative

| Bloc | Notions à réviser |
|---|---|
| Modèle objet | encapsulation, interfaces, polymorphisme, membres d’instance et de classe |
| Tests | structure AAA, exceptions, doublures manuelles et Moq |
| Dépendances | injection, conteneur, DIP, contrats et substitution |
| Git | répertoire de travail, index, commit local, branche locale et référence distante |
| Qualité | SRP, CQS, loi de Déméter et Tell, Don’t Ask |
| SOLID | OCP, LSP, ISP et liens avec DIP et SRP |
| Strategy | contexte, stratégie, stratégies concrètes, injection, délégation et point de composition |
| Structure | héritage, composition, commande/requête et membres statiques |

## Bloc 1 — Encapsulation et types

Vous devriez pouvoir :

- expliquer pourquoi retourner une liste mutable expose l’état interne;
- proposer une vue ou une copie qui empêche la modification directe de la collection;
- préciser qu’une copie de liste ne rend pas immuables les objets qu’elle contient;
- distinguer le type déclaré de la classe concrète;
- déterminer quels membres sont accessibles par une variable typée avec une interface;
- expliquer qu’une interface ne peut pas être instanciée directement;
- distinguer une variable d’objet d’une variable de classe.

### Questions de rappel actif

1. Quel membre détermine ce qui est accessible à la compilation?
2. Pourquoi `IExportable element = new IExportable();` ne compile-t-il pas?
3. Comment appelle-t-on une méthode de classe sans créer d’objet?
4. Quelle limite subsiste lorsqu’une propriété retourne une copie d’une liste d’objets mutables?

## Bloc 2 — Tests et doublures

Structurez un test avec :

- **Arranger** : préparer le sujet et ses collaborateurs;
- **Agir** : exécuter une seule action principale;
- **Auditer** : vérifier le résultat observable.

Une doublure manuelle suffit souvent lorsqu’une interface est courte et que le comportement nécessaire au test est simple. Moq devient utile lorsque la configuration ou la vérification des interactions serait autrement répétitive.

### Questions de rappel actif

1. Quel comportement précis le test doit-il observer?
2. Le test vérifie-t-il un résultat ou une interaction?
3. Une implantation en mémoire, un simulacre manuel ou Moq convient-il mieux?
4. Le test dépend-il inutilement d’un fichier, de la console ou du réseau?

## Bloc 3 — Dépendances et substitution

Assurez-vous de distinguer :

- **DI** : fournir le collaborateur de l’extérieur;
- **DIP** : orienter les dépendances vers un contrat stable;
- **conteneur** : automatiser la construction et l’assemblage;
- **point de composition** : endroit où l’application choisit les implantations concrètes.

Vous devriez pouvoir remplacer un dépôt technique par un dépôt en mémoire sans modifier le service client.

## Bloc 4 — États Git

Reliez chaque commande à l’état qu’elle consulte ou modifie.

| État | Questions à vous poser |
|---|---|
| Répertoire de travail | Quels fichiers ont été modifiés? |
| Index | Quels changements feront partie du prochain commit? |
| Commit local | Quel instantané a été enregistré? |
| Branche locale | Vers quel commit le nom de branche pointe-t-il? |
| Référence distante | Quelle est la dernière information connue sur le dépôt distant? |

Ne mémorisez pas seulement une suite de commandes. Expliquez le changement d’état produit par chacune.

## Bloc 5 — Qualité des collaborations

| Principe | Diagnostic |
|---|---|
| SRP | La classe change-t-elle pour plusieurs raisons distinctes? |
| CQS | La méthode mélange-t-elle modification et consultation? |
| Loi de Déméter | Le client traverse-t-il plusieurs structures internes? |
| Tell, Don’t Ask | Le client extrait-il des données pour décider à la place de l’objet? |

### Méthode de justification

1. Montrez le fragment problématique.
2. Nommez l’information ou la responsabilité mal placée.
3. Expliquez le risque observable.
4. Proposez un message métier ou un collaborateur plus approprié.

## Bloc 6 — OCP, LSP et ISP

- **OCP** : prévoir un point d’extension pour une variation réelle sans réécrire le code stable.
- **LSP** : préserver les attentes promises par le type de base.
- **ISP** : éviter qu’un client dépende d’opérations qu’il n’utilise pas.

### Questions de rappel actif

1. Quelle variation ferait grandir une cascade conditionnelle?
2. Quel comportement stable peut devenir un contrat?
3. Quel sous-type modifie une attente du type de base?
4. Quel client dépend d’un membre d’interface inutile?
5. La composition exprimerait-elle mieux la relation que l’héritage?

## Bloc 7 — Strategy

Strategy organise une famille d'algorithmes interchangeables :

- le **contexte** utilise le comportement variable;
- la **stratégie** définit le contrat dont le contexte a besoin;
- les **stratégies concrètes** implantent les variantes;
- le **point de composition** choisit et fournit une stratégie;
- le contexte **délègue** sans sélectionner le type concret.

### Méthode de reconnaissance

1. Repérez la condition ou la règle qui varie.
2. Nommez ce qui devrait rester stable.
3. Formulez l'opération commune sous forme de contrat.
4. Attribuez les quatre rôles.
5. Vérifiez que l'assemblage choisit la stratégie et que le contexte délègue.

### Questions de rappel actif

1. Pourquoi une interface et deux implantations ne suffisent-elles pas toujours à conclure qu'il s'agit de Strategy?
2. En quoi le polymorphisme est-il un mécanisme plutôt qu'un patron?
3. Comment Strategy soutient-il OCP?
4. Comment DIP oriente-t-il la dépendance du contexte?
5. Pourquoi déplacer une cascade conditionnelle dans une autre méthode ne résout-il pas nécessairement le problème?

## Bloc 8 — CQS et membres statiques

Une **commande** modifie l'état; une **requête** le consulte sans effet observable.
Une méthode `static` appartient à la classe et s'appelle par son nom, sans objet.

### Questions de rappel actif

1. Comment séparer une méthode qui confirme une réservation et retourne son statut?
2. Quel état appartient à chaque objet `Reservation`?
3. Quand une opération utilitaire est-elle réellement indépendante d'une instance?

## Préparer la simulation de 75 minutes

Avant de commencer :

1. fermez les notes, le corrigé et les solutions d'exercices;
2. préparez un chronomètre de 75 minutes;
3. prévoyez une courte relecture dans votre gestion du temps;
4. répondez dans l'ordre qui vous aide à accumuler des points sans rester bloqué.

Pendant la simulation, marquez une question incertaine et poursuivez. Ne cherchez pas seulement le nom d'un principe : montrez le symptôme, son effet et une correction cohérente.

Après la simulation :

1. fermez votre copie avant d'ouvrir le corrigé étudiant;
2. corrigez avec une couleur différente;
3. distinguez une notion non comprise d'une justification imprécise ou d'une erreur de code;
4. choisissez au plus deux priorités de révision;
5. refaites une courte question différente pour vérifier le transfert.

Le format de la simulation sert à pratiquer la gestion du temps. Il ne permet pas de déduire l'ordre, les contextes ou la pondération d'une autre évaluation.

## Simulation personnelle complémentaire

Choisissez un court extrait de code et accordez-vous quelques minutes pour produire une réponse complète :

1. prédire s’il compile;
2. nommer le principe ou le contrat concerné;
3. expliquer le comportement ou le risque;
4. proposer une correction limitée à la demande;
5. relire les conventions C# du cours.

Après la simulation, classez chaque notion dans l’une des catégories suivantes :

- je peux l’expliquer et l’appliquer sans notes;
- je reconnais la notion, mais ma justification reste imprécise;
- je dois revoir l’exemple et refaire une question.

## Confusions fréquentes

- Corriger du code sans expliquer le problème observable.
- Confondre type déclaré, classe concrète et objet créé.
- Utiliser Moq alors qu’une doublure simple suffit, ou tester un détail interne sans raison.
- Croire qu’une interface garantit automatiquement DIP.
- Confondre loi de Déméter et Tell, Don’t Ask.
- Choisir l’héritage uniquement pour réutiliser du code.
- Confondre Strategy avec tout usage du polymorphisme.
- Relier Strategy seulement à OCP sans expliquer DIP, la composition et le point de composition.
- Déplacer la condition sans rendre le comportement interchangeable.
- Confondre une commande et une requête qui devrait rester sans effet observable.
- Appeler une méthode de classe comme si elle appartenait à une instance.

## Autoévaluation

- [ ] Je justifie mes réponses avec le bon principe.
- [ ] Je distingue ce qui ne compile pas de ce qui compile mais viole une règle de conception.
- [ ] Mon code respecte les conventions C# du cours.
- [ ] Mes tests distinguent clairement Arranger, Agir et Auditer.
- [ ] Je peux expliquer DI, DIP, conteneur et substitution.
- [ ] Je distingue SRP, CQS, Déméter et Tell, Don’t Ask.
- [ ] Je peux appliquer OCP, LSP et ISP à un cas concret.
- [ ] Je justifie le choix entre héritage et composition.
- [ ] Je reconnais le contexte, la stratégie, les stratégies concrètes et le point de composition.
- [ ] Je peux montrer l'injection et la délégation sans condition sur le type concret.
- [ ] Je distingue Strategy du polymorphisme et je le relie à OCP et DIP.
- [ ] Je distingue commande, requête, méthode d'instance et méthode de classe.

## Lectures ciblées

- Revoir les chapitres mobilisés depuis le début de la session selon vos résultats d’autoévaluation.
- Prioriser les chapitres 8, 9, 11, 12 et 15 pour les dépendances, les tests, la qualité, Strategy et Git.
