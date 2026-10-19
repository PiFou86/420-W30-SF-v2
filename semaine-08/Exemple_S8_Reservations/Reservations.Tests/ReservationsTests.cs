using Microsoft.Extensions.Logging.Abstractions;
using Reservations.Application;
using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;
namespace Reservations.Tests;

public sealed class ReservationsTests : IDisposable
{
    private readonly string m_dossier = Path.Combine(Path.GetTempPath(), "reservations-s8-" + Guid.NewGuid());

    public ReservationsTests()
    {
        Directory.CreateDirectory(m_dossier);
    }

    private IDepotReservations CreerDepot(string format)
    {
        string chemin = Path.Combine(m_dossier, "reservations." + format);
        return format == "json" ? new DepotReservationsJson(chemin) : new DepotReservationsYaml(chemin);
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void ajouter_NouveauDepot_RelitValeursEtNouvellesInstances(string format)
    {
        // Arranger
        Reservation initiale = new(1, "Réunion");
        IDepotReservations premier = CreerDepot(format);
        // Agir
        premier.Ajouter(initiale);
        Reservation? relue = CreerDepot(format).Obtenir(1);
        // Assert
        Assert.NotNull(relue);
        Assert.Equal(initiale.Titre, relue.Titre);
        Assert.NotSame(initiale, relue);
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void creer_NumeroDejaPresent_RefuseSansModifierFichier(string format)
    {
        // Arranger
        ServiceReservations service = new(
            CreerDepot(format), NullLogger<ServiceReservations>.Instance);
        service.Creer(1, "Réunion");
        string chemin = Path.Combine(m_dossier, "reservations." + format);
        string avant = File.ReadAllText(chemin);
        // Agir
        Result<Reservation> resultat = service.Creer(1, "Autre titre");
        // Assert
        Assert.False(resultat.EstSucces);
        Assert.Equal(avant, File.ReadAllText(chemin));
    }

    [Theory]
    [InlineData("json", "")]
    [InlineData("json", "null")]
    [InlineData("json", "{ invalide")]
    [InlineData("json", "[null]")]
    [InlineData("json", "[{\"numero\":0,\"titre\":\"Réunion\"}]")]
    [InlineData("json", "[{\"numero\":1,\"titre\":\" \"}]")]
    [InlineData("json", "[{\"numero\":1,\"titre\":\"A\"},{\"numero\":1,\"titre\":\"B\"}]")]
    [InlineData("yaml", "")]
    [InlineData("yaml", "null")]
    [InlineData("yaml", "- null")]
    [InlineData("yaml", "[ invalide")]
    [InlineData("yaml", "- numero: 0\n  titre: A")]
    [InlineData("yaml", "- numero: 1\n  titre: ' '")]
    [InlineData("yaml", "- numero: 1\n  titre: A\n- numero: 1\n  titre: B")]
    public void ajouter_FichierInvalide_EchoueEtPreserveContenu(string format, string contenu)
    {
        // Arranger
        string chemin = Path.Combine(m_dossier, "reservations." + format);
        File.WriteAllText(chemin, contenu);
        IDepotReservations depot = CreerDepot(format);
        // Agir
        Exception? exception = Record.Exception(() => depot.Ajouter(new Reservation(2, "Atelier")));
        // Assert
        Assert.NotNull(exception);
        Assert.Equal(contenu, File.ReadAllText(chemin));
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void lire_AbsenceEtListeVide_RetourneCollectionVide(string format)
    {
        // Arranger
        IDepotReservations depot = CreerDepot(format);
        // Agir
        List<Reservation> absentes = depot.Lire();
        File.WriteAllText(Path.Combine(m_dossier, "reservations." + format), "[]");
        List<Reservation> vide = depot.Lire();
        // Assert
        Assert.Empty(absentes);
        Assert.Empty(vide);
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void lire_DossierAbsent_PropageIncident(string format)
    {
        // Arranger
        string chemin = Path.Combine(m_dossier, "inexistant", "donnees." + format);
        IDepotReservations depot = format == "json"
            ? new DepotReservationsJson(chemin) : new DepotReservationsYaml(chemin);
        // Agir
        Exception? exception = Record.Exception(() => depot.Lire());
        // Assert
        Assert.IsType<DirectoryNotFoundException>(exception);
    }

    [Fact]
    public void versEntites_DeuxApproches_ReconstruisentMemesValeurs()
    {
        // Arranger
        Reservation initiale = new(1, "Réunion");
        ConvertisseurReservations convertisseur = new();
        ReservationDonnees dto = new(initiale);
        // Agir
        Reservation directe = dto.VersEntite();
        Reservation externe = convertisseur.VersEntite(dto);
        List<Reservation> liste = convertisseur.VersEntites(new[] { dto });
        // Assert
        Assert.Equal("Réunion", directe.Titre);
        Assert.Equal(1, externe.Numero);
        Assert.Equal("Réunion", Assert.Single(liste).Titre);
        Assert.Throws<ArgumentNullException>(() => convertisseur.VersEntites(new ReservationDonnees?[] { null }));
    }

    public void Dispose()
    {
        Directory.Delete(m_dossier, true);
    }
}
