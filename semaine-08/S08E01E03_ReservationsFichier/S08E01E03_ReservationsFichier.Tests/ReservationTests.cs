using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ReservationTests
{
    [Fact]
    public void Constructeur_RefuseTitreNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Reservation(1, null!));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructeur_RefuseNumeroNonPositif(int numero)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Reservation(numero, "Réunion"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructeur_RefuseTitreBlanc(string titre)
    {
        Assert.Throws<ArgumentException>(() => new Reservation(1, titre));
    }
}
