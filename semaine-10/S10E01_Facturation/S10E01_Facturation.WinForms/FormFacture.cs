using System.Globalization;
using Commandes.Presentation;

namespace S10E01_Facturation.WinForms;

public sealed class FormFacture : Form, IObservateurCommande
{
    private readonly Label m_resume = new();
    private readonly IDisposable? m_abonnement; // À compléter.

    public FormFacture(ModeleCommande modele)
    {
        ArgumentNullException.ThrowIfNull(modele);
        Text = "Facture de consultation";
        Name = "FormFacture";
        ClientSize = new Size(480, 280);
        MinimumSize = new Size(440, 280);
        AutoScaleMode = AutoScaleMode.Font;
        m_resume.Name = "lblResumeFacture";
        m_resume.Dock = DockStyle.Fill;
        m_resume.Padding = new Padding(20);
        m_resume.AccessibleName = "Résumé de facture";
        Controls.Add(m_resume);
        Actualiser(modele.Consulter());
        m_abonnement = null; // À compléter : conserver l’abonnement.
    }

    public void Actualiser(EtatCommandeDto etat)
    {
        ArgumentNullException.ThrowIfNull(etat);
        CultureInfo culture = CultureInfo.GetCultureInfo("fr-CA");
        m_resume.Text = $"Commande {etat.Facture.NumeroCommande}\n\n"
            + $"Sous-total : {etat.Facture.SousTotal.ToString("C", culture)}\n"
            + $"Frais pédagogiques (10 %) : {etat.Facture.Frais.ToString("C", culture)}\n"
            + $"Total : {etat.Facture.Total.ToString("C", culture)}\n\n"
            + (etat.Facture.EstProvisoire ? "Provisoire" : "Commande enregistrée");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            m_abonnement?.Dispose();
        }

        base.Dispose(disposing);
    }
}
