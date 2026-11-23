namespace Commandes.Presentation;

internal sealed class AbonnementConsultation : IDisposable
{
    private List<IObserver<ResultatConsultation>>? m_observateurs;
    private IObserver<ResultatConsultation>? m_observateur;

    public AbonnementConsultation(
        List<IObserver<ResultatConsultation>> observateurs,
        IObserver<ResultatConsultation> observateur)
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
