using Commandes.Application;

namespace Commandes.Presentation;

public sealed class ControleurCommande : IDisposable
{
    public ControleurCommande(ModeleCommande modele, IVueCommande vue, IJournalIncidents journal)
    {
        ArgumentNullException.ThrowIfNull(modele);
        ArgumentNullException.ThrowIfNull(vue);
        ArgumentNullException.ThrowIfNull(journal);
        // À compléter : collaborateurs, abonnement et affichage initial.
    }

    public void Nouvelle(int numero)
    {
        throw new NotImplementedException("Coordonner la demande et le message de refus.");
    }

    public void Ajouter(int numeroProduit, int quantite)
    {
        throw new NotImplementedException("Coordonner la demande et le message de refus.");
    }

    public void Enregistrer()
    {
        throw new NotImplementedException("Coordonner la sauvegarde et les incidents.");
    }

    public void Dispose()
    {
        // À compléter : terminer l'abonnement une seule fois.
    }
}
