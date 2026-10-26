using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;

namespace Commandes.Infrastructure;

public sealed class DepotCommandesJson : IDepotCommandes
{
    private readonly string m_chemin;

    public DepotCommandesJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = chemin;
    }

    public void Ajouter(Commande commande)
    {
        ArgumentNullException.ThrowIfNull(commande);
        List<Commande> commandes = Lire();
        if (commandes.Any(item => item.Numero == commande.Numero))
        {
            throw new InvalidOperationException("Numéro déjà enregistré.");
        }

        commandes.Add(commande.Copier());
        File.WriteAllText(m_chemin, JsonSerializer.Serialize(commandes.Select(VersDonnees).ToArray()));
    }

    public IReadOnlyCollection<Commande> ObtenirToutes()
    {
        // Lire reconstruit déjà des objets indépendants à chaque appel.
        return Lire().OrderBy(commande => commande.Numero).ToArray();
    }

    public Commande? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        return Lire().SingleOrDefault(item => item.Numero == numero);
    }

    private List<Commande> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin);
        }
        catch (FileNotFoundException)
        {
            return new List<Commande>();
        }

        List<CommandeDonnees?>? donnees = JsonSerializer.Deserialize<List<CommandeDonnees?>>(json);
        if (donnees == null)
        {
            throw new InvalidDataException("Liste attendue.");
        }

        List<Commande> commandes = new List<Commande>();
        HashSet<int> numeros = new HashSet<int>();
        foreach (CommandeDonnees? item in donnees)
        {
            try
            {
                if (item == null || item.Lignes == null || !numeros.Add(item.Numero))
                {
                    throw new InvalidDataException("Entrée invalide ou dupliquée.");
                }

                Commande commande = new Commande(item.Numero);
                foreach (LigneDonnees? ligne in item.Lignes)
                {
                    if (ligne == null)
                    {
                        throw new InvalidDataException("Ligne nulle.");
                    }

                    ArgumentException.ThrowIfNullOrWhiteSpace(ligne.Nom);
                    Produit produit = new Produit(ligne.NumeroProduit, ligne.Nom, ligne.Prix);
                    commande.Ajouter(produit, ligne.Quantite);
                }

                if (item.EstConfirmee)
                {
                    commande.Confirmer();
                }

                commandes.Add(commande);
            }
            catch (ArgumentException exception)
            {
                throw new InvalidDataException("Données métier invalides.", exception);
            }
            catch (InvalidOperationException exception)
            {
                throw new InvalidDataException("Données métier invalides.", exception);
            }
        }

        return commandes;
    }

    private static CommandeDonnees VersDonnees(Commande commande)
    {
        List<LigneDonnees?> lignes = new List<LigneDonnees?>();
        foreach (LigneCommande ligne in commande.Lignes)
        {
            LigneDonnees donnees = new LigneDonnees(ligne.Produit.Numero,
                ligne.Produit.Nom, ligne.Produit.Prix, ligne.Quantite);
            lignes.Add(donnees);
        }

        return new CommandeDonnees(commande.Numero, commande.EstConfirmee, lignes);
    }

}
