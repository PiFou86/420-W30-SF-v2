# Vérifications — Contacts S9

Ce protocole concerne le départ complété et les corrigés Contacts.

Ouvrir `S09E01E02_Contacts.slnx` avec Visual Studio sous Windows et .NET 10. Utiliser un fichier de données de démonstration et noter, pour soi ou pour le suivi enseignant, l'état E1/E2, le mode (`manuel` ou `cadriciel`), la taille de fenêtre et l'échelle Windows. Aucun document supplémentaire n'est à remettre par l'étudiant.

## E1 — les deux modes fournis

- [ ] Le designer de `FormContacts` s'ouvre; le formulaire démarre avec les collaborateurs injectés.
- [ ] Nom et courriel saisis : confirmation après écriture; le terminal ou une relecture confirme le contact.
- [ ] Nom/courriel vide : refus; les champs restent présents.
- [ ] Même courriel avec casse différente : refus, aucune deuxième ligne persistée.
- [ ] Effacer vide les champs; focus au nom. Tab, Alt+touches d'accès et Entrée fonctionnent.
- [ ] Un clic crée une seule fois; pas de double abonnement.

## E2 — les deux modes fournis

- [ ] Le designer ouvre le formulaire enrichi sans erreur. Les contrôles d'E1 sont conservés.
- [ ] Liste vide au premier chargement, compteur cohérent; trois créations successives puis relecture après relancement.
- [ ] Recherche par nom et courriel, casse différente et espaces extérieurs : résultats corrects.
- [ ] Recherche vide/blanche : tous; introuvable : grille vide et « Aucun résultat ».
- [ ] Tous annule le filtre. Créer pendant un filtre réaffiche tous et vide les trois champs.
- [ ] Refus : saisie préservée. Grille en lecture seule, sans ligne d'ajout/suppression ni colonne identifiant.
- [ ] Incident de lecture sur une copie de JSON volontairement malformée : message sûr, fichier conservé, ancienne grille non vidée. Réparer puis réessayer.
- [ ] Incident de création : utiliser un chemin vers un dossier inexistant au lancement ou un emplacement non accessible; message sûr, saisie conservée; corriger puis relancer/réessayer. Le journal contient opération/type sans chemin ni trace brute.
- [ ] Si l'enregistrement a réussi mais que la consultation suivante échoue, le message distingue création acquise et actualisation impossible (inspection du traitement puis simulation si possible).
- [ ] Parcours clavier de création/recherche, focus, petite et grande fenêtre, échelle 100 % puis 150 % : rien d'utile n'est masqué.

Rétablir les fichiers et la configuration après les simulations.

## Aller-retour dans le designer

- [ ] Sur une copie de travail, ouvrir chaque formulaire dans le designer;
  vérifier que les contrôles et les colonnes attendus sont présents.
- [ ] Déplacer légèrement un contrôle (ou ajouter un contrôle provisoire au
  départ vide), enregistrer, fermer le designer puis le rouvrir.
- [ ] Vérifier qu'il reste un seul champ `components`, sans champ `m_components`,
  et que `Dispose` utilise ce même champ. Le conteneur peut rester nul tant
  qu'aucun composant non visuel ne le nécessite.
- [ ] Ajouter provisoirement un `Timer` depuis la boîte à outils : le designer
  utilise le même conteneur `components`; supprimer ensuite ce composant d'essai.
- [ ] Vérifier les noms, les dispositions, les valeurs, les colonnes et les
  abonnements. Un premier enregistrement peut normaliser le code généré;
  un deuxième aller-retour ne doit perdre aucun élément ni doubler un événement.
- [ ] Rétablir la modification d'essai, compiler et lancer : chaque clic exécute
  une seule action, les grilles restent en lecture seule et les dialogues
  conservent leur validation, leur annulation et leur fenêtre propriétaire.

Exécuter ce contrôle pour le départ et les deux états du corrigé `FormContacts`.
