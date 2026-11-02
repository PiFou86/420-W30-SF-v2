using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.DotNet;

namespace Commandes.Terminal;

internal static class Program
{
    public static void Main(string[] args)
    {
        ServiceCommandes commandes = new ServiceCommandes(new DepotCommandesMemoire(), new CatalogueProduitsMemoire());
        IJournalIncidents journal = new JournalIncidents(Path.Combine(Path.GetTempPath(), "s10-commandes.log"));
        using ModeleCommande modele = new ModeleCommande(commandes, new ServiceFacturation(commandes), journal);
        VueTerminal vue = new VueTerminal();
        using ControleurCommande controleur = new ControleurCommande(modele, vue, journal);
        ResumeFactureTerminal facture = new ResumeFactureTerminal();
        using IDisposable abonnement = modele.Subscribe(facture);
        facture.OnNext(modele.Consulter());
        controleur.Nouvelle();
        controleur.Ajouter(1, 2);
        controleur.Ajouter(2, 1);
        controleur.Enregistrer();
        // Les abonnements sont encore vivants : les deux vues reçoivent la fin.
        if (args.Contains("--erreur-fatale"))
        {
            modele.SignalerErreurFatale(new IOException("Incident simulé pour la démonstration."));
        }
        else
        {
            modele.Dispose();
        }
    }
}
