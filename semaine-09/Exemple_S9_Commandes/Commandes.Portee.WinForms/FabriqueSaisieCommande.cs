using System.Text.Json;
using Commandes.Application;
using Commandes.Infrastructure;
using Commandes.Presentation.WinForms;
using Microsoft.Extensions.DependencyInjection;

namespace Commandes.Portee.WinForms;

public sealed class FabriqueSaisieCommande : IFabriqueSaisieCommande
{
    private readonly IServiceScopeFactory m_fabriquePortees;
    private readonly IJournalIncidents m_journal;

    public FabriqueSaisieCommande(IServiceScopeFactory fabriquePortees, IJournalIncidents journal)
    {
        ArgumentNullException.ThrowIfNull(fabriquePortees);
        ArgumentNullException.ThrowIfNull(journal);
        m_fabriquePortees = fabriquePortees;
        m_journal = journal;
    }

    public CommandeDto? Ouvrir(Form proprietaire, int numeroPropose)
    {
        ArgumentNullException.ThrowIfNull(proprietaire);
        if (numeroPropose <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numeroPropose));
        }

        // Point d'arrêt : une portée par clic, gardée pendant toute la saisie modale.
        using IServiceScope portee = m_fabriquePortees.CreateScope();
        TemoinPortee temoin = portee.ServiceProvider.GetRequiredService<TemoinPortee>();
        ServiceCommandes service = portee.ServiceProvider.GetRequiredService<ServiceCommandes>();
        string identifiantCourt = temoin.Identifiant.ToString("N").Substring(0, 8);

        // Le simple dialogue de numéro échange des données, sans dépendance au conteneur.
        while (true)
        {
            using FormNouvelleCommande saisie = new FormNouvelleCommande(numeroPropose);
            saisie.Text = $"Nouvelle commande — portée {identifiantCourt}";
            if (saisie.ShowDialog(proprietaire) != DialogResult.OK)
            {
                return null;
            }

            SaisieNouvelleCommande? resultatSaisie = saisie.Result;
            if (resultatSaisie == null)
            {
                throw new InvalidOperationException("Le dialogue validé doit fournir son résultat.");
            }

            numeroPropose = resultatSaisie.Numero;
            try
            {
                Result<CommandeDto> creation = service.Creer(numeroPropose);
                if (!creation.EstSucces)
                {
                    MessageBox.Show(proprietaire, creation.MessageErreur, "Nouvelle commande",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    continue;
                }
            }
            catch (InvalidDataException exception)
            {
                SignalerIncident(proprietaire, exception);
                continue;
            }
            catch (UnauthorizedAccessException exception)
            {
                SignalerIncident(proprietaire, exception);
                continue;
            }
            catch (JsonException exception)
            {
                SignalerIncident(proprietaire, exception);
                continue;
            }
            catch (IOException exception)
            {
                SignalerIncident(proprietaire, exception);
                continue;
            }

            break;
        }

        // Le formulaire reçoit le même ServiceCommandes que celui résolu plus haut.
        // La portée possède ce formulaire : elle le libérera après ShowDialog.
        FormCommande fenetre = portee.ServiceProvider.GetRequiredService<FormCommande>();
        fenetre.Mode = ModeFormCommande.CommandeUnique;
        fenetre.Text = $"Commande {numeroPropose} — portée {identifiantCourt}";
        if (fenetre.ShowDialog(proprietaire) != DialogResult.OK)
        {
            return null;
        }

        CommandeDto? resultat = fenetre.Result;
        if (resultat == null)
        {
            throw new InvalidOperationException("Le dialogue validé doit fournir la commande enregistrée.");
        }

        return resultat;
        // Le using libère la fenêtre, le service disposable éventuel et le témoin avant le retour.
    }

    private void SignalerIncident(Form proprietaire, Exception exception)
    {
        m_journal.Consigner("créer commande dans la portée", exception);
        MessageBox.Show(proprietaire,
            "Création impossible pour le moment. Le numéro saisi est conservé pour un nouvel essai.",
            "Nouvelle commande", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
