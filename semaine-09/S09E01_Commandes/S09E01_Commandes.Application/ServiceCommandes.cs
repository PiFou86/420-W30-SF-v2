using Commandes.Domaine;

namespace Commandes.Application;

public sealed class ServiceCommandes
{
    private readonly IDepotCommandes m_depot;
    private readonly ICatalogueProduits m_catalogue;
    private Commande m_commande = new(1);

    public ServiceCommandes(IDepotCommandes depot, ICatalogueProduits catalogue)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(catalogue);
        m_depot = depot;
        m_catalogue = catalogue;
    }

    public CommandeDto Consulter()
    {
        return new CommandeDto(m_commande);
    }

    public IReadOnlyCollection<ProduitDto> ConsulterProduits()
    {
        return m_catalogue.ObtenirTous().Select(produit =>
            new ProduitDto(produit.Numero, produit.Nom, produit.Prix)).ToArray();
    }

    public Result<CommandeDto> Creer(int numero)
    {
        if (numero <= 0)
        {
            return Result<CommandeDto>.Echec("Le numéro doit être positif.");
        }

        m_commande = new Commande(numero);
        return Result<CommandeDto>.Succes(Consulter());
    }

    public Result<CommandeDto> AjouterProduit(int numeroProduit, int quantite)
    {
        if (numeroProduit <= 0 || quantite <= 0)
        {
            return Result<CommandeDto>.Echec("Sélectionnez un produit et une quantité positive.");
        }

        if (m_commande.EstConfirmee)
        {
            return Result<CommandeDto>.Echec("Créez une nouvelle commande avant d'ajouter une ligne.");
        }

        Produit? produit = m_catalogue.Obtenir(numeroProduit);
        if (produit is null)
        {
            return Result<CommandeDto>.Echec("Produit indisponible.");
        }

        m_commande.Ajouter(produit, quantite);
        return Result<CommandeDto>.Succes(Consulter());
    }

    public Result<CommandeDto> Enregistrer()
    {
        if (m_commande.Lignes.Count == 0)
        {
            return Result<CommandeDto>.Echec("Ajoutez au moins une ligne.");
        }

        if (m_commande.EstConfirmee || m_depot.Obtenir(m_commande.Numero) is not null)
        {
            return Result<CommandeDto>.Echec("Numéro déjà enregistré.");
        }

        Commande aEnregistrer = m_commande.Copier();
        aEnregistrer.Confirmer();
        m_depot.Ajouter(aEnregistrer);
        // Le brouillon n'est confirmé qu'après la réussite du dépôt.
        m_commande = aEnregistrer;
        return Result<CommandeDto>.Succes(Consulter());
    }
}
