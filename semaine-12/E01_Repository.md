# Exercice 1

**Activité intégrée au TP02 · environ 40 min, incluses dans son budget**

Ouvrir la solution de son lot TP02 dans Visual Studio. Générer et lancer les tests pour conserver un point de comparaison.

Dans le projet Application, extraire le contrat générique `IRepository<T> where T : class` décrit dans `CONTRATS.md` du TP. Faire hériter le port du lot de ce contrat; conserver la recherche spécifique sur ce port. Adapter les dépôts et les appels concernés sans modifier les comportements.

Depuis le projet de tests, créer une doublure manuelle utilisée à travers l’abstraction. Montrer séparément la valeur renvoyée et l’appel observé. Construire directement le sujet testé.

La solution, les tests du départ et le Terminal doivent toujours fonctionner. Les numéros affichés restent 101 pour Commandes, 1/2/3 pour Menu et 301 pour Réservations. Consigner le commit et la preuve dans le journal du lot; ils serviront à la première PR.

La version autonome de cette activité reste [facultative](pratique_facultative/E01_Repository.md).
