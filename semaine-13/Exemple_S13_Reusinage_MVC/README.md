# Démonstration S13 — du formulaire aux services et à MVC

Trois exécutables montrent la même consultation de commande. Le départ fonctionne, mais mélange fichier, recherche, calcul et format monétaire dans le formulaire. La transformation conserve le comportement observable.

## Les trois états

| État | Code à montrer | Répartition |
|---|---|---|
| `01_Depart` | `Consultation/ConsultationInitiale.cs`, puis le bouton dans `FormCommande.cs` | Tout est dans la même classe de formulaire. |
| `02_Services` | `ServiceCommandes`, `Commande`, `IDepotCommandes`, `DepotCommandesJson`, puis le formulaire | Les règles et la persistance ont quitté le formulaire. Il interprète encore le résultat du service. |
| `03_MVC` | `ModeleConsultation`, `ControleurCommande`, puis le formulaire | MVC organise la présentation en s'appuyant sur les mêmes services. |

### Quel fichier ouvrir dans Visual Studio ?

Dans chacun des trois projets, ouvrir le concepteur par clic droit sur **FormCommande.cs → Afficher le concepteur**. Choisir **Afficher le code** pour lire le gestionnaire du bouton. Chaque projet possède sa propre fenêtre nommée `FormCommande` pour permettre de comparer trois versions exécutables de la même interaction.

Dans `01_Depart`, développer `FormCommande.cs` dans l’Explorateur de solutions. Sous ce formulaire figurent le code du concepteur et son fichier de ressources. La méthode à réusiner se trouve dans le dossier `Consultation` du projet.

| Fichier du départ | Ce qu’on y montre |
|---|---|
| `FormCommande.cs` | Le constructeur appelle `InitializeComponent()`. Le bouton lit le numéro et appelle `ConsulterTotal`. |
| `FormCommande.Designer.cs` | Le concepteur crée les contrôles et abonne le bouton à `bConsulter_Click`. |
| `FormCommande.resx` | Ressources du formulaire, gérées par le concepteur. |
| `Consultation/ConsultationInitiale.cs` | La suite du code de `FormCommande`. Elle contient la méthode à réusiner et s’ouvre comme du code. Le projet de tests reprend ce fichier pour exercer l’algorithme initial. |

Chaque étape possède son propre fichier designer et copie les mêmes données JSON dans son dossier de sortie. `01_Depart` est autonome. `02_Services` et `03_MVC` utilisent les mêmes projets Application, domaine et Infrastructure.

## Architecture finale

- `Commandes.Domaine` : `Commande` et `LigneCommande`; calcul du total.
- `Commandes.Application` : `ServiceCommandes`, `Result<T>` et `IDepotCommandes`; orchestration de la consultation.
- `Commandes.Infrastructure` : lecture JSON et conversion vers les objets du domaine.
- `Commandes.Presentation` : contrôleur et modèle observable de présentation, sans dépendance à WinForms.
- `03_MVC` : vue WinForms et assemblage des objets dans `Program.cs`.

Le contrôleur transmet la demande à `ModeleConsultation`. Le modèle implante `IObservable<ResultatConsultation>`. Le formulaire implante `IObserver<ResultatConsultation>`. Le programme l’abonne avec `Subscribe` et libère l’abonnement au retour de `Application.Run`. Le modèle publie le total ou une raison d’échec par `OnNext`. Une commande absente ou un incident de lecture laisse l’abonnement actif pour la prochaine consultation.

## Conduire la démonstration en 55 minutes

**Avant la classe.** Extraire l’archive dans un dossier de travail sous Windows. Ouvrir `Demonstration.slnx` dans Visual Studio avec le SDK .NET 10 et la charge de travail de développement desktop .NET. Générer la solution et exécuter les tests une première fois. Ouvrir le diaporama à côté de l’IDE. Pour changer de version, arrêter le débogage, choisir le projet indiqué comme projet de démarrage, puis relancer. Les trois fenêtres se ressemblent volontairement.

