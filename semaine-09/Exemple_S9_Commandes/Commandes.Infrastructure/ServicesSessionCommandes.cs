using Commandes.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Commandes.Infrastructure;

// Inscriptions communes à l'exécutable avec portées et à ses tests d'intégration.
// La présentation ajoute elle-même les formulaires et la fabrique Windows.
public static class ServicesSessionCommandes
{
    public static IServiceCollection AjouterSessionsCommandes(IServiceCollection services,
        IDepotCommandes depot, ICatalogueProduits catalogue, IJournalIncidents journal)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(depot);
        ArgumentNullException.ThrowIfNull(catalogue);
        ArgumentNullException.ThrowIfNull(journal);
        services.AddSingleton(depot);
        services.AddSingleton(catalogue);
        services.AddSingleton(journal);
        services.AddSingleton<ServiceConsultationCommandes>();
        services.AddScoped<ServiceCommandes>();
        services.AddScoped<TemoinPortee>();
        return services;
    }
}
