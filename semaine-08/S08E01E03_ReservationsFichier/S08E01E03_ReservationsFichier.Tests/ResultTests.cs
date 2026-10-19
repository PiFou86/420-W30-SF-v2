using Reservations.Application;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Succes_ExposeValeurSansMessageErreur()
    {
        ReservationDto dto = new(1, "A");
        Result<ReservationDto> resultat = Result<ReservationDto>.Succes(dto);
        Assert.True(resultat.EstSucces);
        Assert.Same(dto, resultat.Valeur);
        Assert.Empty(resultat.MessageErreur);
    }

    [Fact]
    public void Succes_RefuseValeurNulle()
    {
        Assert.Throws<ArgumentNullException>(() => Result<ReservationDto>.Succes(null!));
    }

    [Fact]
    public void Echec_ExposeMessageMaisRefuseValeur()
    {
        Result<ReservationDto> resultat = Result<ReservationDto>.Echec("Refus attendu.");
        Assert.False(resultat.EstSucces);
        Assert.Equal("Refus attendu.", resultat.MessageErreur);
        Assert.Throws<InvalidOperationException>(() => resultat.Valeur);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Echec_RefuseMessageInvalide(string? message)
    {
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => Result<ReservationDto>.Echec(message!)));
    }
}
