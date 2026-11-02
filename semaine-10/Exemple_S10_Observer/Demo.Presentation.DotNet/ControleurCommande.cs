using System.Text.Json;
using Commandes.Application;

namespace Commandes.Presentation.DotNet;

public sealed class ControleurCommande : IDisposable
{
    private readonly ModeleCommande m_modele;
    private readonly IVueCommande m_vue;
    private readonly IJournalIncidents m_journal;
    private readonly IDisposable m_abonnement;
    private bool m_estDispose;

    public ControleurCommande(ModeleCommande modele, IVueCommande vue, IJournalIncidents journal)
    {
        ArgumentNullException.ThrowIfNull(modele);
        ArgumentNullException.ThrowIfNull(vue);
        ArgumentNullException.ThrowIfNull(journal);
        m_modele = modele;
        m_vue = vue;
        m_journal = journal;
        m_abonnement = modele.Subscribe(vue);
        try
        {
            vue.OnNext(modele.Consulter());
        }
        catch
        {
            m_abonnement.Dispose();
            throw;
        }
    }

    public void Nouvelle()
    {
        Executer(m_modele.Nouvelle, "nouvelle commande");
    }

    public void Nouvelle(int numero)
    {
        Executer(() => m_modele.Nouvelle(numero), "nouvelle commande");
    }

    public void Ajouter(int numeroProduit, int quantite)
    {
        Executer(() => m_modele.Ajouter(numeroProduit, quantite), "ajouter produit");
    }

    public void Enregistrer()
    {
        Executer(m_modele.Enregistrer, "enregistrer commande");
    }

    private void Executer(Func<Result<CommandeDto>> action, string operation)
    {
        ObjectDisposedException.ThrowIf(m_estDispose, this);
        try
        {
            Result<CommandeDto> resultat = action();
            if (!resultat.EstSucces)
            {
                m_vue.PresenterRefus(resultat.MessageErreur);
            }
            // Le modèle a déjà notifié les vues en succès : pas de second affichage.
        }
        catch (IOException exception)
        {
            m_journal.Consigner(operation, exception);
            m_vue.PresenterIncident("Action impossible pour le moment. Vos lignes sont conservées.");
        }
        catch (InvalidDataException exception)
        {
            m_journal.Consigner(operation, exception);
            m_vue.PresenterIncident("Action impossible pour le moment. Vos lignes sont conservées.");
        }
        catch (UnauthorizedAccessException exception)
        {
            m_journal.Consigner(operation, exception);
            m_vue.PresenterIncident("Action impossible pour le moment. Vos lignes sont conservées.");
        }
        catch (JsonException exception)
        {
            m_journal.Consigner(operation, exception);
            m_vue.PresenterIncident("Action impossible pour le moment. Vos lignes sont conservées.");
        }
    }

    public void Dispose()
    {
        if (!m_estDispose)
        {
            m_abonnement.Dispose();
            m_estDispose = true;
        }
    }
}
