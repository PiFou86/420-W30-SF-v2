namespace Contacts.Domaine;

public sealed class Contact
{
    public Guid Identifiant { get; }
    public string Nom { get; }
    public string Courriel { get; }

    public Contact(Guid identifiant, string nom, string courriel)
    {
        if (identifiant == Guid.Empty)
        {
            throw new ArgumentException("Un identifiant non vide est requis.", nameof(identifiant));
        }

        ArgumentException.ThrowIfNullOrWhiteSpace(nom);
        ArgumentException.ThrowIfNullOrWhiteSpace(courriel);
        Identifiant = identifiant;
        Nom = nom.Trim();
        Courriel = courriel.Trim();
    }
}