| Temps | Diapos | Écran et action |
|---|---|---|
| 0–3 min | 1 à 3 | Projeter le parcours. Poser la question « Comment saurons-nous que le réusinage a préservé la consultation ? » |
| 3–10 min | 4 et 5 | Lancer le départ, recueillir les trois observations, suivre le clic dans le code, puis revenir au tableau des responsabilités. |
| 10–20 min | 6 | Extraire le calcul en direct dans le départ. Rejouer une consultation, puis comparer avec la méthode placée dans le domaine. |
| 20–32 min | 7 | Montrer l’extraction d’interface en aperçu, suivre le contrat jusqu’au service, puis exécuter l’état Services. |
| 32–45 min | 8 à 11 | Lancer MVC. Suivre un clic au débogueur, puis utiliser le schéma pour expliquer les dépendances. |
| 45–55 min | 12 à 14 | Comparer les observations, lancer les tests, faire formuler la synthèse et annoncer la suite. |

### 1. Observer le comportement avant de projeter la solution · diapos 1 à 5

Après les objectifs, lancer `01_Depart`. Saisir successivement **1**, **2** et **99**, puis cliquer chaque fois sur **Consulter le total**. Inscrire au tableau **30,00 $**, **0,00 $** et **« Commande introuvable. »**. Faire expliquer la différence entre une commande existante sans lignes et un numéro absent.

À la diapo 4, ouvrir le code du bouton dans `01_Depart/FormCommande.cs`, puis utiliser **Atteindre la définition** sur `ConsulterTotal`. Faire remarquer que la navigation change de fichier tout en restant dans la même classe. Demander « Quelles lignes changeraient si les commandes venaient d’une autre source ? Et si seul le format monétaire changeait ? » Laisser repérer la lecture JSON et le `ToString("C2")` avant de révéler les destinations sur la diapo 5.

### 2. Faire une transformation en direct · diapo 6

Arrêter le programme. Dans `01_Depart/Consultation/ConsultationInitiale.cs`, sélectionner **de `decimal total = 0m;` à l’accolade fermante du `foreach` incluse**. Laisser `return total.ToString("C2");` hors sélection. Ouvrir les actions rapides (**Ctrl + .**), choisir **Extraire la méthode**, puis la nommer `CalculerTotal`. Lire l’appel généré et vérifier que la valeur de type `decimal` est disponible pour le formatage. Générer et consulter de nouveau la commande 1.

Demander « À quelle classe appartient le calcul maintenant ? » Il appartient encore à `FormCommande`. Ouvrir ensuite la version préparée dans `Commandes.Domaine/Commande.cs`. Comparer les lignes reçues du fichier au départ avec les lignes que possède l’objet `Commande`. Montrer comment `DepotCommandesJson.Obtenir` transforme les données du fichier en objets du domaine. **La décision d’attribuer le calcul à la commande est le geste architectural.**

### 3. Montrer le passage au service · diapo 7

1. Ouvrir `Commandes.Infrastructure/DepotCommandesJson.cs`. Avant de projeter la diapo, demander « De quelle opération le service a-t-il besoin pour consulter une commande ? »
2. Sur le nom de la classe, ouvrir **Ctrl + . → Extraire l’interface**. Montrer le membre `Obtenir(int identifiant)` et son retour `Commande?`. **Annuler** à ce point. L’état Services livré contient déjà l’interface. Ouvrir `Commandes.Application/IDepotCommandes.cs` pour montrer le contrat effectivement utilisé.
3. Suivre `ServiceCommandes.ConsulterTotal`. Le constructeur reçoit le dépôt par son interface, le service demande la commande et renvoie un refus si elle est absente. Sinon, il appelle `commande.CalculerTotal()`. Montrer les références de projets et retrouver **Infrastructure → Application**.
4. Lancer `02_Services`, essayer 1, 2 et 99. Ouvrir son `FormCommande.cs`. Demander « Qui décide encore d’afficher un total ou un refus ? » Suivre le `if (!resultat.EstSucces)` dans le bouton. C’est le point de départ de MVC.

### 4. Suivre un clic en MVC · diapos 8 à 11

