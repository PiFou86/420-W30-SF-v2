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

        List<CommandeDonnees?> donnees = JsonSerializer.Deserialize<List<CommandeDonnees?>>(json)
            ?? throw new InvalidDataException("Liste attendue.");
        List<Commande> commandes = new();
        HashSet<int> numeros = new();
        foreach (CommandeDonnees? item in donnees)
        {
            try
            {
                if (item is null || item.Lignes is null || !numeros.Add(item.Numero))
                {
                    throw new InvalidDataException("Entrée invalide ou dupliquée.");
                }

                Commande commande = new(item.Numero);
                foreach (LigneDonnees? ligne in item.Lignes)
                {
                    if (ligne is null)
                    {
                        throw new InvalidDataException("Ligne nulle.");
                    }

                    commande.Ajouter(new Produit(ligne.NumeroProduit, ligne.Nom!, ligne.Prix), ligne.Quantite);
                }

                if (item.EstConfirmee)
                {
                    commande.Confirmer();
                }

                commandes.Add(commande);
            }
            catch (Exception exception) when (exception is ArgumentException or InvalidOperationException)
            {
                throw new InvalidDataException("Données métier invalides.", exception);
            }
        }

        return commandes;
    }

    private static CommandeDonnees VersDonnees(Commande commande)
    {
        return new CommandeDonnees(commande.Numero, commande.EstConfirmee,
            commande.Lignes.Select(ligne => (LigneDonnees?)new LigneDonnees(ligne.Produit.Numero,
                ligne.Produit.Nom, ligne.Produit.Prix, ligne.Quantite)).ToList());
    }

    private sealed record CommandeDonnees(int Numero, bool EstConfirmee, List<LigneDonnees?>? Lignes);
    private sealed record LigneDonnees(int NumeroProduit, string? Nom, decimal Prix, int Quantite);
}
