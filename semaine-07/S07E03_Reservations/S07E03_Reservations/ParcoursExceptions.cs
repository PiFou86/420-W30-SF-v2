namespace Reservations;

public static class ParcoursExceptions
{
    // Accepte volontairement tout int pour observer l'appel invalide interne.
    public static IReadOnlyList<string> Tracer(int numero)
    {
        List<string> trace = new();
        try
        {
            trace.Add("début");
            Reservation reservation = new(numero, "Réunion");
            trace.Add("créée");
        }
        catch (ArgumentOutOfRangeException)
        {
            trace.Add("catch");
        }
        finally
        {
            trace.Add("finally");
        }

        trace.Add("suite");
        return trace.ToArray();
    }
}
