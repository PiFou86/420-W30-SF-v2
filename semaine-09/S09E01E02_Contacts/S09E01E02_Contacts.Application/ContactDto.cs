using Contacts.Domaine;

namespace Contacts.Application;

public sealed class ContactDto
{
    public Guid Identifiant { get; }
    public string Nom { get; }
    public string Courriel { get; }

    public ContactDto(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        Identifiant = contact.Identifiant;
        Nom = contact.Nom;
        Courriel = contact.Courriel;
    }
}
