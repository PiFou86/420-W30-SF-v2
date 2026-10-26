namespace Contacts.Application;

// Une demande transporte la saisie, qui peut encore être invalide.
public sealed class DemandeCreationContact
{
    public string? Nom { get; }
    public string? Courriel { get; }

    public DemandeCreationContact(string? nom, string? courriel)
    {
        Nom = nom;
        Courriel = courriel;
    }
}
