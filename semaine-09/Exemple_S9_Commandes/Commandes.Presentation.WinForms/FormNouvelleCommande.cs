using System.ComponentModel;

namespace Commandes.Presentation.WinForms;

public partial class FormNouvelleCommande : Form
{
    private readonly int m_numeroPropose = 1042;

    // Propriété ordinaire de cette instance. Null tant que la saisie n'est pas validée.
    // Ce nom suit la convention de préparation du cours; ce n'est pas Result<T>.
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SaisieNouvelleCommande? Result { get; private set; }

    public FormNouvelleCommande()
    {
        InitializeComponent();
    }

    public FormNouvelleCommande(int numeroPropose) : this()
    {
        if (numeroPropose <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numeroPropose));
        }

        m_numeroPropose = numeroPropose;
        // Point d'arrêt 1 : l'objet existe, mais aucune fenêtre n'est affichée.
    }

    private void FormNouvelleCommande_Load(object? sender, EventArgs e)
    {
        // Point d'arrêt 2 : les paramètres de l'objet préparent la vue avant l'affichage.
        nudNumero.Value = m_numeroPropose;
        Result = null;
    }

    private void FormNouvelleCommande_Shown(object? sender, EventArgs e)
    {
        // Point d'arrêt 3 : la fenêtre est maintenant affichée.
        nudNumero.Focus();
        nudNumero.Select(0, nudNumero.Text.Length);
    }

    private void bValider_Click(object? sender, EventArgs e)
    {
        int numero = decimal.ToInt32(nudNumero.Value);
        // La propriété reçoit un objet de données avant la fin du dialogue.
        // Les refus métier (par exemple un numéro déjà enregistré) restent dans Application.
        Result = new SaisieNouvelleCommande(numero);
        DialogResult = DialogResult.OK;
    }
}
