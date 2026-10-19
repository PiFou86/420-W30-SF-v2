using Reservations.Application;

namespace S08E01E03_ReservationsFichier.Terminal;

public sealed class ReservationViewModel
{
    public string Libelle { get; }
    public string EtatAffiche { get; }

    public ReservationViewModel(ReservationDto reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        Libelle = $"Réservation {reservation.Numero} — {reservation.Titre}";
        EtatAffiche = "Enregistrée";
    }
}
