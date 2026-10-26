using Commandes.Application;
using Microsoft.Extensions.Configuration;

namespace Commandes.Infrastructure;

// Les deux exécutables réutilisent la même configuration des détails techniques.
public sealed class ConfigurationCommandes
{
    private readonly string m_format;
    private readonly string m_cheminDonnees;
    private readonly string m_cheminJournal;

    public ConfigurationCommandes(IConfiguration configuration, string dossierBase)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentException.ThrowIfNullOrWhiteSpace(dossierBase);
        m_format = configuration["Donnees:Format"] ?? "memoire";
        m_cheminDonnees = Path.GetFullPath(configuration["Donnees:Chemin"] ?? "commandes.json", dossierBase);
        m_cheminJournal = Path.GetFullPath(configuration["Journal:Chemin"] ?? "commandes.log", dossierBase);
        if (string.Equals(m_cheminDonnees, m_cheminJournal, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Journal et données doivent être distincts.");
        }
    }

    public IDepotCommandes CreerDepot()
    {
        switch (m_format.ToLowerInvariant())
        {
            case "memoire":
                return new DepotCommandesMemoire();
            case "json":
                return new DepotCommandesJson(m_cheminDonnees);
            default:
                throw new InvalidOperationException("Format inconnu.");
        }
    }

    public IJournalIncidents CreerJournal()
    {
        return new JournalIncidents(m_cheminJournal);
    }
}
