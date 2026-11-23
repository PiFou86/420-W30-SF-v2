namespace Commandes.Presentation;

public sealed class ControleurCommande
{
    private readonly ModeleConsultation m_modele;

    public ControleurCommande(ModeleConsultation modele)
    {
        m_modele = modele;
    }

    public void Consulter(int identifiant)
    {
        m_modele.ConsulterTotal(identifiant);
    }
}
