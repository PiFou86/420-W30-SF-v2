using System.Globalization;
using Commandes.Presentation.DotNet;

namespace Demo.WinForms;

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
        m_abonnement = m_modele.Subscribe(this);
    }

    public void OnNext(EtatCommandeDto etat)
    {
        AfficherEtat(etat);
    }

    private void AfficherEtat(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        CultureInfo culture = CultureInfo.GetCultureInfo("fr-CA");
        string statut = "Provisoire";
        if (!etat.Facture.EstProvisoire)
        {
            statut = "Commande enregistrée";
        }

        lResumeFacture.Text = $"Commande {etat.Facture.NumeroCommande}\n\n"
            + $"Sous-total : {etat.Facture.SousTotal.ToString("C", culture)}\n"
            + $"Frais de service fictifs (10 %) : {etat.Facture.Frais.ToString("C", culture)}\n"
            + $"Total : {etat.Facture.Total.ToString("C", culture)}\n\n"
            + statut;
    }

    private void FormFacture_Disposed(object? sender, EventArgs e)
    {
        SeDesabonner();
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
