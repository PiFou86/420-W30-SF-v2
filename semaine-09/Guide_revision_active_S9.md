# Guide de révision active — Semaine 9

## Méthode

Fermer les notes, répondre à une question, puis vérifier avec les sources. Associer chaque réponse à un extrait, un schéma, un test ou une observation. Refaire les questions mal justifiées après un délai; remplir la grille sans recopier des définitions.

Les exercices actuels suivent une progression contacts : fiche de création en E1, puis liste et recherche en E2. Les diapositives et la préparation enseignante utilisent les commandes pour illustrer les mêmes notions. Les [énoncés](../../Exercices/semaine-09/README.md) restent dans le dépôt étudiant. Ce guide ne rajoute aucun livrable au travail ramassé.

## 1. Changer de présentation

Sans notes, expliquer quelles responsabilités restent identiques si le terminal de création de contacts devient WinForms. Nommer présentation / Application / domaine / Infrastructure, puis tracer les références de projets.

1. Qui construit la demande depuis les champs? Qui construit l'entité?
2. Où sont vérifiés les champs requis et le courriel déjà utilisé?
3. Qui choisit le dépôt JSON et son chemin? Un événement Click doit-il faire ce choix?
4. Pourquoi le même cas d'utilisation reste-t-il appelable sans fenêtre?
5. Le projet WinForms peut-il référencer Infrastructure pour son point de composition sans lire le JSON dans ses événements?

Preuve : tracer le parcours clic Créer → DemandeCreationContact → CreerContact.Executer → dépôt → ContactDto → affichage. Distinguer références de compilation et appels à l'exécution. Comparer avec le parcours d'une commande montré en cours.

## 2. Formulaire, objet et designer

Reconstituer le rôle de `FormContacts.cs`, `FormContacts.Designer.cs` et `InitializeComponent()`. Expliquer `partial` avec ses propres mots.

- Quelle classe fournit le comportement d'une fenêtre?
- Quand les contrôles existent-ils? En quoi l'instanciation diffère-t-elle de l'événement Load?
- Pourquoi le constructeur sans service aide-t-il le designer, alors que le lancement injecte les collaborateurs?
- Changer Text renomme-t-il Name ou la variable d'objet?
- Que signifient les préfixes tbNom, lNom, bCreer, dgvContacts et nudQuantite?
- Comment un seul clic pourrait-il déclencher deux tentatives de création?

Preuve : repérer création d'un contrôle, propriété, événement et gestionnaire dans le code. Une fenêtre reste une instance d'une classe; ses méthodes et propriétés suivent les mêmes règles POO.

## 3. Choisir les contrôles et organiser les zones

Choisir un contrôle pour un nom saisi, un libellé, une action, une liste de contacts et un compteur. Puis comparer avec une quantité et un choix de produit dans l'exemple de commande.

Réinvestir Label, TextBox, Button, NumericUpDown, ListBox, DataGridView et MenuStrip. Expliquer OpenFileDialog et SaveFileDialog : qui choisit le chemin, qui lit le contenu, que faire après Annuler? Un dialogue ne constitue pas un dépôt.

Preuve : dessiner les zones création / recherche / résultats / état et nommer les contrôles. Justifier une grille en lecture seule et les propriétés qui empêchent ajout et suppression directs.

## 4. Demande, refus et incident

Prédire le résultat et le nombre d'ajouts au dépôt :

| Situation | Prédiction à vérifier |
|---|---|
| Nom blanc | Demande possible, refus, focus? |
| Courriel vide | Message, saisie conservée, ajout? |
| Courriel déjà utilisé avec une autre casse | Succès ou refus? |
| Nouvelle demande valide | Moment où la confirmation devient vraie? |
| JSON malformé ou écriture impossible | Incident, message sûr, journal, nouvel essai? |

Réinvestir Result<T> : EstSucces, Valeur, MessageErreur, Succes et Echec. Pourquoi consulter Valeur après un refus est-il une erreur? Quelle différence entre une demande invalide transportée par l'UI et une entité valide du domaine?

Preuve : retrouver un test de refus sans écriture et un test de relecture après création. Les incidents ne deviennent pas « courriel déjà utilisé ». Expliquer pourquoi le message utilisateur ne montre pas de chemin ni de trace brute.

