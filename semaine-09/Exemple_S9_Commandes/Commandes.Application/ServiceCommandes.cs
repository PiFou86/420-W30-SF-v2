using Commandes.Domaine;

namespace Commandes.Application;

public sealed class ServiceCommandes
{
    private readonly IDepotCommandes m_depot;
    private readonly ICatalogueProduits m_catalogue;
    private Commande? m_commande;

    public ServiceCommandes(IDepotCommandes depot, ICatalogueProduits catalogue)
    {
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(catalogue);
        m_depot = depot;
        m_catalogue = catalogue;
    }

    public CommandeDto? Consulter()
    {
        if (m_commande == null)
        {
            return null;
        }

        return new CommandeDto(m_commande);
    }

    public IReadOnlyCollection<ProduitDto> ConsulterProduits()
    {
        return m_catalogue.ObtenirTous().Select(produit =>
            new ProduitDto(produit.Numero, produit.Nom, produit.Prix)).ToArray();
    }

    // Aucun brouillon implicite au démarrage : la création est un cas d'utilisation.
    public Result<CommandeDto> Creer(int numero, bool abandonnerBrouillon = false)
    {
        if (numero <= 0)
        {
            return Result<CommandeDto>.Echec("Le numéro doit être positif.");
        }

        if (m_commande != null && !m_commande.EstConfirmee
            && m_commande.Lignes.Count > 0 && !abandonnerBrouillon)
        {
            return Result<CommandeDto>.Echec("Enregistrez la commande ou confirmez son abandon.");
        }

        if (m_depot.Obtenir(numero) != null)
        {
            return Result<CommandeDto>.Echec("Ce numéro de commande est déjà enregistré.");
        }

        // Remplacer le brouillon seulement après les validations et la lecture du dépôt.
        m_commande = new Commande(numero);
        return Result<CommandeDto>.Succes(new CommandeDto(m_commande));
    }

    public Result<CommandeDto> AjouterProduit(int numeroProduit, int quantite)
    {
        if (m_commande == null)
        {
            return Result<CommandeDto>.Echec("Créez d'abord une commande.");
        }

        if (numeroProduit <= 0 || quantite <= 0)
        {
            return Result<CommandeDto>.Echec("Sélectionnez un produit et une quantité positive.");
        }

        if (m_commande.EstConfirmee)
        {
            return Result<CommandeDto>.Echec("Créez une nouvelle commande avant d'ajouter une ligne.");
        }

        Produit? produit = m_catalogue.Obtenir(numeroProduit);
        if (produit == null)
        {
            return Result<CommandeDto>.Echec("Produit indisponible.");
        }

        m_commande.Ajouter(produit, quantite);
        return Result<CommandeDto>.Succes(new CommandeDto(m_commande));
    }

    public Result<CommandeDto> Enregistrer()
    {
        if (m_commande == null)
        {
            return Result<CommandeDto>.Echec("Créez d'abord une commande.");
        }

        if (m_commande.Lignes.Count == 0)
        {
            return Result<CommandeDto>.Echec("Ajoutez au moins une ligne.");
        }

        if (m_commande.EstConfirmee || m_depot.Obtenir(m_commande.Numero) != null)
        {
            return Result<CommandeDto>.Echec("Numéro déjà enregistré.");
        }

        Commande aEnregistrer = m_commande.Copier();
        aEnregistrer.Confirmer();
        m_depot.Ajouter(aEnregistrer);
        m_commande = aEnregistrer;
        return Result<CommandeDto>.Succes(new CommandeDto(m_commande));
    }
}
