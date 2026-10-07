using Commandes.Application;
using Commandes.Infrastructure;

namespace S10E01_Facturation.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        ServiceCommandes service = new(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        service.Creer(101);
        service.AjouterProduit(1, 2);
        service.AjouterProduit(2, 1);
        Result<CommandeDto> resultat = service.Enregistrer();
        Console.Out.WriteLine(resultat.EstSucces ? $"Commande {resultat.Valeur.Numero} : {resultat.Valeur.Total:F2}" : resultat.MessageErreur);
    }
}
