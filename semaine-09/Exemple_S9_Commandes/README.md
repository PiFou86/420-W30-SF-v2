# Exemple de préparation S9 — prise de commande WinForms

Source de préparation de cours et d'extraits pour les diapos, comme l'exemple de
semaine 8. Le parcours complet ajoute la **création explicite de la commande**
aux extraits sur l'ajout d'une ligne. Démarrer par le
[guide de démonstration](../Preparation_S9_WinForms.md).

Prérequis : SDK .NET 10, C# 14. L'interface s'exécute sous Windows.

## Premier exécutable : une fenêtre de commande

Depuis ce dossier, ouvrir `Commandes.slnx` dans Visual Studio et sélectionner
`Commandes.WinForms`, ou utiliser :

```sh
dotnet build Commandes.slnx
dotnet run --project Commandes.WinForms
```

L'assemblage manuel est le choix initial. Pour comparer avec le conteneur :

```sh
dotnet run --project Commandes.WinForms -- --assemblage=cadriciel
```

L'application démarre sans commande. **Créer une nouvelle commande** ouvre une
fenêtre de saisie : `new` crée l'objet, `Load` prépare sa vue et `Shown` place le
focus. Après validation, le formulaire appelant lit sa propriété `Result` et
appelle le service Application. Annuler ne crée rien.

Choisir **Sandwich**, saisir **2** dans `nudQuantite` puis **Ajouter** : total
**16,00 $**. Enregistrer, puis créer une nouvelle commande. Les lignes non
enregistrées demandent une confirmation avant leur abandon ou la fermeture.

## Deuxième exécutable : une portée par commande

```sh
dotnet run --project Commandes.Portee.WinForms
```

La fenêtre d'accueil affiche seulement les commandes enregistrées : numéro,
nombre de lignes et total. **Nouvelle commande** ouvre la saisie du numéro,
puis l'éditeur partagé. Les dialogues sont modaux et utilisent une même portée.
Après enregistrement, l'éditeur se ferme, la portée est libérée et la liste est
actualisée. **Actualiser** permet aussi de relire le dépôt manuellement.

Chaque clic crée une nouvelle portée et un nouveau `ServiceCommandes`, sans
brouillon automatique. Le dépôt mémoire, le catalogue et le journal restent
partagés. Le titre du dialogue affiche l'identifiant d'un témoin Scoped; en configuration
**Debug**, la sortie **Débogage** de Visual Studio trace sa création et sa libération.

Le formulaire principal reçoit un service de consultation et une fabrique de
saisie. Seule cette fabrique reçoit `IServiceScopeFactory` et résout les services;
les formulaires ne connaissent pas le conteneur. Le DTO de résultat est lu avant
la libération et reste exploitable après celle-ci.

Les deux exécutables et le terminal partagent les projets domaine, Application
et Infrastructure. Les deux exécutables Windows partagent aussi
`Commandes.Presentation.WinForms`; aucun fichier de formulaire n'est dupliqué.
Le second exécutable utilise toujours le conteneur et n'a pas d'option `assemblage`.
Les dépôts mémoire des **deux processus distincts** ne communiquent pas : le partage
montré concerne les portées d'un même lancement.

## Persistance JSON facultative

```sh
dotnet run --project Commandes.WinForms -- --Donnees:Format=json --Donnees:Chemin=commandes.json
```

Par défaut, le dépôt mémoire conserve les commandes pour la durée du lancement.
Avec JSON, elles sont relues au lancement suivant. Les chemins relatifs partent
du dossier de l'exécutable (`Commandes.WinForms/bin/Debug/net10.0-windows/`), où
`appsettings.json` est copié. Les arguments remplacent la configuration du fichier.
Pour le deuxième exécutable :

```sh
dotnet run --project Commandes.Portee.WinForms -- --Donnees:Format=json --Donnees:Chemin=commandes.json
```

Son dossier de base est `Commandes.Portee.WinForms/bin/Debug/net10.0-windows/`.
Les deux exécutables utilisent la même classe `ConfigurationCommandes` pour
les formats, les chemins et la séparation entre données et journal.
Les données et le journal ont des fichiers distincts.

Un numéro proposé est un point de départ pour la saisie, sans garantie d'être
libre dans un ancien fichier. Un doublon donne un refus et préserve la commande
courante. Une panne de lecture ou d'écriture laisse le brouillon disponible,
affiche un message sûr et consigne la catégorie de l'incident. L'enregistrement
confirme une copie puis remplace le brouillon après réussite du dépôt.

Ce dépôt pédagogique ne fournit ni transaction ni protection contre des écritures
concurrentes. Les mises à jour du fichier ne sont pas garanties atomiques.

## Retrouver les extraits

