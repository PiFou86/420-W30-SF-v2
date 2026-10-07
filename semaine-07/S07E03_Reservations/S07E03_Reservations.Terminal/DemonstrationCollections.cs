namespace S07E03_Reservations.Terminal;

internal static class DemonstrationCollections
{
    public static void Executer()
    {
        List<int> numeros = new() { 17, 18, 17 };
        HashSet<int> uniques = new(numeros);
        Dictionary<int, string> titres = new()
        {
            [17] = "Réunion",
            [18] = "Atelier"
        };
        Queue<int> file = new(new[] { 17, 18 });
        Stack<int> pile = new(new[] { 17, 18 });

        Console.Out.WriteLine($"Liste : {string.Join(", ", numeros)}; {numeros.Count} éléments");
        Console.Out.WriteLine($"Ensemble : {uniques.Count} numéros distincts");
        Console.Out.WriteLine($"Numéro 17 : {titres[17]}");
        Console.Out.WriteLine($"File : {file.Dequeue()}");
        Console.Out.WriteLine($"Pile : {pile.Pop()}");
    }
}
