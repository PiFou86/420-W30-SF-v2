using Contacts.Domaine;

namespace Contacts.Application;

public sealed class CreerContact
{
    private readonly IDepotContacts m_depot;

    public CreerContact(IDepotContacts depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    public Result<ContactDto> Executer(DemandeCreationContact demande)
    {
        ArgumentNullException.ThrowIfNull(demande);
        if (string.IsNullOrWhiteSpace(demande.Nom) || string.IsNullOrWhiteSpace(demande.Courriel))
        {
            return Result<ContactDto>.Echec("Le nom et le courriel sont requis.");
        }

        string courriel = demande.Courriel.Trim();
        if (m_depot.ObtenirParCourriel(courriel) != null)
        {
            return Result<ContactDto>.Echec("Ce courriel est déjà utilisé.");
        }

        Contact contact = new Contact(Guid.NewGuid(), demande.Nom, courriel);
        m_depot.Ajouter(contact);
        // Un succès est annoncé seulement après la réussite de l'écriture.
        return Result<ContactDto>.Succes(new ContactDto(contact));
    }
}
