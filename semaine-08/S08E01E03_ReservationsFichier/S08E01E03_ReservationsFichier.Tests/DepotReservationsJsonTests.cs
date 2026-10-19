using System.Text.Json;
using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class DepotReservationsJsonTests : IDisposable
{
    private readonly string m_repertoire = Path.Combine(
        Path.GetTempPath(), $"reservations-test-{Guid.NewGuid():N}");

    private string Chemin => Path.Combine(m_repertoire, "reservations.json");

    public DepotReservationsJsonTests()
    {
        Directory.CreateDirectory(m_repertoire);
    }

    [Fact]
    public void Constructeur_RefuseCheminVide()
    {
        Assert.Throws<ArgumentException>(() => new DepotReservationsJson(" "));
    }

    [Fact]
    public void Ajouter_PersistePuisRelitReservation()
    {
        DepotReservationsJson depot = new(Chemin);
        depot.Ajouter(new Reservation(1, "Réunion"));

        DepotReservationsJson autreInstance = new(Chemin);
        Assert.Equal("Réunion", autreInstance.Obtenir(1)?.Titre);
    }

    [Fact]
    public void Obtenir_RetourneNullSiFichierAbsent()
    {
        DepotReservationsJson depot = new(Chemin);
        Assert.Null(depot.Obtenir(1));
        Assert.Empty(depot.ObtenirToutes());
    }

    [Fact]
    public void Ajouter_RefuseDoublon()
    {
        DepotReservationsJson depot = new(Chemin);
        depot.Ajouter(new Reservation(1, "Réunion"));
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(new Reservation(1, "Autre")));
    }

    [Fact]
    public void Obtenir_RefuseNumeroNonPositif()
    {
        DepotReservationsJson depot = new(Chemin);
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(0));
    }

    [Fact]
    public void Ajouter_RefuseNull()
    {
        DepotReservationsJson depot = new(Chemin);
        Assert.Throws<ArgumentNullException>(() => depot.Ajouter(null!));
    }

    [Fact]
    public void Obtenir_PropageContenuInvalide()
    {
        File.WriteAllText(Chemin, "{invalide");
        DepotReservationsJson depot = new(Chemin);
        Assert.Throws<JsonException>(() => depot.Obtenir(1));
    }

    public void Dispose()
    {
        Directory.Delete(m_repertoire, recursive: true);
    }
}
