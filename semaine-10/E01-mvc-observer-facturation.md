# Exercice 1

## Mission

Faire travailler ensemble une fenêtre de commande et une fenêtre de facture avec
**les interfaces .NET `IObservable<EtatCommandeDto>` et
`IObserver<EtatCommandeDto>`**. Les modalités et l’échéance restent sur la
plateforme.

La structure MVC, les formulaires et les couches Application / domaine /
Infrastructure sont fournis. Vous complétez seulement cinq blocs de code et deux
tests guidés. Aucun calcul métier, dépôt, assemblage de services, document de
justification ou nouvel événement C# n’est à programmer.

Le contexte est une prise de commande autonome. Les montants reçus comprennent
des **frais de service fictifs de 10 %**, inventés pour l’exemple, sans règle
fiscale réelle. Soupe × 2 et Sandwich × 1 donnent 22,00 + 2,20 = **24,20**.

## Ouvrir et observer le départ

1. Ouvrir `S10E01_Facturation/S10E01_Facturation.slnx` dans Visual Studio sous
   Windows, avec .NET 10 et la charge de travail Développement Desktop .NET.
2. Utiliser **Générer la solution**, puis **Exécuter tous les tests** dans
   l’Explorateur de tests. Au départ, **69 tests fournis réussissent** et
   **les deux tests `ObserverAtelierTests` échouent volontairement** avec
   `NotImplementedException`. Aucun autre échec n’est attendu.
3. Choisir le projet `.WinForms` comme projet de démarrage et lancer.
   L’interface s’ouvre; les changements ne sont pas encore diffusés et la
   facture affiche un message d’attente. Cela correspond aux blocs manquants.
4. Créer `dev` depuis `main` si elle n’existe pas, puis votre branche
   `fonctionnalite/observer-dotnet` depuis `dev`.

La vue de commande appelle un contrôleur fourni; celui-ci demande une action
au modèle de présentation. Après un succès seulement, le modèle publie un
instantané contenant commande et facture. Les deux fenêtres observent la même
source. Vous devez retrouver ces rôles, sans les reconstruire.

## Fichiers à modifier

Tous les fichiers existent. Les repères `S10-...` indiquent les seuls blocs à
compléter. Les constructeurs, gardes, colonnes, messages sûrs et événements
WinForms sont déjà raccordés.

| Projet | Fichier et bloc | Travail attendu |
| --- | --- | --- |
| `.Presentation` | `ModeleCommande.cs` — `S10-S`, méthode `Subscribe` | Inscrire l’observateur dans la liste avant de retourner l’objet de désabonnement déjà fourni. |
| `.Presentation` | `ModeleCommande.cs` — `S10-N`, diffusion après succès | Appeler `OnNext(etat)` sur l’observateur courant. La boucle et les protections sont fournies. |
| `.WinForms` | `FormFacture.cs` — `S10-V`, méthode privée `AfficherEtat` | Afficher numéro, sous-total, frais, total et statut à partir de `etat.Facture`. |
| `.WinForms` | `FormFacture.cs` — `S10-A`, gestionnaire `Load` | Abonner cette fenêtre à son modèle et conserver le `IDisposable` retourné. |
| `.WinForms` | `FormFacture.cs` — `S10-D`, gestionnaire `Disposed` | Appeler la méthode privée `SeDesabonner()` déjà fournie. |
| `.Tests` | `ObserverAtelierTests.cs` — `S10-T1` et `S10-T2` | Remplacer les deux exceptions d’attente par les tests décrits ci-dessous. |

Dans `S10-V`, utiliser `lResumeFacture.Text` et les propriétés
`NumeroCommande`, `SousTotal`, `Frais`, `Total`, `EstProvisoire` du DTO de
facture. Présenter les montants avec deux décimales ou le format monétaire.
Le statut indique « Provisoire » avant l’enregistrement et « Commande
enregistrée » après sa réussite. Ne pas recalculer les frais.

Les contrôles et leur disposition par `Anchor` sont fournis : `tbNumero`,
`lbProduits`, `nudQuantite`, `bAjouter`, `bEnregistrer`, `bFacture`,
`dgvLignes`, `lEtat` et `lResumeFacture`. Aucun travail dans les `.Designer.cs`
ou `.resx` n’est demandé.

## Contrat de la source et des observateurs

