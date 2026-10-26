namespace Commandes.Application;

// Mécanisme déjà introduit : refus attendu distinct de l'incident technique.
public sealed class Result<T>
{
    private readonly T m_valeur;
    public bool EstSucces { get; }
    public string MessageErreur { get; }

    public T Valeur
    {
        get
        {
            if (!EstSucces)
            {
                throw new InvalidOperationException("Un échec ne porte aucune valeur.");
            }

            return m_valeur;
        }
    }

    private Result(bool estSucces, T valeur, string messageErreur)
    {
        EstSucces = estSucces;
        m_valeur = valeur;
        MessageErreur = messageErreur;
    }

    public static Result<T> Succes(T valeur)
    {
        ArgumentNullException.ThrowIfNull(valeur);
        return new Result<T>(true, valeur, string.Empty);
    }

    public static Result<T> Echec(string messageErreur)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageErreur);
        return new Result<T>(false, default!, messageErreur);
    }
}
