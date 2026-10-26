# Exercice 1

**Formatif — 60 minutes.** Construire une fiche de création de contact, puis la relier au cas d'utilisation déjà programmé. Cette interface sera conservée à l'Exercice 2.

## Départ et périmètre

Ouvrir la solution [S09E01E02_Contacts.slnx](S09E01E02_Contacts/S09E01E02_Contacts.slnx) du dossier `semaine-09/S09E01E02_Contacts`, sous Windows avec Visual Studio et .NET 10. Le formulaire initial est vide. Les projets domaine, Application, Infrastructure JSON, Terminal et Tests sont fournis. Le fichier `Program.cs` du projet WinForms fournit la configuration, l'injection et les deux assemblages; aucun de ces éléments n'est à programmer.

Modifier uniquement `FormContacts.cs` et construire les contrôles avec le designer du projet `S09E01E02_Contacts.WinForms`. Visual Studio mettra à jour `FormContacts.Designer.cs` et, au besoin, son fichier de ressources. Ne pas recopier de règles dans le formulaire, ne pas y instancier `Contact` et ne pas y écrire du JSON.

Créer depuis la branche Git `dev` une branche `fonctionnalite/contacts-winforms`, puis conserver cette branche pour E2. [Rappel Git](../semaine-03/README.md).

## 1. Observer le parcours fourni — 10 min

Utiliser directement **Visual Studio** :

1. Choisir **Générer la solution** dans le menu **Générer**.
2. Ouvrir l'**Explorateur de tests** dans le menu **Test**. Regrouper ou filtrer les tests par le trait `Etape`, puis exécuter le groupe `E1`.
3. Dans l'Explorateur de solutions, définir `S09E01E02_Contacts.Terminal` comme projet de démarrage, puis lancer avec **Démarrer sans débogage**.

Le terminal crée « Alex Roy », affiche le contact et le nombre de contacts. Un second lancement avec le même courriel produit un refus sans deuxième ajout. Le formulaire initial est vide, mais la création et la persistance fonctionnent déjà sans interface graphique.

Le fichier de données est relatif au dossier de l'exécutable. La console et WinForms ont donc par défaut leurs propres fichiers.

Repérer `DemandeCreationContact`, le cas d'utilisation `CreerContact.Executer(...)` et `Result<ContactDto>`. La demande transporte `Nom` et `Courriel`, même vides; le cas d'utilisation renvoie alors un refus attendu. Il refuse également un courriel déjà utilisé, sans distinction de casse. Aucune validation complexe d'adresse n'est demandée.

Les **25 tests `Etape=E1`** doivent réussir dès le départ. Les neuf cas de tests `Etape=E2` échouent volontairement avec `NotImplementedException` jusqu'à E2-R : ne pas programmer cette méthode maintenant.

## 2. Construire la fiche — 25 min

![Maquette de la fiche](maquettes/E01-fiche-contact.svg)

La maquette indique les zones et les noms; les dimensions exactes ne sont pas imposées.

| Contrôle | Name | Text ou usage |
|---|---|---|
| Label | `lNom` | `&Nom :` |
| TextBox | `tbNom` | Saisie du nom |
| Label | `lCourriel` | `C&ourriel :` |
| TextBox | `tbCourriel` | Saisie du courriel |
| Button | `bCreer` | `&Créer` |
| Button | `bEffacer` | `&Effacer` |
| Label | `lEtat` | Message lisible, sur plusieurs lignes si nécessaire |

Placer les contrôles **directement sur le formulaire** dans le designer, puis régler leur propriété `Anchor` :

| Contrôles | Anchor | Effet quand la fenêtre s'agrandit |
|---|---|---|
| `lNom`, `lCourriel`, `bCreer`, `bEffacer` | `Top, Left` | Garder leur position en haut à gauche |
| `tbNom`, `tbCourriel` | `Top, Left, Right` | Élargir les champs avec la fenêtre |
| `lEtat` | `Bottom, Left, Right` | Garder le message en bas et élargir sa zone |

Pour `lEtat`, désactiver `AutoSize` et prévoir assez de hauteur pour un message sur plusieurs lignes. Aucun conteneur de disposition n'est nécessaire pour cette fiche. Ordre de tabulation : nom, courriel, Créer, Effacer. Donner une taille minimale à la fenêtre. Utiliser `bCreer` comme `AcceptButton`.

La méthode `InitializeComponent()` construit les contrôles; le gestionnaire du clic se trouve dans l'autre partie de la classe. Laisser le designer gérer `FormContacts.Designer.cs`, notamment son champ `components`; ne pas le renommer avec le préfixe `m_`. Relier une seule fois `bCreer.Click` à `bCreer_Click` et `bEffacer.Click` à `bEffacer_Click`. Le chargement du formulaire est déjà relié; il n'a rien à faire en E1.

## 3. Transmettre la demande — 15 min

Compléter ces trois méthodes du fichier `FormContacts.cs` :

| Repère | Méthode | Travail |
|---|---|---|
| E1-C | `bCreer_Click` | Construire `DemandeCreationContact` depuis les deux champs; appeler `Creation.Executer(demande)`; vérifier `EstSucces` avant `Valeur`; afficher le nom créé ou `MessageErreur` |
| E1-E | `EffacerSaisie` | Vider les deux champs et placer le focus sur `tbNom` |
| E1-M | `AfficherMessage` | Affecter le texte à `lEtat.Text` |

Le bouton Effacer appelle déjà les deux dernières méthodes. Après un refus, garder la saisie et placer le focus sur le champ à corriger. Les blocs `catch` et la méthode `SignalerIncident` sont fournis : garder leur message sûr et leur journalisation. Ne pas afficher `exception.Message`.

