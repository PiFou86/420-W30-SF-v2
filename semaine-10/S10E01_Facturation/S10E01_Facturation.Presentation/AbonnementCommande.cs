namespace Commandes.Presentation;

internal sealed class AbonnementCommande : IDisposable
{
    private List<IObserver<EtatCommandeDto>>? m_observateurs;
    private IObserver<EtatCommandeDto>? m_observateur;

    public AbonnementCommande(List<IObserver<EtatCommandeDto>> observateurs,
        IObserver<EtatCommandeDto> observateur)
    {
        m_observateurs = observateurs;
        m_observateur = observateur;
    }

    public void Dispose()
    {
        if (m_observateurs is not null)
        {
            int index = m_observateurs.FindIndex(item => ReferenceEquals(item, m_observateur));
            if (index >= 0)
            {
                m_observateurs.RemoveAt(index);
            }

            m_observateurs = null;
            m_observateur = null;
        }
    }
}
