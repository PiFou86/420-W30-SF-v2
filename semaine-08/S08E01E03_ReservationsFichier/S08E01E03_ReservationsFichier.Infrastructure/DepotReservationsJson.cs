using System.Text.Json;

using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

public sealed class DepotReservationsJson : IDepotReservations
{
    private readonly string m_chemin;
    private static readonly JsonSerializerOptions s_options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public DepotReservationsJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin = chemin;
    }

    public void Ajouter(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        List<Reservation> reservations = Lire();
        if (reservations.Any(item => item.Numero == reservation.Numero))
        {
            throw new InvalidOperationException("Numéro déjà présent.");
        }

        reservations.Add(reservation);
        ReservationDonnees[] donnees = reservations.Select(item => new ReservationDonnees(item)).ToArray();
        // E1-A : sérialiser donnees avec s_options et écrire dans m_chemin.
        throw new NotImplementedException("E1-A : écriture JSON à compléter.");
    }

    public Reservation? Obtenir(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numero));
        }

        return Lire().SingleOrDefault(item => item.Numero == numero);
    }

    public IReadOnlyCollection<Reservation> ObtenirToutes()
    {
        return Lire().ToArray();
    }

    private List<Reservation> Lire()
    {
        string json;
        try
        {
            json = File.ReadAllText(m_chemin);
        }
        catch (FileNotFoundException)
        {
            return new List<Reservation>();
        }

        // E1-B : désérialiser json avec s_options dans une liste de DTO.
        List<ReservationDonnees?>? donnees = null;
        return ReservationDonnees.Convertir(donnees);
    }
}