- `Subscribe` reçoit un observateur non nul. L’inscription de la même instance
  une deuxième fois est refusée. Ces gardes sont fournies.
- Notre source ne transmet pas d’état initial pendant `Subscribe` : les
  appelants initialisent la vue séparément avec `Consulter`, ce qui est fourni.
- Une opération réussie transmet **le même instantané une fois à chaque
  observateur**. Un refus ou un incident récupérable du dépôt ne le transmet pas.
- `Dispose` sur un abonnement retire uniquement cet observateur. L’autre vue
  continue à recevoir les changements. La classe `AbonnementCommande`, fournie,
  tolère un second appel à `Dispose`.
- `AfficherEtat` contient le code d’affichage commun. `Load` l’appelle pour
  l’état initial; `OnNext` l’appelle pour chaque notification. Ces appels sont fournis.
- Le constructeur conserve le modèle; `Load` affiche l’état initial et abonne
  la fenêtre. `Disposed` termine l’abonnement. Créer un objet `FormFacture`
  sans le montrer ne l’abonne donc pas.
- La méthode privée `SeDesabonner()` appelle `Dispose`, puis libère la référence.
  Elle est fournie et peut être appelée plusieurs fois. Dans `S10-D`, l’appeler
  simplement à la fermeture; ne pas réécrire son code.
- `OnCompleted` et `OnError` sont fournis et appellent aussi `SeDesabonner()`.
  Ils décrivent la fin de la source;
  fermer une seule fenêtre ne doit appeler aucune de ces deux méthodes sur
  les autres vues. Un refus métier n’est pas une fin du flux.
  Se désabonner ne déclenche pas automatiquement `OnCompleted`.
- S’abonner dans le constructeur est une variante possible. Conserver le
  `IDisposable` permet de se désabonner, indépendamment du moment de l’inscription.
  Ici, `Load` et `Disposed` relient l’abonnement à l’ouverture et à la libération
  de la fenêtre.

Les comportements avancés sont déjà programmés : copie des abonnés, refus de
réentrée, isolation d’une réaction défaillante, journalisation, fin normale ou
fatale de la source. Le cas est synchrone et prévu pour un seul fil. Il n’est
pas demandé d’étudier ces protections en détail pour réaliser les blocs.

## Deux tests guidés à compléter

Les objets de départ, la fabrique du modèle et l’observateur espion manuel sont
fournis dans `ObserverAtelierTests.cs`. Construire directement le modèle dans
les tests, sans conteneur et sans ouvrir de fenêtre.

1. **`S10-T1`** : abonner les deux observateurs fournis, ajouter Soupe × 2 et
   vérifier une notification chacun, le même objet reçu (`Assert.Same`) et un
   total de facture de 14,30. Utiliser `using` pour les deux abonnements.
2. **`S10-T2`** : abonner les deux observateurs, terminer deux fois le premier
   abonnement, puis ajouter une ligne. Vérifier que le premier n’a rien reçu
   et que le second a reçu un état. Terminer aussi le second abonnement.

Après réalisation, **les 71 tests étudiants doivent réussir**. Il n’y a pas
d’autre test à écrire. Le corrigé enseignant contient des tests supplémentaires
des protections fournies.

## Vérification sous Windows et remise

Effectuer les six parcours du [protocole Windows](VERIFICATION_WINDOWS.md) :
ajout cohérent, refus, facture ouverte après une saisie, fermeture/réouverture,
enregistrement et incident conservant les lignes. Inscrire brièvement vos
observations dans la colonne prévue. Aucune capture supplémentaire n’est exigée.

Remettre la solution et ce protocole selon la plateforme. Vérifier compilation
et tests avant la fusion dans `dev`, puis sur `dev`. L’évaluation porte sur
l’utilisation des interfaces .NET, la diffusion, l’affichage du DTO, la durée
des abonnements et les deux tests, avec les couches fournies préservées.

<details>
<summary>Rappels et commandes facultatives</summary>

Revoir [WinForms en S9](../semaine-09/E02-gestion-contacts.md).
`IObservable<T>` est la source; `IObserver<T>` reçoit `OnNext`.
Qui doit conserver le `IDisposable` lorsque la facture est ouverte?

Depuis `semaine-10/S10E01_Facturation` :

```bash
dotnet build S10E01_Facturation.slnx
dotnet test S10E01_Facturation.slnx
dotnet run --project S10E01_Facturation.WinForms
```

</details>
