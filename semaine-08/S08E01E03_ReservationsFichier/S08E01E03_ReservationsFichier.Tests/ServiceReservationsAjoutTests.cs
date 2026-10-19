using Reservations.Application;
using Reservations.Domaine;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ServiceReservationsAjoutTests
{
    [Fact]
    public void TenterAjouter_SuccesAjouteUneFois()
    {
        DepotObservateur depot = new();
        ServiceReservations service = new(depot);
        Result<ReservationDto> resultat = service.TenterAjouter(1, "Réunion");
        Assert.True(resultat.EstSucces);
        Assert.Equal("Réunion", resultat.Valeur.Titre);
        Assert.Equal(1, depot.NombreAjouts);
        Assert.Equal(1, depot.NombreLectures);
    }

    [Theory]
    [InlineData(0, "Réunion")]
    [InlineData(-1, "Réunion")]
    [InlineData(1, null)]
    [InlineData(1, "")]
    [InlineData(1, " ")]
    public void TenterAjouter_SaisieInvalideNeConsulteNiAjoute(int numero, string? titre)
    {
        DepotObservateur depot = new();
        Result<ReservationDto> resultat = new ServiceReservations(depot).TenterAjouter(numero, titre!);
        Assert.False(resultat.EstSucces);
        Assert.NotEmpty(resultat.MessageErreur);
        Assert.Equal(0, depot.NombreAjouts);
        Assert.Equal(0, depot.NombreLectures);
    }

    [Fact]
    public void TenterAjouter_DoublonEstRefusSansAjout()
    {
        DepotObservateur depot = new() { Existante = new Reservation(1, "Réunion") };
        Result<ReservationDto> resultat = new ServiceReservations(depot).TenterAjouter(1, "Autre");
        Assert.False(resultat.EstSucces);
        Assert.Equal(0, depot.NombreAjouts);
        Assert.Equal(1, depot.NombreLectures);
    }

    [Fact]
    public void TenterAjouter_IncidentLectureNeDevientPasRefusMetier()
    {
        IOException incident = new("incident simulé");
        DepotObservateur depot = new() { IncidentLecture = incident };
        Assert.Same(incident, Assert.Throws<IOException>(() => new ServiceReservations(depot).TenterAjouter(1, "A")));
        Assert.Equal(0, depot.NombreAjouts);
    }

    [Fact]
    public void TenterAjouter_IncidentEcritureNeDevientPasSucces()
    {
        IOException incident = new("incident simulé");
        DepotObservateur depot = new() { IncidentAjout = incident };
        Assert.Same(incident, Assert.Throws<IOException>(() => new ServiceReservations(depot).TenterAjouter(1, "A")));
    }

    private sealed class DepotObservateur : IDepotReservations
    {
        public int NombreAjouts { get; private set; }
        public int NombreLectures { get; private set; }
        public Reservation? Existante { get; init; }
        public IOException? IncidentLecture { get; init; }
        public IOException? IncidentAjout { get; init; }

        public void Ajouter(Reservation reservation)
        {
            if (IncidentAjout is not null)
            {
                throw IncidentAjout;
            }

            NombreAjouts++;
        }

        public Reservation? Obtenir(int numero)
        {
            NombreLectures++;
            if (IncidentLecture is not null)
            {
                throw IncidentLecture;
            }

            return Existante;
        }

        public IReadOnlyCollection<Reservation> ObtenirToutes()
        {
            throw new NotSupportedException("Le service n'utilise pas cette méthode dans ces tests.");
        }
    }
}
