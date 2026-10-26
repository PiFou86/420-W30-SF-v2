using Contacts.Application;
using Contacts.Domaine;
using Contacts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Contacts.Tests;

[Trait("Etape", "E1")]
public sealed class PreconditionsTests
{
    [Fact]
    public void Contact_RefuseIdentifiantVideEtChampsInvalides()
    {
        Assert.Throws<ArgumentException>(() => new Contact(Guid.Empty, "Alex", "a@test.test"));
        Assert.Throws<ArgumentNullException>(() => new Contact(Guid.NewGuid(), null!, "a@test.test"));
        Assert.Throws<ArgumentException>(() => new Contact(Guid.NewGuid(), " ", "a@test.test"));
        Assert.Throws<ArgumentNullException>(() => new Contact(Guid.NewGuid(), "Alex", null!));
        Assert.Throws<ArgumentException>(() => new Contact(Guid.NewGuid(), "Alex", " "));
    }

    [Fact]
    public void CollaborateursEtDemandeNull_SontRejetes()
    {
        DepotContactsJson depot = new DepotContactsJson("inutile.json");
        Assert.Throws<ArgumentNullException>(() => new CreerContact(null!));
        Assert.Throws<ArgumentNullException>(() => new ConsulterContacts(null!));
        Assert.Throws<ArgumentNullException>(() => new CreerContact(depot).Executer(null!));
        Assert.Throws<ArgumentNullException>(() => new ConsulterContacts(depot).Rechercher(null!));
        Assert.Throws<ArgumentNullException>(() => new ContactDto(null!));
        Assert.Throws<ArgumentNullException>(() => new ContactDonnees(null!));
        Assert.Throws<ArgumentException>(() => new ContactDonnees().VersContact());
    }

    [Fact]
    public void Depot_RefuseArgumentsInvalidesAvantTouteLecture()
    {
        Assert.Throws<ArgumentNullException>(() => new DepotContactsJson(null!));
        Assert.Throws<ArgumentException>(() => new DepotContactsJson(" "));
        DepotContactsJson depot = new DepotContactsJson("inutile.json");
        Assert.Throws<ArgumentNullException>(() => depot.Ajouter(null!));
        Assert.Throws<ArgumentNullException>(() => depot.ObtenirParCourriel(null!));
        Assert.Throws<ArgumentException>(() => depot.ObtenirParCourriel(" "));
        Assert.Throws<ArgumentNullException>(() => depot.Rechercher(null!));
    }

    [Fact]
    public void Result_ProtegeSesFrontieres()
    {
        Assert.Throws<ArgumentNullException>(() => Result<ContactDto>.Succes(null!));
        Assert.Throws<ArgumentNullException>(() => Result<ContactDto>.Echec(null!));
        Assert.Throws<ArgumentException>(() => Result<ContactDto>.Echec(" "));
        Result<ContactDto> resultat = Result<ContactDto>.Echec("Refus attendu");
        Assert.Throws<InvalidOperationException>(() => resultat.Valeur);
    }

    [Fact]
    public void Configuration_RefuseDependanceBaseEtCheminsConfondus()
    {
        IConfiguration configuration = new ConfigurationBuilder().Build();
        Assert.Throws<ArgumentNullException>(() => new ConfigurationContacts(null!, Path.GetTempPath()));
        Assert.Throws<ArgumentNullException>(() => new ConfigurationContacts(configuration, null!));
        Assert.Throws<ArgumentException>(() => new ConfigurationContacts(configuration, " "));
        IConfiguration confondue = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Donnees:Chemin"] = "commun.json", ["Journal:Chemin"] = "commun.json"
        }).Build();
        Assert.Throws<InvalidOperationException>(() => new ConfigurationContacts(confondue, Path.GetTempPath()));
    }

    [Fact]
    public void Journal_RefuseArgumentsInvalides()
    {
        Assert.Throws<ArgumentNullException>(() => new JournalIncidents(null!));
        Assert.Throws<ArgumentException>(() => new JournalIncidents(" "));
        JournalIncidents journal = new JournalIncidents("inutile.log");
        Assert.Throws<ArgumentNullException>(() => journal.Consigner(null!, new IOException()));
        Assert.Throws<ArgumentException>(() => journal.Consigner(" ", new IOException()));
        Assert.Throws<ArgumentNullException>(() => journal.Consigner("lire", null!));
    }

    [Fact]
    public void Journal_NeCopiePasLeMessageBrutEtTolereSonIndisponibilite()
    {
        string dossier = Path.Combine(Path.GetTempPath(), "contacts-journal-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            string chemin = Path.Combine(dossier, "journal.log");
            new JournalIncidents(chemin).Consigner("lire contacts", new IOException("SECRET chemin sensible"));
            string texte = File.ReadAllText(chemin);
            Assert.Contains("lire contacts", texte);
            Assert.Contains("IOException", texte);
            Assert.DoesNotContain("SECRET", texte);
            new JournalIncidents(Path.Combine(dossier, "absent", "journal.log"))
                .Consigner("lire contacts", new IOException());
        }
        finally
        {
            Directory.Delete(dossier, true);
        }
    }

    [Fact]
    public void Configuration_ResoutLesCheminsDepuisLeDossierFourni()
    {
        string dossier = Path.Combine(Path.GetTempPath(), "contacts-config-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            ConfigurationContacts configuration = new ConfigurationContacts(new ConfigurationBuilder().Build(), dossier);
            configuration.CreerDepot().Ajouter(new Contact(Guid.NewGuid(), "Alex", "a@test.test"));
            configuration.CreerJournal().Consigner("lecture", new IOException());
            Assert.True(File.Exists(Path.Combine(dossier, "contacts.json")));
            Assert.True(File.Exists(Path.Combine(dossier, "contacts.log")));
        }
        finally
        {
            Directory.Delete(dossier, true);
        }
    }
}
