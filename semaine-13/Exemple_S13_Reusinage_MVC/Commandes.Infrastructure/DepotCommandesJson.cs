using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;
using Commandes.Fichiers;

namespace Commandes.Infrastructure;

public sealed class DepotCommandesJson : IDepotCommandes
{
    private readonly string m_chemin;

    public DepotCommandesJson(string chemin)
    {
        m_chemin = chemin;
    }

    public Commande? Obtenir(int identifiant)
    {
        string json = File.ReadAllText(m_chemin);
        List<CommandeFichier> commandes =
            JsonSerializer.Deserialize<List<CommandeFichier>>(json)
            ?? throw new JsonException("Une liste de commandes est attendue.");
        CommandeFichier? donnees =
            commandes.FirstOrDefault(item => item.Identifiant == identifiant);
        if (donnees is null)
        {
            return null;
        }

        List<LigneCommande> lignes = new List<LigneCommande>();
        foreach (LigneCommandeFichier ligne in donnees.Lignes)
        {
            lignes.Add(new LigneCommande(ligne.PrixUnitaire, ligne.Quantite));
        }
        return new Commande(donnees.Identifiant, lignes);
    }
}
