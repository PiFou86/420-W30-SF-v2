using Reservations.Application;
using Reservations.Domaine;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ReservationDtoTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ConstructeurValeurs_RefuseNumeroNonPositif(int numero)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ReservationDto(numero, "A"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ConstructeurValeurs_RefuseTitreInvalide(string? titre)
    {
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => new ReservationDto(1, titre!)));
    }

    [Fact]
    public void Constructeur_CopieLesDonneesUtiles()
    {
        ReservationDto dto = new(new Reservation(2, "Atelier"));
        Assert.Equal(2, dto.Numero);
        Assert.Equal("Atelier", dto.Titre);
    }

    [Fact]
    public void Constructeur_RefuseNull()
    {
        Assert.Throws<ArgumentNullException>(() => new ReservationDto(null!));
    }
}
