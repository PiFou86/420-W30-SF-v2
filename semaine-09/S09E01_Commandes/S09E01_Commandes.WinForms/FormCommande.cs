using Commandes.Application;

namespace S09E01_Commandes.WinForms;

public partial class FormCommande : Form
{
    private readonly ServiceCommandes? m_service;
    private readonly IJournalIncidents? m_journal;

    public FormCommande()
    {
        InitializeComponent();
    }

    public FormCommande(ServiceCommandes service, IJournalIncidents journal) : this()
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(journal);
        m_service = service;
        m_journal = journal;
        // TODO : charger les produits, afficher le DTO et relier la présentation.
    }
}
