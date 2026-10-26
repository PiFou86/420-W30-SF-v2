namespace Commandes.Portee.WinForms;

public sealed class CommandeListeViewModel
{
    public int Numero { get; }
    public int NombreLignes { get; }
    public decimal Total { get; }

    public CommandeListeViewModel(int numero, int nombreLignes, decimal total)
    {
        Numero = numero;
        NombreLignes = nombreLignes;
        Total = total;
    }
}
