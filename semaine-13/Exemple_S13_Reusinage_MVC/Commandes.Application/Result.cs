namespace Commandes.Application;

public class Result<T>
{
    public bool EstSucces { get; }
    public T? Valeur { get; }
    public string? MessageErreur { get; }

    private Result(bool estSucces, T? valeur, string? messageErreur)
    {
        EstSucces = estSucces;
        Valeur = valeur;
        MessageErreur = messageErreur;
    }

    public static Result<T> Succes(T valeur)
    {
        return new Result<T>(true, valeur, null);
    }

    public static Result<T> Echec(string messageErreur)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(messageErreur);

        return new Result<T>(false, default, messageErreur);
    }
}
