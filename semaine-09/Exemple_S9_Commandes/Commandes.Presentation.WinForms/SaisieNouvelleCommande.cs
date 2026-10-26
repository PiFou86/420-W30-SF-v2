namespace Commandes.Presentation.WinForms;

// Objet de présentation : les données saisies, sans référence à un contrôle.
public sealed class SaisieNouvelleCommande
{
    public int Numero { get; }

    public SaisieNouvelleCommande(int numero)
    {
        Numero = numero;
    }
}
