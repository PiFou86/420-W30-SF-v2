using System.Diagnostics;
using System.Text.Json;
using Commandes.Application;

namespace Commandes.Presentation;

public sealed class ModeleConsultation : IObservable<ResultatConsultation>
{
    private readonly ServiceCommandes m_service;
    private readonly List<IObserver<ResultatConsultation>> m_observateurs =
        new List<IObserver<ResultatConsultation>>();

    public ModeleConsultation(ServiceCommandes service)
    {
        m_service = service;
    }

    public IDisposable Subscribe(IObserver<ResultatConsultation> observateur)
    {
        ArgumentNullException.ThrowIfNull(observateur);
        if (m_observateurs.Any(item => ReferenceEquals(item, observateur)))
        {
            throw new InvalidOperationException("Cet observateur est déjà abonné.");
        }

        m_observateurs.Add(observateur);
        return new AbonnementConsultation(m_observateurs, observateur);
    }

    public void ConsulterTotal(int identifiant)
    {
        ResultatConsultation resultat;
        try
        {
            Result<decimal> consultation = m_service.ConsulterTotal(identifiant);
            resultat = consultation.EstSucces
                ? ResultatConsultation.Succes(consultation.Valeur)
                : ResultatConsultation.Echec(consultation.MessageErreur!);
        }
        catch (IOException exception)
        {
            resultat = ProduireIncident(exception);
        }
        catch (UnauthorizedAccessException exception)
        {
            resultat = ProduireIncident(exception);
        }
        catch (JsonException exception)
        {
            resultat = ProduireIncident(exception);
        }

        foreach (IObserver<ResultatConsultation> observateur in m_observateurs.ToArray())
        {
            observateur.OnNext(resultat);
        }
    }

    private static ResultatConsultation ProduireIncident(Exception exception)
    {
        Trace.TraceError(exception.ToString());
        return ResultatConsultation.Echec("Consultation impossible pour le moment.");
    }
}
