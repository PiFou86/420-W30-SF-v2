using Reservations.Application;
using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ServiceReservationsTests
{
    [Fact]
    public void Constructeur_RefuseDepotNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ServiceReservations(null!));
    }

    [Fact]
    public void Obtenir_RefuseNumeroNonPositif()
    {
        ServiceReservations service = new(new DepotReservationsMemoire());
        Assert.Throws<ArgumentOutOfRangeException>(() => service.Obtenir(0));
    }

    [Fact]
    public void Obtenir_FonctionneAvecDepotMemoire()
    {
        DepotReservationsMemoire depot = new();
        depot.Ajouter(new Reservation(3, "Atelier"));
        ServiceReservations service = new(depot);
        Assert.Equal("Atelier", service.Obtenir(3)?.Titre);
    }

    [Fact]
    public void Obtenir_FonctionneAvecDepotJson()
    {
        string repertoire = Path.Combine(Path.GetTempPath(), $"service-reservations-{Guid.NewGuid():N}");
        Directory.CreateDirectory(repertoire);
        try
        {
            DepotReservationsJson depot = new(Path.Combine(repertoire, "reservations.json"));
            depot.Ajouter(new Reservation(3, "Atelier"));
            ServiceReservations service = new(depot);
            Assert.Equal("Atelier", service.Obtenir(3)?.Titre);
        }
        finally
        {
            Directory.Delete(repertoire, recursive: true);
        }
    }
}
