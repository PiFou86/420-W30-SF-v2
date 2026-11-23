namespace Commandes.Fichiers;

internal sealed class CommandeFichier
{
    public int Identifiant { get; set; }
    public List<LigneCommandeFichier> Lignes { get; set; } = new List<LigneCommandeFichier>();
}
