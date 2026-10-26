using Commandes.Application;
using Commandes.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Commandes.Tests;

// Tests d'intégration du conteneur : les tests métier ordinaires continuent
// de construire leurs sujets directement. Les inscriptions sont celles de production.
public sealed class PorteesCommandesTests
{
    [Fact]
    public void DeuxPortees_PartagentLeDepotMaisPossedentDeuxBrouillons()
    {
        ServiceCollection services = new ServiceCollection();
        DepotCommandesMemoire depot = new DepotCommandesMemoire();
        ServicesSessionCommandes.AjouterSessionsCommandes(services, depot, new CatalogueProduitsMemoire(), new JournalSilencieux());
        using ServiceProvider fournisseur = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true, ValidateOnBuild = true
        });
        using IServiceScope premiere = fournisseur.CreateScope();
        using IServiceScope seconde = fournisseur.CreateScope();
        ServiceCommandes service1 = premiere.ServiceProvider.GetRequiredService<ServiceCommandes>();
        ServiceCommandes service2 = seconde.ServiceProvider.GetRequiredService<ServiceCommandes>();
        Assert.Same(service1, premiere.ServiceProvider.GetRequiredService<ServiceCommandes>());
        Assert.NotSame(service1, service2);
        Assert.Same(depot, premiere.ServiceProvider.GetRequiredService<IDepotCommandes>());
        Assert.Same(depot, seconde.ServiceProvider.GetRequiredService<IDepotCommandes>());
        Assert.Same(premiere.ServiceProvider.GetRequiredService<ICatalogueProduits>(),
            seconde.ServiceProvider.GetRequiredService<ICatalogueProduits>());
        Assert.Same(premiere.ServiceProvider.GetRequiredService<IJournalIncidents>(),
            seconde.ServiceProvider.GetRequiredService<IJournalIncidents>());
        service1.Creer(1042);
        service1.AjouterProduit(2, 2);
        service2.Creer(1043);
        Assert.Empty(service2.Consulter()!.Lignes);
        Assert.True(service1.Enregistrer().EstSucces);
        CommandeDto enregistree = Assert.Single(fournisseur.GetRequiredService<ServiceConsultationCommandes>().ConsulterEnregistrees());
        Assert.Equal(1042, enregistree.Numero);
        Assert.Equal(1043, service2.Consulter()!.Numero);
        Assert.Empty(service2.Consulter()!.Lignes);
    }

    [Fact]
    public void PorteeFermee_LibereLeTemoinEtLaSuivanteCommenceSansCommande()
    {
        ServiceCollection services = new ServiceCollection();
        ServicesSessionCommandes.AjouterSessionsCommandes(services, new DepotCommandesMemoire(), new CatalogueProduitsMemoire(), new JournalSilencieux());
        using ServiceProvider fournisseur = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        TemoinPortee temoin1;
        CommandeDto instantane;
        using (IServiceScope premiere = fournisseur.CreateScope())
        {
            temoin1 = premiere.ServiceProvider.GetRequiredService<TemoinPortee>();
            Assert.Same(temoin1, premiere.ServiceProvider.GetRequiredService<TemoinPortee>());
            ServiceCommandes service = premiere.ServiceProvider.GetRequiredService<ServiceCommandes>();
            service.Creer(1042);
            service.AjouterProduit(2, 2);
            instantane = service.Enregistrer().Valeur;
            Assert.False(temoin1.EstLibere);
        }

        Assert.True(temoin1.EstLibere);
        Assert.Equal(16m, instantane.Total);
        Assert.Single(fournisseur.GetRequiredService<ServiceConsultationCommandes>().ConsulterEnregistrees());
        using IServiceScope seconde = fournisseur.CreateScope();
        Assert.Null(seconde.ServiceProvider.GetRequiredService<ServiceCommandes>().Consulter());
        Assert.NotEqual(temoin1.Identifiant, seconde.ServiceProvider.GetRequiredService<TemoinPortee>().Identifiant);
    }

    [Fact]
    public void AbandonDuBrouillon_NAjouteRienAuDepotCommun()
    {
        ServiceCollection services = new ServiceCollection();
        ServicesSessionCommandes.AjouterSessionsCommandes(services, new DepotCommandesMemoire(), new CatalogueProduitsMemoire(), new JournalSilencieux());
        using ServiceProvider fournisseur = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        using (IServiceScope portee = fournisseur.CreateScope())
        {
            ServiceCommandes service = portee.ServiceProvider.GetRequiredService<ServiceCommandes>();
            service.Creer(1042);
            service.AjouterProduit(2, 2);
        }

        Assert.Empty(fournisseur.GetRequiredService<ServiceConsultationCommandes>().ConsulterEnregistrees());
    }

    [Fact]
    public void Validation_InterditLaResolutionDuServiceScopedDepuisLaRacine()
    {
        ServiceCollection services = new ServiceCollection();
        ServicesSessionCommandes.AjouterSessionsCommandes(services, new DepotCommandesMemoire(), new CatalogueProduitsMemoire(), new JournalSilencieux());
        using ServiceProvider fournisseur = services.BuildServiceProvider(new ServiceProviderOptions { ValidateScopes = true });
        Assert.Throws<InvalidOperationException>(() => fournisseur.GetRequiredService<ServiceCommandes>());
    }
}