Les repères E2 restent intacts. Après une création réussie en E1, afficher la confirmation; l'effacement automatique et la liste seront ajoutés en E2.

## 4. Vérifier — 10 min

Dans Visual Studio, générer à nouveau la solution et relancer le groupe de tests `E1`. Définir ensuite `S09E01E02_Contacts.WinForms` comme projet de démarrage et lancer le formulaire avec **Démarrer**. Le mode d'assemblage par défaut est déjà fourni; aucun argument de lancement n'est nécessaire.

Sans écrire de nouveaux tests ni de compte rendu :

- Créer un contact et observer le succès après l'enregistrement.
- Essayer un nom vide ou un courriel vide : refus, champs conservés.
- Réutiliser le même courriel en majuscules : refus, pas de deuxième contact.
- Utiliser Effacer, Tab, Entrée et les touches d'accès; vérifier le focus et agrandir la fenêtre.

Utiliser la même solution `S09E01E02_Contacts.slnx` pour les tests et l'interface. Le [protocole Windows](VERIFICATION_WINDOWS.md) fournit les observations détaillées pour E1 et E2.

**Fin observable :** une fiche fonctionnelle qui appelle le cas d'utilisation fourni et distingue succès, refus et incident. Conserver ce travail pour [l'Exercice 2](E02-gestion-contacts.md).

<details>
<summary>Si vous préférez la ligne de commande — facultatif</summary>

Depuis le dossier `semaine-09/S09E01E02_Contacts`, qui contient la solution `.slnx` :

```bash
dotnet build S09E01E02_Contacts.slnx
dotnet test S09E01E02_Contacts.slnx --filter "Etape=E1"
dotnet run --project S09E01E02_Contacts.WinForms
```

Ces commandes remplacent les manipulations dans Visual Studio; il n'est pas demandé de faire les deux.

</details>

<details>
<summary>Repères du code fourni : configuration, incidents et contrats</summary>

Les six projets sont fournis : domaine (entité `Contact`), Application (demande,
cas d'utilisation, DTO, `Result<T>` et abstractions), Infrastructure (JSON,
configuration et journal), Terminal, WinForms et Tests. Les références restent
à l'intérieur de cette solution. Le domaine ne connaît ni JSON ni WinForms;
les gestionnaires appellent Application et affichent ses DTO. Le choix du dépôt
est dans le fichier `Program.cs` du projet WinForms.

Le fichier `appsettings.json` est copié dans le dossier de chaque exécutable.
`Donnees:Chemin` et `Journal:Chemin` sont relatifs à `AppContext.BaseDirectory`.
Utiliser un dossier existant. Un fichier absent signifie une liste vide; un
dossier absent ou un JSON invalide constitue un incident, sans écrasement
silencieux. Pour observer les mêmes contacts depuis les deux présentations,
passer le même chemin absolu avec `--Donnees:Chemin` lors de leurs lancements;
le journal doit conserver un autre chemin.

Le mode WinForms `manuel` est le défaut; `cadriciel` est également fourni.
L'exercice peut être réalisé avec le mode par défaut. Aucune portée n'est à
programmer : celle fournie reste vivante pendant `Application.Run`.
Les incidents techniques peuvent remonter à la présentation; les `catch` fournis
journalisent l'opération et le type, puis affichent un message sûr. Les refus
attendus utilisent `Result<T>`. Ne pas afficher le message brut d'une exception.
Le dépôt pédagogique n'offre ni transaction ni coordination de plusieurs écrivains.

La classe `DemandeCreationContact` possède un constructeur explicite et des
propriétés en lecture seule. Elle transporte la saisie; le cas d'utilisation
reste responsable de son acceptation ou de son refus. Les gardes, conversions
et traitements techniques ci-dessous sont déjà programmés :

- `Contact` : identifiant non vide, nom/courriel non nuls et non blancs, espaces extérieurs retirés. Propriétés en lecture seule.
- `DemandeCreationContact` accepte des champs nuls/blancs pour transporter une saisie invalide; `CreerContact.Executer` refuse ces champs par `Result<T>`. Une demande nulle et des collaborateurs nuls sont des erreurs de précondition.
- `ContactDto` et `ContactDonnees(Contact)` refusent une entité nulle; la reconstruction par `VersContact` réapplique les invariants du domaine.
- Dépôt : chemin non nul/non blanc, ajout non nul, courriel exact non nul/non blanc. Le courriel est unique sans distinction de casse. L'ordre d'enregistrement est conservé.
- `Rechercher` refuse `null`, mais accepte vide/blanc comme « tous ». Sa garde et sa lecture sont déjà fournies.
- `Result<T>.Succes` refuse une valeur nulle; `Echec` exige un message; `Valeur` est inaccessible après un échec.
- Configuration : dépendance et dossier de base valides; données et journal distincts. Journal : chemin et opération non blancs, exception non nulle. Un incident du journal ne masque pas le message utilisateur.
- Le constructeur sans paramètres de `FormContacts` sert au designer; le lancement fournit des collaborateurs non nuls à l'autre constructeur. Les gestionnaires s'utilisent après ce lancement.

Les contrats du code fourni sont couverts par les tests E1 fournis. Les constructeurs et événements WinForms nécessitent les observations natives Windows. Au besoin : [aide Microsoft sur l’Explorateur de tests](https://learn.microsoft.com/fr-fr/visualstudio/test/run-unit-tests-with-test-explorer).

</details>
