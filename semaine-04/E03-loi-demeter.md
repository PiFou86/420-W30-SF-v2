# Exercice 3

## Mission et durée

En 60 minutes, retirez la chaîne d'appels utilisée pour obtenir l'adresse de
notification du client.

Dans le dépôt Git de l’exercice, créez la branche de fonctionnalité
`fonctionnalite/demeter` depuis la branche `dev`.

## Travail demandé

Le code de départ permet un appel semblable à
`commande.Client.Profil.Coordonnees.Courriel`. Ajoutez plutôt un message utile
sur l'objet connu directement par le service.

1. Dans le projet principal, ajoutez à la classe `Client` une méthode qui
   fournit le courriel à utiliser pour la notification.
2. Rendez les propriétés donnant accès aux objets `ProfilClient` et
   `Coordonnees` non publiques lorsqu’elles ne sont plus nécessaires à
   l’extérieur de leurs classes.
3. Modifiez la méthode `Creer` de la classe `ServiceCommandes` pour qu’elle
   envoie la notification en communiquant seulement avec ses collaborateurs
   directs, notamment l’objet `Client`.
4. Exécutez les deux tests d’interaction créés à l’exercice 1 : ils doivent
   encore passer sans être modifiés. Ils caractérisent le comportement externe
   de notification avant et après le réusinage.
5. Dans le projet de tests, ajoutez une seule méthode de test qui vérifie que
   `Client.ObtenirCourrielNotification()` retourne le courriel prévu, sans
   tester directement la structure interne de la classe `Client`.
6. Ajoutez dans `DECISIONS.md` la chaîne supprimée, le message qui la remplace
   et votre comparaison entre l’espion et Moq.

> [!TIP]
> **Tests déjà présents :** les deux tests d’interaction de l’exercice 1 et les
> deux tests de taxe et CQS de l’exercice 2. **Test à ajouter ici :** un test
> du message métier `Client.ObtenirCourrielNotification()`. Ne recréez pas les
> tests avec espion et Moq : leur succès démontre déjà que la notification est
> préservée.

> [!WARNING]
> N’utilisez pas Moq pour remplacer `Client`, `ProfilClient` ou `Coordonnees`.
> Ce sont de petits objets déterministes; construisez-les réellement.

<details>
<summary>Repère UML — à consulter après votre première tentative</summary>

Le diagramme se concentre sur le nouveau chemin de communication. Les classes
`ProfilClient` et `Coordonnees` existent toujours, mais elles ne sont plus
exposées comme chemin de navigation public.

```mermaid
classDiagram
    direction TB
    class ServiceCommandes
    class Client {
        - ProfilClient m_profil
        + ObtenirCourrielNotification() string
    }
    class ProfilClient {
        <<internal>>
        - Coordonnees m_coordonnees
        + ObtenirCourriel() string
    }
    class Coordonnees {
        <<internal>>
        + Courriel : string «get»
    }

    ServiceCommandes ..> Client : demande le courriel
    Client *-- "1" ProfilClient
    ProfilClient *-- "1" Coordonnees
```

**Question de lecture :** quel message remplace la chaîne de propriétés, et
quelle partie de la structure interne du client peut maintenant changer sans
modifier `ServiceCommandes`?

</details>

Dans le dépôt Git de l’exercice, fusionnez successivement la branche
`fonctionnalite/demeter` dans la branche `dev`, puis la branche `dev` dans la
branche `main`. Exécutez `dotnet test` sur chaque branche de destination après
la fusion.
