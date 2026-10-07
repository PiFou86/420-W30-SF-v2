
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class JournalIncidentsTests : IDisposable
{
    private readonly string m_repertoire = Path.Combine(Path.GetTempPath(), $"journal-test-{Guid.NewGuid():N}");

    public JournalIncidentsTests()
    {
        Directory.CreateDirectory(m_repertoire);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructeur_RefuseCheminInvalide(string? chemin)
    {
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => new JournalIncidents(chemin!)));
    }

    [Fact]
    public void Consigner_EcritOperationEtTypeSansMessageSensible()
    {
        string chemin = Path.Combine(m_repertoire, "journal.log");
        new JournalIncidents(chemin).Consigner("lecture", new IOException("secret-simule /chemin/prive"));
        string contenu = File.ReadAllText(chemin);
        Assert.Contains("lecture", contenu);
        Assert.Contains("IOException", contenu);
        Assert.DoesNotContain("secret-simule", contenu);
        Assert.DoesNotContain("/chemin/prive", contenu);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Consigner_RefuseOperationInvalide(string? operation)
    {
        JournalIncidents journal = new(Path.Combine(m_repertoire, "journal.log"));
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => journal.Consigner(operation!, new IOException())));
    }

    [Fact]
    public void Consigner_RefuseExceptionNulle()
    {
        JournalIncidents journal = new(Path.Combine(m_repertoire, "journal.log"));
        Assert.Throws<ArgumentNullException>(() => journal.Consigner("lecture", null!));
    }

    [Fact]
    public void Consigner_JournalIndisponibleNeMasquePasIncident()
    {
        JournalIncidents journal = new(m_repertoire); // Un dossier n'est pas un fichier journal.
        Assert.Null(Record.Exception(() => journal.Consigner("lecture", new IOException())));
    }

    public void Dispose()
    {
        Directory.Delete(m_repertoire, recursive: true);
    }
}
