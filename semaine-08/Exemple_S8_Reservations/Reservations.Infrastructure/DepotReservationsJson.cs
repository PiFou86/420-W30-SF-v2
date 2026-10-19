using System.Text.Json;
using Reservations.Application;
using Reservations.Domaine;
namespace Reservations.Infrastructure;

public sealed class DepotReservationsJson : IDepotReservations
{
    private readonly string m_chemin_fichier;
    private static readonly JsonSerializerOptions s_options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public DepotReservationsJson(string chemin)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(chemin);
        m_chemin_fichier = chemin;
    }

    public List<Reservation> Lire()
    {
        string texte;
        try
        {
            texte = File.ReadAllText(m_chemin_fichier);
        }
        catch (FileNotFoundException)
        {
            return new List<Reservation>();
        }
        List<ReservationDonnees?>? donnees = JsonSerializer.Deserialize<List<ReservationDonnees?>>(texte, s_options);
        return ValidationReservations.Reconstruire(donnees);
    }

    public Reservation? Obtenir(int numero)
    {
        return Lire().SingleOrDefault(r => r.Numero == numero);
    }

    public void Ajouter(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        // Aucune écriture tant que la lecture et la validation n'ont pas réussi.
        List<Reservation> reservations = Lire();
        if (reservations.Any(r => r.Numero == reservation.Numero))
        {
            throw new InvalidOperationException("Le numéro existe déjà dans le dépôt.");
        }
        reservations.Add(reservation);
        ReservationDonnees[] donnees = reservations
            .Select(item => new ReservationDonnees(item)).ToArray();
        string texte = JsonSerializer.Serialize(donnees, s_options);
        if (!Directory.Exists(Path.GetDirectoryName(m_chemin_fichier)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(m_chemin_fichier)!);
        }
        File.WriteAllText(m_chemin_fichier, texte);
    }
}
