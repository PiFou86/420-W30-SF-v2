using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

public sealed class JournalIncidents
{
    private readonly string m_chemin;

    public JournalIncidents(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = chemin;
    }

    public void Consigner(string operation, Exception exception)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);
        ArgumentNullException.ThrowIfNull(exception);
        string ligne = $"{DateTimeOffset.UtcNow:u} | {operation} | {exception.GetType().Name}{Environment.NewLine}";
        try
        {
            File.AppendAllText(m_chemin, ligne);
        }
        catch (Exception erreurJournal) when (erreurJournal is IOException or UnauthorizedAccessException)
        {
            // L'indisponibilité du journal ne masque pas le message sûr de présentation.
        }
    }
}
