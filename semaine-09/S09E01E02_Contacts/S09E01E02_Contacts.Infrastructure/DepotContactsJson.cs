using System.Text.Json;
using Contacts.Application;
using Contacts.Domaine;

namespace Contacts.Infrastructure;

public sealed class DepotContactsJson : IDepotContacts
{
    private static readonly JsonSerializerOptions s_options = new JsonSerializerOptions() { WriteIndented = true };
    private readonly string m_chemin;

    public DepotContactsJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = chemin;
    }

    public void Ajouter(Contact contact)
    {
        ArgumentNullException.ThrowIfNull(contact);
        List<Contact> contacts = Lire();
        if (contacts.Any(item => item.Identifiant == contact.Identifiant
            || string.Equals(item.Courriel, contact.Courriel, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Contact déjà présent.");
        }

        contacts.Add(contact);
        ContactDonnees[] donnees = contacts.Select(item => new ContactDonnees(item)).ToArray();
        File.WriteAllText(m_chemin, JsonSerializer.Serialize(donnees, s_options));
    }

    public IReadOnlyCollection<Contact> ObtenirTous()
    {
        return Lire().ToArray();
    }

    public Contact? ObtenirParCourriel(string courriel)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(courriel);
        string recherche = courriel.Trim();
        return Lire().SingleOrDefault(contact =>
            string.Equals(contact.Courriel, recherche, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyCollection<Contact> Rechercher(string motClef)
    {
        ArgumentNullException.ThrowIfNull(motClef);
        string recherche = motClef.Trim();
        List<Contact> contacts = Lire();
        // E2-R : seul bloc hors présentation à compléter. Conserver les trois lignes ci-dessus.
        throw new NotImplementedException("E2-R : recherche par mot-clef à compléter à l'exercice 2.");
    }

    private List<Contact> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin);
        }
        catch (FileNotFoundException)
        {
            return new List<Contact>();
        }

        List<ContactDonnees?>? donnees = JsonSerializer.Deserialize<List<ContactDonnees?>>(json);
        if (donnees == null)
        {
            throw new InvalidDataException("Une liste de contacts est attendue.");
        }

        List<Contact> contacts = new List<Contact>();
        HashSet<Guid> identifiants = new HashSet<Guid>();
        HashSet<string> courriels = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (ContactDonnees? item in donnees)
        {
            try
            {
                if (item == null)
                {
                    throw new InvalidDataException("Un contact ne peut pas être nul.");
                }

                Contact contact = item.VersContact();
                if (!identifiants.Add(contact.Identifiant) || !courriels.Add(contact.Courriel))
                {
                    throw new InvalidDataException("Contact dupliqué.");
                }

                contacts.Add(contact);
            }
            catch (ArgumentException exception)
            {
                throw new InvalidDataException("Données de contact invalides.", exception);
            }
        }

        return contacts;
    }
}