## 5. Consultation, recherche et actualisation

Expliquer le trajet mot-clef → cas de consultation → dépôt → DTO → grille. La recherche concerne une sous-chaîne dans le nom ou le courriel, sans distinction de casse; vérifier aussi les espaces extérieurs, une recherche vide et l'absence de résultat.

- Pourquoi le formulaire ne devrait-il pas refaire le filtrage du dépôt?
- Pourquoi une consultation ne doit-elle pas réécrire le fichier?
- Comment vérifier que l'ordre du dépôt est conservé?
- Pourquoi montrer zéro résultat plutôt que conserver une ancienne recherche à l'écran?
- Après création, quels champs et quel filtre vider? Quelle liste réafficher?
- Après refus ou incident, quelles saisies et quelle ancienne liste préserver?
- Comment annoncer une création réussie si l'actualisation qui suit échoue?

Preuve : relier grille et compteur à la même collection de DTO, puis retrouver les tests de recherche fournis. Les neuf tests E2 sont volontairement rouges avant E2-R; les tests E1 sont utilisables dès le départ. Aucun test supplémentaire n'est à écrire pour les exercices.

## 6. Disposition et clavier

Sans notes, distinguer Anchor et Dock, en précisant le parent direct. Reconstituer les cinq zones de Dock. Comparer TableLayoutPanel, FlowLayoutPanel, GroupBox, Panel et SplitContainer selon le besoin.

Préparer un essai Windows : Tab dans les conteneurs, touche d'accès, Entrée et AcceptButton, focus après refus et effacement, petite/grande fenêtre, échelle d'affichage. Les messages restent lisibles sans dépendre d'une couleur.

Preuve : réaliser une observation du formulaire réel. La compilation et les tests sans UI ne prouvent ni le designer ni l'ergonomie native.

## 7. Fenêtres et composition — transfert depuis la démonstration

Distinguer un message standard MessageBox, un dialogue de choix de fichier et un formulaire personnalisé. Dans MessageBox.Show, repérer propriétaire, texte, titre, boutons et icône. Prédire le retour de YesNo et OKCancel, puis expliquer pourquoi afficher une question ne suffit pas : l'appelant doit traiter le DialogResult. Quel bouton présélectionner pour éviter un abandon involontaire?

Comparer Show et ShowDialog(this), puis DialogResult.OK et Cancel. Une propriété Result de formulaire remplace-t-elle un Result<T> applicatif? Expliquer leur rôle et le moment où chacune est consultable.

Reconstruire le lancement manuel puis l'assemblage fourni par le cadriciel : Program, STAThread, Initialize, service injecté, Application.Run, AddScoped, Build, CreateScope et GetRequiredService. Associer IConfiguration, IOptions<T>, IServiceProvider et IServiceScopeFactory à leur rôle.

Dans la démonstration Commandes.Portee.WinForms, retracer la création puis la libération d'une portée pour chaque commande. Expliquer pourquoi une fabrique reçoit IServiceScopeFactory et pourquoi le formulaire reçoit ses collaborateurs. **Les exercices Contacts ne demandent ni fenêtre secondaire ni gestion de portée à programmer.**

Preuve : distinguer les sujets vus en démonstration du code à écrire dans le travail. Les tests unitaires construisent leurs sujets directement.

## 8. Mini-diagnostic sans notes

- « Je construis Contact et écris le JSON dans bCreer_Click. » Replacer les responsabilités.
- « Le service renvoie un échec, puis je consulte Valeur. » Diagnostiquer.
- « Rechercher trie puis sauvegarde la liste filtrée. » Comparer au contrat de consultation.
- « La recherche ne trouve rien : je garde les anciennes lignes. » Diagnostiquer l'affichage.
- « Un clic crée deux fois. » Vérifier une première cause avant le dépôt.
- « Mes tests passent, donc le designer et le clavier sont validés. » Préciser la preuve manquante.

## Lectures et suite

Notes : chapitre WinForms, sections formulaire, événements, disposition, messages, fenêtres et composition. La semaine 10 aborde MVC et Observer avec une solution autonome de facturation; aucun fichier Contacts n'est requis. Ne pas confondre un événement de bouton et l'explication complète du patron Observer.