| Fichier | Rôle |
| --- | --- |
| `Commandes.Presentation.WinForms/FormNouvelleCommande.cs` | Objet fenêtre, constructeur, Load, Shown et propriété Result |
| `Commandes.Presentation.WinForms/SaisieNouvelleCommande.cs` | Données validées par la fenêtre, sans contrôles exposés |
| `Commandes.Presentation.WinForms/FormCommande.cs` | Création, ajout, enregistrement, affichage et fermeture |
| `Commandes.Presentation.WinForms/*.Designer.cs` | Contrôles, préfixes, dispositions et abonnements |
| `Commandes.WinForms/Program.cs` | Composition manuelle puis conteneur, configuration et journal |
| `Commandes.Portee.WinForms/Program.cs` | Composition du second exemple : dépôt partagé et service Scoped |
| `Commandes.Portee.WinForms/FormAccueil.cs` | Liste des commandes enregistrées et déclenchement de la saisie |
| `Commandes.Portee.WinForms/FabriqueSaisieCommande.cs` | CreateScope, deux dialogues et libération au retour |
| `Commandes.Infrastructure/ServicesSessionCommandes.cs` | Inscriptions communes du second exemple, testées en intégration |
| `Commandes.Infrastructure/TemoinPortee.cs` | Identifiant de démonstration et traces de libération |
| `Commandes.Application/ServiceConsultationCommandes.cs` | Consultation des commandes enregistrées |
| `Commandes.Application/ServiceCommandes.cs` | Cas d'utilisation et cycle de la commande |
| `Commandes.Application/CommandeDto.cs` | Instantané stable de sortie |
| `Commandes.Domaine/` | Entités, objets immuables et invariants |
| `Commandes.Infrastructure/` | Catalogue, dépôts mémoire/JSON et journal |
| `Commandes.Terminal/Program.cs` | Même cycle sans interface graphique |

Les numéros des diapositives restent inchangés. Le dialogue de création et le
cycle de vie sont démontrés à partir de ce code de préparation. Les sources des
couches communes ont été adaptées du matériel S9 existant; cet exemple est
ensuite autonome. Il ne référence aucun projet du départ étudiant ou du corrigé.

## Vérifier

```sh
dotnet build Commandes.slnx
dotnet test Commandes.slnx
dotnet run --project Commandes.Terminal
```

Ouvrir uniquement `Commandes.slnx` sous Windows : cette solution contient les
couches partagées, le terminal, les tests et les deux exécutables WinForms.
La même solution sert à compiler, tester et lancer les interfaces. Le scénario terminal affiche successivement : aucune commande, création
1042 vide, ajout d'une ligne à 16,00, enregistrement, création 1043 vide.

Les 31 cas de tests passent, dont les 17 cas d’origine conservés. Les tests ajoutés couvrent la consultation,
les copies, le tri, la relecture JSON, la configuration commune et les portées
avec les mêmes inscriptions que le deuxième exécutable. Ils ne valident pas les
événements ni l’apparence des fenêtres. Utiliser le protocole
[VERIFICATION_WINDOWS.md](VERIFICATION_WINDOWS.md) pour cette partie.

## Choix de syntaxe pour la classe

Les objets de saisie, DTO, ViewModel et objets de persistance sont des classes
avec constructeur explicite et propriétés. Le remplacement des anciens records
n'ajoute pas de comparaison par valeur; le parcours n'en utilise aucune.
La lecture et l'écriture JSON conservent les mêmes noms de propriétés.
La fabrique reçoit un `Form proprietaire` et appelle `ShowDialog(proprietaire)`.
`ServicesSessionCommandes.AjouterSessionsCommandes(...)` est une méthode statique
ordinaire, commune au lancement et aux tests d'intégration.

Les `new` nomment la classe construite, les décisions sont développées et les
incidents sont capturés par types. `Result<T>`, la nullabilité, les `using`,
l'injection et les portées restent visibles : ils font partie du parcours.
Les propriétés en lecture seule rendent l'immutabilité visible sans record.

Les `.Designer.cs` gardent `components` et la structure des méthodes générées.
Le champ reste nul tant qu'aucun composant non visuel ne nécessite de conteneur;
`Dispose` le libère lorsqu'il existe. Les colonnes des grilles sont déclarées
explicitement. La culture monétaire est affectée dans `Load`, hors du designer.
Le premier enregistrement dans Visual Studio peut normaliser le fichier; la
préservation des contrôles et événements se vérifie ensuite sous Windows.


## Disposition simple avec Anchor

Les trois fenêtres placent les contrôles directement sur le formulaire.
`FormCommande` et `FormAccueil` ancrent leurs grilles aux quatre bords; les
messages restent en bas et les boutons conservent leur position ou leur marge.
Le dialogue de numéro reste de taille fixe avec ses boutons en bas à droite.
Aucun conteneur de disposition n'est nécessaire pour cette démonstration.
Le conteneur de dépendances et les portées conservent leur rôle distinct.
