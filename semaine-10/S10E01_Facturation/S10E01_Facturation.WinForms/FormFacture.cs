using Commandes.Presentation;

namespace S10E01_Facturation.WinForms;

public sealed partial class FormFacture : Form, IObserver<EtatCommandeDto>
{
    private IDisposable? m_abonnement;

    private readonly ModeleCommande? m_modele;

    // Constructeur réservé au designer.
    public FormFacture()
    {
        InitializeComponent();
    }

    public FormFacture(ModeleCommande modele) : this()
    {
        ArgumentNullException.ThrowIfNull(modele);
        m_modele = modele;
    }

    private void FormFacture_Load(object? sender, EventArgs e)
    {
        if (m_modele is null)
        {
            return; // Le constructeur sans modèle est réservé au designer.
        }

        AfficherEtat(m_modele.Consulter());
        // S10-A : conserver l’abonnement retourné par la source.
        m_abonnement = null; // À remplacer.
    }

    public void OnNext(EtatCommandeDto etat)
    {
        AfficherEtat(etat);
    }

    private void AfficherEtat(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        // S10-V : afficher les montants reçus et le statut, sans les recalculer.
        lResumeFacture.Text = "Affichage de la facture à compléter (S10-V).";
    }

    private void FormFacture_Disposed(object? sender, EventArgs e)
    {
        // S10-D : appeler la méthode SeDesabonner fournie.
        // À compléter : appeler la méthode fournie.
    }

    // Fourni : le nettoyage peut être demandé plusieurs fois.
    private void SeDesabonner()
    {
        if (m_abonnement is not null)
        {
            m_abonnement.Dispose();
            m_abonnement = null;
        }
    }

    // Fournis : une fin du flux est distincte d'un refus métier.
    public void OnError(Exception erreur)
    {
        ArgumentNullException.ThrowIfNull(erreur);
        SeDesabonner();
        lResumeFacture.Text = "Les notifications de la facture sont interrompues.";
    }

    public void OnCompleted()
    {
        SeDesabonner();
        lResumeFacture.Text += "\nNotifications terminées.";
    }

}
