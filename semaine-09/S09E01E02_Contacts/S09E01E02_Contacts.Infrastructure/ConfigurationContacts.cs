using Contacts.Application;
using Microsoft.Extensions.Configuration;

namespace Contacts.Infrastructure;

public sealed class ConfigurationContacts
{
    private readonly string m_cheminContacts;
    private readonly string m_cheminJournal;

    public ConfigurationContacts(IConfiguration configuration, string dossierBase)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentException.ThrowIfNullOrWhiteSpace(dossierBase);
        m_cheminContacts = Path.GetFullPath(configuration["Donnees:Chemin"] ?? "contacts.json", dossierBase);
        m_cheminJournal = Path.GetFullPath(configuration["Journal:Chemin"] ?? "contacts.log", dossierBase);
        if (string.Equals(m_cheminContacts, m_cheminJournal, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Le journal et les contacts doivent utiliser des fichiers distincts.");
        }
    }

    public IDepotContacts CreerDepot()
    {
        return new DepotContactsJson(m_cheminContacts);
    }

    public IJournalIncidents CreerJournal()
    {
        return new JournalIncidents(m_cheminJournal);
    }
}
