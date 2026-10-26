namespace Commandes.Infrastructure;

internal sealed class CommandeDonnees
{
    public int Numero { get; }
    public bool EstConfirmee { get; }
    public List<LigneDonnees?>? Lignes { get; }

    public CommandeDonnees(int numero, bool estConfirmee, List<LigneDonnees?>? lignes)
    {
        Numero = numero;
        EstConfirmee = estConfirmee;
        Lignes = lignes;
    }
}
