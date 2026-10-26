# Vérification Windows du code de préparation

À réaliser sous Windows; aucune case n'est présumée vérifiée par la compilation
croisée sous macOS ou par les tests portables.

- [ ] Ouvrir les deux formulaires de Commandes.Presentation.WinForms dans le designer : les fichiers `partial` et les contrôles sont accessibles.
- [ ] Lancer en mode manuel : aucune commande et boutons Ajouter/Enregistrer désactivés.
- [ ] Observer au débogueur : constructeur du dialogue, puis Load, Shown, clic de validation, retour de ShowDialog.
- [ ] Après `new`, inspecter Result : null et aucune commande créée; la fenêtre n'est pas encore visible.
- [ ] Annuler par bouton, Échap puis croix : aucun appel à Service.Creer et état courant conservé.
- [ ] Valider 1042 : Result contient le numéro, DialogResult vaut OK et la commande vide apparaît.
- [ ] Choisir Sandwich, quantité 2 : une ligne, total 16,00 $, enregistrement disponible.
- [ ] Ouvrir une nouvelle création, refuser l'abandon : lignes et numéro courants conservés.
- [ ] Accepter l'abandon : nouvelle commande vide, aucun enregistrement implicite.
- [ ] Enregistrer une commande, en créer une autre et tenter un numéro déjà enregistré : message de refus et état conservé.
- [ ] Fermer avec des lignes non enregistrées : Non conserve la fenêtre, Oui permet la fermeture.
- [ ] Refaire le parcours en mode cadriciel.
- [ ] Refaire avec JSON, relancer puis vérifier le refus d'un numéro persisté.
- [ ] Avec JSON dans un dossier inexistant : message sûr, brouillon conservé, nouvelle tentative possible après correction.
- [ ] Vérifier Tab, Entrée, Échap, focus, redimensionnement et mise à l'échelle à 125 % et 150 %.
- [ ] Vérifier les titres français des colonnes et le format monétaire fr-CA.


## Deuxième exécutable : Commandes.Portee.WinForms

Ces contrôles natifs restent à réaliser sous Windows.

- [ ] Ouvrir FormAccueil dans le designer, ainsi que les deux formulaires partagés; vérifier noms, événements et propriété Mode.
- [ ] Lancer le premier exécutable en mode manuel puis cadriciel : la création reste visible et l'enregistrement ne ferme pas la fenêtre principale.
- [ ] Lancer le deuxième exécutable en mémoire : grille vide, boutons Nouvelle commande et Actualiser disponibles.
- [ ] Annuler le choix du numéro par bouton, Échap et croix : retour à l'accueil, grille inchangée et trace de libération du témoin.
- [ ] Valider le numéro, ajouter Sandwich × 2 et enregistrer : l'éditeur se ferme, la portée est libérée et la grille d'accueil affiche une ligne à 16,00 $.
- [ ] Vérifier dans l'éditeur que la création d'une autre commande est masquée, que le focus est utilisable et que le bouton Ajouter répond à Entrée.
- [ ] Répéter avec un autre numéro : identifiant de témoin différent et nouvelle saisie vide; première commande toujours dans la liste.
- [ ] Essayer un numéro déjà enregistré : message de refus, numéro conservé pour correction et aucune commande remplacée.
- [ ] Fermer l'éditeur avec des lignes non enregistrées : Non maintient le dialogue, Oui abandonne puis libère la portée sans modifier le dépôt.
- [ ] Fermer l'éditeur vide : retour sans enregistrement et sans demande d'abandon inutile.
- [ ] Cliquer sur Actualiser : seuls numéro, nombre de lignes et total sont affichés; la grille est en lecture seule et les commandes sont triées.
- [ ] Refaire avec JSON, relancer et vérifier la relecture des commandes déjà enregistrées.
- [ ] En mode JSON, simuler un incident après création : Enregistrer conserve le dialogue et ses lignes, permet un nouvel essai et n'affiche aucun détail technique.
- [ ] Simuler une erreur d'actualisation : ancienne liste conservée avec avertissement; Actualiser fonctionne après correction.
- [ ] Vérifier les titres des dialogues, les traces du témoin dans Débogage, la navigation clavier, le redimensionnement et l'affichage à 125 % et 150 %.

## Aller-retour dans le designer — à exécuter sous Windows

Ces observations ne sont pas réalisées sous macOS. Une compilation sans
avertissement ne suffit pas à les cocher.

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

Formulaires concernés : `FormNouvelleCommande`, `FormCommande` et `FormAccueil`.


## Disposition avec Anchor — observations Windows restantes

- [ ] Les contrôles sont directement sur le formulaire, sans panneau de disposition.
- [ ] Agrandir puis réduire jusqu'à la taille minimale : les champs/grilles
  ancrés à gauche et à droite s'élargissent; la grille ancrée aux quatre bords
  change aussi de hauteur. Aucun contrôle ne se recouvre.
- [ ] Les boutons et messages ancrés en bas ou à droite gardent leurs marges.

Ces observations restent à exécuter nativement sous Windows; aucune capture
native ni validation du redimensionnement n'est présumée sous macOS.

## Déclarations des colonnes après enregistrement du designer

- [ ] Les champs de contrôles/colonnes sont déclarés sans `= null!`; leur création
  se trouve dans `InitializeComponent` et utilise bien les champs, sans variable
  locale du même nom.
- [ ] Enregistrer puis rouvrir le designer : toutes les colonnes sont toujours
  ajoutées à la grille; `AutoGenerateColumns` reste désactivé pour les grilles
  explicites. Les noms de propriétés de liaison et formats sont conservés.
- [ ] Recompiler sans avertissement de champ inutilisé; lancer et vérifier que
  les colonnes monétaires et le total sont affichés sans référence nulle.
- [ ] Les métadonnées `UserAddedColumn=True` sont présentes dans le `.resx`
  pour chaque colonne ajoutée manuellement; après deux enregistrements du
  designer, aucune colonne ne disparaît et les champs ne deviennent pas inutilisés.
