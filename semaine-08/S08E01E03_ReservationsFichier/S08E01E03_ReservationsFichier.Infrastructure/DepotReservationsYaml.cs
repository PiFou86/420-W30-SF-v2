using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using Reservations.Application;
using Reservations.Domaine;

namespace Reservations.Infrastructure;

public sealed class DepotReservationsYaml : IDepotReservations
{
    private readonly string m_chemin;

    public DepotReservationsYaml(string chemin)
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
        ISerializer serialiseur = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        ReservationDonnees[] donnees = reservations.Select(item => new ReservationDonnees(item)).ToArray();
        File.WriteAllText(m_chemin, serialiseur.Serialize(donnees));
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
        string yaml;
        try
        {
            yaml = File.ReadAllText(m_chemin);
        }
        catch (FileNotFoundException)
        {
            return new List<Reservation>();
        }

        IDeserializer deserialiseur = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        List<ReservationDonnees?>? donnees = deserialiseur.Deserialize<List<ReservationDonnees?>>(yaml);
        return ReservationDonnees.Convertir(donnees);
    }
}
