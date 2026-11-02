# Démonstration S10 — Observer .NET

Ouvrir Demonstrations_S10.slnx dans Visual Studio sous Windows (.NET 10).
Choisir Demo.Terminal.DotNet ou Demo.WinForms comme projet de démarrage.
Le guide ../Preparation_S10_MVC_Observer.md donne les points d’arrêt.

Les deux exécutables réutilisent Demo.Presentation.DotNet et les trois projets
Commandes.Domaine, Commandes.Application et Commandes.Infrastructure.
La solution est autonome, sans référence vers le départ étudiant incomplet.
Elle contient sept projets et une seule implantation d’Observer, avec les
interfaces System.IObservable<EtatCommandeDto> et System.IObserver<EtatCommandeDto>.

Subscribe retourne un AbonnementCommande qui implante IDisposable.
OnNext transmet un instantané; OnCompleted et OnError annoncent la fin de
la source. Dispose d’un abonnement retire seulement l’observateur concerné.
Load initialise la facture avec AfficherEtat, puis l’abonne. OnNext appelle
la même méthode privée AfficherEtat lors des notifications.
Sa méthode privée SeDesabonner est appelée
dans Disposed, OnCompleted et OnError. Le contrôleur possède l’abonnement
principal; la console conserve ses using.

Le terminal propose --erreur-fatale pour montrer OnError et le message sûr.
Sans argument, il montre la fin normale par OnCompleted.
Aucun bloc incomplet ni NotImplementedException dans la démonstration.

Les positions des contrôles réajustées par l’enseignant sont conservées.
Les designers gardent components, les champs simples, les Anchor et les
métadonnées UserAddedColumn=True des trois colonnes. Les conventions du cours
s’appliquent au code manuel; Visual Studio garde celles des fichiers générés.

Validation : sept projets, compilation complète sans avertissement et
104 tests réussis. Le terminal a été exécuté avec fin normale et fin en erreur.
Les formulaires et designers restent à vérifier nativement sous Windows.
La solution Portable sert uniquement à la validation enseignante.

Le bouton Nouvelle attribue automatiquement un numéro disponible. Le numéro
est affiché en lecture seule et repris par la facture. ServiceCommandes
recherche le premier numéro libre, puis avance pour les créations suivantes
de la session. Le dépôt mémoire ou JSON est consulté avant chaque attribution;
la commande n’est persistée qu’au clic Enregistrer. Au démarrage WinForms,
une première commande reçoit aussi un numéro disponible, dans les deux modes
d’assemblage. Les positions des contrôles ne sont pas modifiées.
