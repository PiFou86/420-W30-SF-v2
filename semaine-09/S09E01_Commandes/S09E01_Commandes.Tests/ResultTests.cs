using Commandes.Application;
using Commandes.Domaine;
using Xunit;

namespace Commandes.Tests;

public sealed class ResultTests
{
    [Fact]
    public void Succes_ExposeValeurSansMessageErreur()
    {
        CommandeDto dto = new(new Commande(1));
        Result<CommandeDto> resultat = Result<CommandeDto>.Succes(dto);
        Assert.True(resultat.EstSucces);
        Assert.Same(dto, resultat.Valeur);
        Assert.Empty(resultat.MessageErreur);
    }

    [Fact]
    public void Succes_RefuseValeurNulle()
    {
        Assert.Throws<ArgumentNullException>(() => Result<CommandeDto>.Succes(null!));
    }

    [Fact]
    public void Echec_ExposeMessageMaisRefuseValeur()
    {
        Result<CommandeDto> resultat = Result<CommandeDto>.Echec("Refus attendu.");
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
        Assert.IsAssignableFrom<ArgumentException>(Record.Exception(() => Result<CommandeDto>.Echec(message!)));
    }
}
