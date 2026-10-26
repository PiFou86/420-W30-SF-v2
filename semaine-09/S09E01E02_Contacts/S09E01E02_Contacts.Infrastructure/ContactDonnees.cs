using Contacts.Domaine;

namespace Contacts.Infrastructure;

public sealed class ContactDonnees
{
    public Guid Identifiant { get; set; }
    public string? Nom { get; set; }
    public string? Courriel { get; set; }

    public ContactDonnees()
    {
    }

    public ContactDonnees(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        Identifiant = contact.Identifiant;
        Nom = contact.Nom;
        Courriel = contact.Courriel;
    }

    public Contact VersContact()
    {
        // JSON peut fournir null; le constructeur du domaine réapplique ses préconditions.
        return new Contact(Identifiant, Nom!, Courriel!);
    }
}
