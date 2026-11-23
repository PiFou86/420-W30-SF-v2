namespace Commandes.Presentation;

public sealed class ResultatConsultation
{
    public bool EstSucces => Total.HasValue;
    public decimal? Total { get; }
    public string? Raison { get; }

    private ResultatConsultation(decimal? total, string? raison)
    {
        Total = total;
        Raison = raison;
    }

    public static ResultatConsultation Succes(decimal total)
    {
        return new ResultatConsultation(total, null);
    }

    public static ResultatConsultation Echec(string raison)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(raison);
        return new ResultatConsultation(null, raison);
    }
}
