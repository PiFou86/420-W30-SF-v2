using Reservations.Application;
using S08E01E03_ReservationsFichier.Terminal;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ReservationViewModelTests
{
    [Fact]
    public void Constructeur_FormateLibellePourLaVue()
    {
        ReservationViewModel modele = new(new ReservationDto(2, "Atelier"));
        Assert.Contains("Atelier", modele.Libelle);
        Assert.Equal("Enregistrée", modele.EtatAffiche);
    }

    [Fact]
    public void Constructeur_RefuseNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ReservationViewModel(null!));
    }
}
