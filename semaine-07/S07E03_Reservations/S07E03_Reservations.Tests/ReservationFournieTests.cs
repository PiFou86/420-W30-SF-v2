using Reservations;
using Xunit;

namespace S07E03_Reservations.Tests;

public sealed class ReservationFournieTests
{
    [Fact]
    public void Constructeur_ValeursValides_ValeursConservees()
    {
        Reservation reservation = new(17, "Réunion");
        Assert.Equal(17, reservation.Numero);
        Assert.Equal("Réunion", reservation.Titre);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructeur_NumeroInvalide_ExceptionAvecParametre(int numero)
    {
        ArgumentOutOfRangeException erreur = Assert.Throws<ArgumentOutOfRangeException>(() => new Reservation(numero, "Réunion"));
        Assert.Equal("numero", erreur.ParamName);
    }

    [Fact]
    public void Constructeur_TitreNull_ExceptionAvecParametre()
    {
        ArgumentNullException erreur = Assert.Throws<ArgumentNullException>(() => new Reservation(17, null!));
        Assert.Equal("titre", erreur.ParamName);
    }
}
