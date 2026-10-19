using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class ReservationIntrouvableExceptionTests
{
    [Fact]
    public void Constructeur_ConserveNumero()
    {
        ReservationIntrouvableException exception = new(17);
        Assert.Equal(17, exception.Numero);
    }

    [Fact]
    public void Constructeur_RefuseNumeroNonPositif()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new ReservationIntrouvableException(0));
    }
}
