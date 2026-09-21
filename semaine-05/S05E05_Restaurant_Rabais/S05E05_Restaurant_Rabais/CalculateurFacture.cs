namespace Restaurant.Rabais;

public class CalculateurFacture
{
    public decimal CalculerTotal(decimal sousTotal, string typeRabais)
    {
        if (typeRabais == "fidelite")
        {
            return sousTotal * 0.90m;
        }

        if (typeRabais == "fixe")
        {
            return Math.Max(0m, sousTotal - 5m);
        }

        return sousTotal;
    }
}
