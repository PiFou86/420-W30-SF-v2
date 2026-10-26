using Commandes.Application;
using Commandes.Infrastructure;

namespace Commandes.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        // Même cas d'utilisation que le formulaire, sans fenêtre graphique.
        ServiceCommandes service = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        Afficher("Avant la création", service.Consulter());
        Afficher("Création", service.Creer(1042).Valeur);
        Afficher("Ajout de deux sandwichs", service.AjouterProduit(2, 2).Valeur);
        Afficher("Enregistrement", service.Enregistrer().Valeur);
        Afficher("Nouvelle commande", service.Creer(1043).Valeur);
    }

    private static void Afficher(string etape, CommandeDto? commande)
    {
        if (commande == null)
        {
            Console.Out.WriteLine($"{etape} : aucune commande.");
            return;
        }

        Console.Out.WriteLine($"{etape} : commande {commande.Numero}, "
            + $"{commande.Lignes.Count} ligne(s), {commande.Total:F2}, enregistrée : {commande.EstEnregistree}.");
    }
}
