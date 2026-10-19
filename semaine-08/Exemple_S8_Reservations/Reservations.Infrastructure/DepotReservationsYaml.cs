using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Reservations.Application;
using Reservations.Domaine;
namespace Reservations.Infrastructure;

public sealed class DepotReservationsYaml : IDepotReservations
{
    private readonly string m_chemin_fichier;
    private static readonly ISerializer s_serialiseur = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
    private static readonly IDeserializer s_deserialiseur = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

    public DepotReservationsYaml(string chemin)
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
        List<ReservationDonnees?>? donnees = s_deserialiseur.Deserialize<List<ReservationDonnees?>>(texte);
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
        string texte = s_serialiseur.Serialize(donnees);
        File.WriteAllText(m_chemin_fichier, texte);
    }
}
