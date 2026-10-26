# S09E01E02_Contacts — départ cumulatif

C# 14 / .NET 10. Utiliser ce même dossier pour les [Exercices 1](../E01-fiche-contact.md) et [2](../E02-gestion-contacts.md). Le formulaire de départ est vide mais la solution compile.

## Ce qui est fourni

| Projet | Responsabilité / état |
|---|---|
| `.Domaine` | Entité `Contact` immuable, identifiant et invariants fournis |
| `.Application` | `DemandeCreationContact`, `CreerContact`, `ConsulterContacts`, DTO, `Result<T>` et abstractions fournis |
| `.Infrastructure` | Dépôt JSON, conversions, lecture validée, configuration et journal fournis; seul bloc `E2-R` à écrire en E2 |
| `.Terminal` | Démonstration de création et consultation sans UI, fournie |
| `.WinForms` | Lancement fourni; formulaire vide et gestionnaires balisés à compléter |
| `.Tests` | Tests fournis, sujets construits directement, aucun nouveau test demandé |

Les références de projets restent à l'intérieur de la solution. Le domaine ne connaît ni JSON ni WinForms. Les gestionnaires appellent Application et affichent ses DTO; le choix du dépôt est dans `Program.cs`.

## Lancement et tests

Depuis **ce dossier**, où se trouvent les fichiers `.slnx` :

```bash
dotnet build S09E01E02_Contacts.slnx
dotnet test S09E01E02_Contacts.Portable.slnx --filter "Etape=E1"
dotnet run --project S09E01E02_Contacts.Terminal
```

Le terminal crée « Alex Roy », affiche la liste et le nombre de contacts. Le relancer avec le même courriel donne un refus sans deuxième ajout. Pour un autre contact :

```bash
dotnet run --project S09E01E02_Contacts.Terminal -- --Scenario ajouter --Contact:Nom "Mia Roy" --Contact:Courriel "mia@example.test"
dotnet run --project S09E01E02_Contacts.Terminal -- --Scenario lister
```

Après E2-R :

```bash
dotnet test S09E01E02_Contacts.Portable.slnx --filter "Etape=E2"
dotnet test S09E01E02_Contacts.Portable.slnx
dotnet run --project S09E01E02_Contacts.Terminal -- --Scenario rechercher --MotClef "ROY"
```

**État initial et fin E1 :** les 25 tests E1 sont verts; les neuf cas E2 échouent uniquement avec `NotImplementedException` dans `DepotContactsJson.Rechercher`. Un lancement du scénario `rechercher` est donc réservé à E2. Aucun autre échec n'est attendu. **Fin E2 :** les 34 tests doivent être verts.

Sous Windows :

```bash
dotnet run --project S09E01E02_Contacts.WinForms
dotnet run --project S09E01E02_Contacts.WinForms -- --assemblage cadriciel
```

La solution portable exclut le projet WinForms et permet les tests sous macOS/Linux. La solution complète peut se compiler par ciblage Windows; cela ne valide pas la fenêtre ni son designer. [Protocole natif](../VERIFICATION_WINDOWS.md).

## Configuration et incidents

`appsettings.json` est copié dans chaque dossier d'exécutable. `Donnees:Chemin` et `Journal:Chemin` sont relatifs à `AppContext.BaseDirectory`. Les deux exécutables ont donc des fichiers distincts par défaut. Pour partager des données lors d'une démonstration, passer le même chemin absolu avec `--Donnees:Chemin`; le journal doit garder un autre chemin. Utiliser un dossier existant. Le fichier absent signifie une liste vide; un dossier absent ou un JSON invalide est un incident, sans écrasement silencieux.

Le mode WinForms `manuel` est le défaut; `cadriciel` est déjà fourni. Aucune portée à programmer. La portée fournie reste vivante pendant `Application.Run`.

Les cas d'utilisation peuvent propager un incident technique au point de présentation; les blocs `catch` fournis journalisent opération/type et affichent un message sûr. Les refus attendus sont des `Result<T>` en échec. Ne pas afficher le message brut de l'exception. Le dépôt pédagogique n'offre ni transaction ni coordination de plusieurs écrivains.

## Frontières fournies

- `Contact` : identifiant non vide, nom/courriel non nuls et non blancs, espaces extérieurs retirés. Propriétés en lecture seule.
- `DemandeCreationContact` accepte des champs nuls/blancs pour transporter une saisie invalide; `CreerContact.Executer` refuse ces champs par `Result<T>`. Une demande nulle et des collaborateurs nuls sont des erreurs de précondition.
- `ContactDto` et `ContactDonnees(Contact)` refusent une entité nulle; la reconstruction par `VersContact` réapplique les invariants du domaine.
- Dépôt : chemin non nul/non blanc, ajout non nul, courriel exact non nul/non blanc. Le courriel est unique sans distinction de casse. L'ordre d'enregistrement est conservé.
- `Rechercher` refuse `null`, mais accepte vide/blanc comme « tous ». Sa garde et sa lecture sont déjà fournies.
- `Result<T>.Succes` refuse une valeur nulle; `Echec` exige un message; `Valeur` est inaccessible après un échec.
- Configuration : dépendance et dossier de base valides; données et journal distincts. Journal : chemin et opération non blancs, exception non nulle. Un incident du journal ne masque pas le message utilisateur.
- Le constructeur sans paramètres de `FormContacts` sert au designer; le lancement fournit des collaborateurs non nuls à l'autre constructeur. Les gestionnaires s'utilisent après ce lancement.

Les frontières portables sont couvertes par les tests E1 fournis. Les constructeurs et événements WinForms nécessitent les observations natives Windows.

## Lisibilité et designer

`DemandeCreationContact` est une classe ordinaire avec constructeur explicite et
propriétés en lecture seule. Elle accepte toujours une saisie invalide; le cas
d'utilisation décide du succès ou du refus. Les types sont écrits après `new`,
les décisions sont développées et les blocs `catch` fournis capturent les types
d'incidents avant d'appeler le même signalement sûr. Les repères et les travaux
à compléter sont inchangés : notamment `E2-R` avant sa réalisation.

Les fichiers `.Designer.cs` conservent le champ `components` et les conventions
de Visual Studio. Le code manuel utilise les conventions du cours. Construire
l'interface avec le designer; garder les noms `tbNom`, `tbCourriel`, `bCreer`,
`bEffacer`, `lEtat`, puis les contrôles d'E2. La compilation ne remplace pas la
vérification Windows d'un enregistrement et d'une réouverture du formulaire.
