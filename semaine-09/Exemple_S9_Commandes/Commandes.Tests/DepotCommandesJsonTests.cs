using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class DepotCommandesJsonTests
{
    [Fact]
    public void Enregistrer_EstReluAvecUnNouveauDepot()
    {
        string dossier = Path.Combine(Path.GetTempPath(), "preparation-s9-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            string chemin = Path.Combine(dossier, "commandes.json");
            ServiceCommandes service = new ServiceCommandes(new DepotCommandesJson(chemin), new CatalogueProduitsMemoire());
            Assert.True(service.Creer(1042).EstSucces);
            service.AjouterProduit(2, 2);
            service.Enregistrer();
            Commande? relue = new DepotCommandesJson(chemin).Obtenir(1042);
            Assert.NotNull(relue);
            Assert.Equal(16m, relue.Total);
            Assert.True(relue.EstConfirmee);
            Assert.False(new ServiceCommandes(new DepotCommandesJson(chemin), new CatalogueProduitsMemoire()).Creer(1042).EstSucces);
        }
        finally
        {
            Directory.Delete(dossier, recursive: true);
        }
    }
}
