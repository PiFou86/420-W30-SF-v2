using Contacts.Application;


namespace Contacts.Infrastructure;

public sealed class JournalIncidents : IJournalIncidents
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
        catch (UnauthorizedAccessException)
        {
            // L'indisponibilité du journal ne masque pas le message sûr de présentation.
        }
        catch (IOException)
        {
            // L'indisponibilité du journal ne masque pas le message sûr de présentation.
        }
    }
}
