using System.Text.Json;
using Contacts.Application;
using Contacts.Domaine;
using Contacts.Infrastructure;
using Xunit;

namespace Contacts.Tests;

public sealed class DepotContactsJsonTests : IDisposable
{
    private readonly string m_dossier = Path.Combine(Path.GetTempPath(), "contacts-tests-" + Guid.NewGuid());
    private string Chemin
    {
        get
        {
            return Path.Combine(m_dossier, "contacts.json");
        }
    }

    public DepotContactsJsonTests()
    {
        Directory.CreateDirectory(m_dossier);
    }

    private DepotContactsJson CreerDepotRempli()
    {
        DepotContactsJson depot = new DepotContactsJson(Chemin);
        depot.Ajouter(new Contact(Guid.NewGuid(), "Zoé Roy", "zoe@ecole.test"));
        depot.Ajouter(new Contact(Guid.NewGuid(), "Alex Tremblay", "alex@travail.test"));
        depot.Ajouter(new Contact(Guid.NewGuid(), "Mia Roy", "mia@ecole.test"));
        return depot;
    }

    [Fact, Trait("Etape", "E1")]
    public void FichierAbsent_ListeVideSansCreationDeFichier()
    {
        Assert.Empty(new DepotContactsJson(Chemin).ObtenirTous());
        Assert.False(File.Exists(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void Creation_EstRelueAvecUnNouveauDepot()
    {
        CreerContact creation = new CreerContact(new DepotContactsJson(Chemin));
        Result<ContactDto> resultat = creation.Executer(new DemandeCreationContact("  Alex Roy  ", " alex@test.test "));
        Assert.True(resultat.EstSucces);
        Contact relu = Assert.Single(new DepotContactsJson(Chemin).ObtenirTous());
        Assert.Equal(resultat.Valeur.Identifiant, relu.Identifiant);
        Assert.Equal("Alex Roy", relu.Nom);
        Assert.Equal("alex@test.test", relu.Courriel);
    }

    [Fact, Trait("Etape", "E1")]
    public void CourrielDuplique_RefusSansReecriture()
    {
        CreerContact creation = new CreerContact(new DepotContactsJson(Chemin));
        creation.Executer(new DemandeCreationContact("Alex", "alex@test.test"));
        byte[] avant = File.ReadAllBytes(Chemin);
        Result<ContactDto> resultat = creation.Executer(new DemandeCreationContact("Autre", " ALEX@TEST.TEST "));
        Assert.False(resultat.EstSucces);
        Assert.Equal(avant, File.ReadAllBytes(Chemin));
        Assert.Single(new DepotContactsJson(Chemin).ObtenirTous());
    }

    [Theory, Trait("Etape", "E1")]
    [InlineData(null, "a@test.test")]
    [InlineData("   ", "a@test.test")]
    [InlineData("Alex", null)]
    [InlineData("Alex", " ")]
    public void ChampsVides_RefusSansEcriture(string? nom, string? courriel)
    {
        Result<ContactDto> resultat = new CreerContact(new DepotContactsJson(Chemin))
            .Executer(new DemandeCreationContact(nom, courriel));
        Assert.False(resultat.EstSucces);
        Assert.NotEmpty(resultat.MessageErreur);
        Assert.False(File.Exists(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void CourrielSimple_SansValidationComplexe()
    {
        Assert.True(new CreerContact(new DepotContactsJson(Chemin))
            .Executer(new DemandeCreationContact("Alex", "adresse-locale")).EstSucces);
    }

    [Fact, Trait("Etape", "E1")]
    public void Consultation_DtoEtCollectionsIndependants()
    {
        DepotContactsJson depot = CreerDepotRempli();
        IReadOnlyCollection<Contact> premiere = depot.ObtenirTous();
        Assert.NotSame(premiere.First(), depot.ObtenirTous().First());
        ContactDto[] dtos = new ConsulterContacts(depot).ObtenirTous().ToArray();
        Assert.Equal(new[] { "Zoé Roy", "Alex Tremblay", "Mia Roy" }, dtos.Select(item => item.Nom));
        Array.Clear(dtos);
        Assert.Equal(3, depot.ObtenirTous().Count);
    }

    [Fact, Trait("Etape", "E1")]
    public void RechercheExacteCourriel_IgnoreCasseEtEspaces()
    {
        DepotContactsJson depot = CreerDepotRempli();
        Assert.Equal("Zoé Roy", depot.ObtenirParCourriel(" ZOE@ECOLE.TEST ")!.Nom);
        Assert.Null(depot.ObtenirParCourriel("inconnu@test.test"));
    }

    [Theory, Trait("Etape", "E1")]
    [InlineData("null")]
    [InlineData("[null]")]
    [InlineData("[{}]")]
    public void DonneesInvalides_NeSontPasEcrasees(string json)
    {
        File.WriteAllText(Chemin, json);
        DepotContactsJson depot = new DepotContactsJson(Chemin);
        Assert.Throws<InvalidDataException>(() => depot.ObtenirTous());
        Assert.Throws<InvalidDataException>(() => depot.Ajouter(new Contact(Guid.NewGuid(), "Alex", "a@test.test")));
        Assert.Equal(json, File.ReadAllText(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void JsonMalForme_NeDevientPasUneListeVide()
    {
        File.WriteAllText(Chemin, "{");
        Assert.Throws<JsonException>(() => new DepotContactsJson(Chemin).ObtenirTous());
        Assert.Equal("{", File.ReadAllText(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void DoublonsDansLeFichier_SontRejetes()
    {
        Contact contact = new Contact(Guid.NewGuid(), "Alex", "a@test.test");
        string json = JsonSerializer.Serialize(new[] { new ContactDonnees(contact), new ContactDonnees(contact) });
        File.WriteAllText(Chemin, json);
        Assert.Throws<InvalidDataException>(() => new DepotContactsJson(Chemin).ObtenirTous());
        Assert.Equal(json, File.ReadAllText(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void AjoutDirectDuplique_EstRejeteSansEcriture()
    {
        DepotContactsJson depot = CreerDepotRempli();
        byte[] avant = File.ReadAllBytes(Chemin);
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(depot.ObtenirTous().First()));
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(new Contact(Guid.NewGuid(), "Autre", "ZOE@ECOLE.TEST")));
        Assert.Equal(avant, File.ReadAllBytes(Chemin));
    }

    [Fact, Trait("Etape", "E1")]
    public void DossierManquant_EstUnIncidentPasUnRefus()
    {
        DepotContactsJson depot = new DepotContactsJson(Path.Combine(m_dossier, "absent", "contacts.json"));
        Assert.Throws<DirectoryNotFoundException>(() => new CreerContact(depot)
            .Executer(new DemandeCreationContact("Alex", "a@test.test")));
    }

    [Theory, Trait("Etape", "E2")]
    [InlineData("Roy", "Zoé Roy|Mia Roy")]
    [InlineData("travail", "Alex Tremblay")]
    [InlineData("rOy", "Zoé Roy|Mia Roy")]
    [InlineData("  ROY  ", "Zoé Roy|Mia Roy")]
    [InlineData("", "Zoé Roy|Alex Tremblay|Mia Roy")]
    [InlineData("   ", "Zoé Roy|Alex Tremblay|Mia Roy")]
    [InlineData("introuvable", "")]
    public void Recherche_RespecteLeContrat(string motClef, string nomsAttendus)
    {
        DepotContactsJson depot = CreerDepotRempli();
        byte[] avant = File.ReadAllBytes(Chemin);
        DateTime dateAvant = File.GetLastWriteTimeUtc(Chemin);
        IReadOnlyCollection<ContactDto> resultats = new ConsulterContacts(depot).Rechercher(motClef);
        Assert.Equal(nomsAttendus, string.Join("|", resultats.Select(item => item.Nom)));
        Assert.Equal(avant, File.ReadAllBytes(Chemin));
        Assert.Equal(dateAvant, File.GetLastWriteTimeUtc(Chemin));
        Assert.Equal(3, new DepotContactsJson(Chemin).ObtenirTous().Count);
    }

    [Fact, Trait("Etape", "E2")]
    public void RechercheSansFichier_ResteSansFichier()
    {
        Assert.Empty(new DepotContactsJson(Chemin).Rechercher("Alex"));
        Assert.False(File.Exists(Chemin));
    }

    [Fact, Trait("Etape", "E2")]
    public void Recherche_DeuxSortiesIndependantesSansMutation()
    {
        DepotContactsJson depot = CreerDepotRempli();
        Contact[] trouves = depot.Rechercher("Roy").ToArray();
        Assert.NotSame(trouves[0], depot.Rechercher("Roy").First());
        Array.Clear(trouves);
        Assert.Equal(2, depot.Rechercher("Roy").Count);
        Assert.Equal(3, depot.ObtenirTous().Count);
    }

    public void Dispose()
    {
        Directory.Delete(m_dossier, true);
    }
}