À la diapo 8, partir du bouton de `02_Services`, puis montrer l’abonnement de la vue dans `03_MVC`. À la diapo 9, poser un point d’arrêt sur `observateur.OnNext(resultat)` dans `ModeleConsultation`. Consulter **1**, puis **99**. À la diapo 10, suivre la notification jusqu’à `FormCommande.OnNext` dans le formulaire et montrer le formatage ainsi que le désabonnement à la fermeture de la fenêtre. À la diapo 11, revenir à `03_MVC/Program.cs` et au schéma des dépendances.

### 5. Conclure et passer aux exercices · diapos 12 à 14

À la diapo 12, reprendre les trois observations du tableau. Dans l’Explorateur de tests, lancer tous les tests. Ouvrir le test `Consulter_JsonInvalide_ConserveLIncidentSansAfficherUnTotal` et demander quel message voit la personne si le fichier est illisible. La vue affiche **« Consultation impossible pour le moment. »**. Comparer avec le refus obtenu pour 99. Le test sur le fichier compare ses octets avant et après les appels. Pour vérifier les opérations effectuées, montrer aussi `File.ReadAllText` dans le dépôt.

À la diapo 13, faire compléter oralement « Le bouton transmet…, le contrôleur transmet…, le modèle publie…, le service orchestre…, la commande calcule… ». Utiliser la diapo 14 pour annoncer le prochain cours. Pour le bloc pratique de S13, ouvrir ensuite la solution de prêts du dépôt étudiant. Prévoir **40 min pour E01** (déplacer le calcul), **65 min pour E02** (extraire le contrat et injecter le service) et **15 min de marge**, soit **2 h** en plus de la démonstration.

Si le temps manque, passer de l’extraction de méthode directement aux états Services et MVC déjà compilés. Conserver le passage de **1 à 99** au débogueur pour que la notification du modèle reste visible.

## Lancer sous Windows

Extraire le ZIP dans un nouveau dossier local accessible en écriture. Avec le SDK .NET 10 et le développement desktop .NET de Visual Studio, ouvrir `Demonstration.slnx` et choisir l'un des trois exécutables comme projet de démarrage. En ligne de commande, depuis ce dossier :

```powershell
dotnet run --project 01_Depart/01_Depart.csproj
dotnet run --project 02_Services/02_Services.csproj
dotnet run --project 03_MVC/03_MVC.csproj
```

Les programmes fixent la culture à `fr-CA`. La commande 1 donne 30,00 en format monétaire, la commande 2 donne zéro et le numéro 99 affiche « Commande introuvable. ». Aucune consultation n'écrit dans le fichier.

Un fichier absent ou un JSON invalide donne « Consultation impossible pour le moment. ». Les détails techniques passent par `Trace.TraceError`, jamais dans le libellé utilisateur. Pour vérifier ces incidents sous Windows, modifier temporairement une **copie de sortie** de `commandes.json`, puis la restaurer avant la comparaison suivante.

Le périmètre utilise des fichiers respectant la structure fournie, avec identifiants, lignes, prix et quantités présents. Aucune nouvelle règle de validation, taxe ou remise n'est ajoutée pendant ce réusinage.

## Vérification automatisée

```bash
dotnet build Demonstration.slnx
dotnet test Demonstration.Portable.slnx
```

`Commandes.Tests` reprend directement le fichier de calcul/lecture du départ, sans la partie Windows du formulaire. La comparaison teste donc son vrai algorithme et le chemin final du contrôleur au service et du modèle à la vue espionne. Elle ne simule pas les contrôles WinForms.

Résultats obtenus :

- Compilation des huit projets, dont les trois exécutables WinForms : **0 avertissement, 0 erreur**.
- **11 tests réussis** : totaux, absence, cultures `fr-CA` et `en-CA`, lecture sans écriture, incidents JSON/fichier, substitution du dépôt, notification unique et désabonnement.
- Compilation croisée effectuée sur macOS. L'ouverture du designer, le lancement des fenêtres et les clics réels restent à vérifier sous Windows.
