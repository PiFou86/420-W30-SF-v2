using Contacts.Domaine;

namespace Contacts.Application;

public interface IDepotContacts
{
    void Ajouter(Contact contact);
    IReadOnlyCollection<Contact> ObtenirTous();
    Contact? ObtenirParCourriel(string courriel);
    IReadOnlyCollection<Contact> Rechercher(string motClef);
}
