using System.Text.Json;
using Commandes.Fichiers;

namespace Commandes.Presentation.WinForms;

// Même formulaire que dans FormCommande.cs. Cette partie contient la consultation initiale.
public partial class FormCommande
{
    // Volontairement dans le formulaire : fichier, données, calcul et format monétaire.
    public string ConsulterTotal(int identifiant, string chemin)
    {
        string json = File.ReadAllText(chemin);
        List<CommandeFichier> commandes =
            JsonSerializer.Deserialize<List<CommandeFichier>>(json)
            ?? throw new JsonException("Une liste de commandes est attendue.");
        CommandeFichier? commande =
            commandes.FirstOrDefault(item => item.Identifiant == identifiant);
        if (commande is null)
        {
            return "Commande introuvable.";
        }

        decimal total = 0m;
        foreach (LigneCommandeFichier ligne in commande.Lignes)
        {
            total += ligne.PrixUnitaire * ligne.Quantite;
        }
        return total.ToString("C2");
    }
}
