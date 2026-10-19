using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class DepotReservationsMemoireTests
{
    [Fact]
    public void Ajouter_PermetDeRetrouverReservation()
    {
        DepotReservationsMemoire depot = new();
        Reservation reservation = new(1, "Réunion");
        depot.Ajouter(reservation);
        Assert.Same(reservation, depot.Obtenir(1));
    }

    [Fact]
    public void Ajouter_RefuseDoublon()
    {
        DepotReservationsMemoire depot = new();
        depot.Ajouter(new Reservation(1, "Réunion"));
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(new Reservation(1, "Autre")));
    }

    [Fact]
    public void Ajouter_RefuseNull()
    {
        DepotReservationsMemoire depot = new();
        Assert.Throws<ArgumentNullException>(() => depot.Ajouter(null!));
    }

    [Fact]
    public void Obtenir_RetourneNullSiAbsent()
    {
        DepotReservationsMemoire depot = new();
        Assert.Null(depot.Obtenir(9));
    }

    [Fact]
    public void Obtenir_RefuseNumeroNonPositif()
    {
        DepotReservationsMemoire depot = new();
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(0));
    }

    [Fact]
    public void ObtenirToutes_ProtegeStructureInterne()
    {
        DepotReservationsMemoire depot = new();
        depot.Ajouter(new Reservation(1, "Réunion"));
        Reservation[] copie = Assert.IsType<Reservation[]>(depot.ObtenirToutes());
        copie[0] = new Reservation(2, "Autre");
        Assert.NotNull(depot.Obtenir(1));
        Assert.Null(depot.Obtenir(2));
    }
}
