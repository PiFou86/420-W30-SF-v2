using System.Text.Json;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class CompatibiliteJsonTests
{
    [Fact]
    public void Depot_RelitAncienFormatEtConserveLeSchemaApresAjout()
    {
        string dossier = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(dossier);
        string chemin = Path.Combine(dossier, "commandes.json");
        try
        {
            // Document compatible avec l'ancienne version à records.
            string ancienJson = "[{\"Numero\":101,\"EstConfirmee\":true,\"Lignes\":[{\"NumeroProduit\":1,\"Nom\":\"Soupe\",\"Prix\":6.5,\"Quantite\":2}]}]";
            File.WriteAllText(chemin, ancienJson);
            DepotCommandesJson depot = new DepotCommandesJson(chemin);
            Commande? ancienne = depot.Obtenir(101);
            Assert.NotNull(ancienne);
            Assert.True(ancienne.EstConfirmee);
            Assert.Equal(13m, ancienne.Total);
            Assert.Equal(ancienJson, File.ReadAllText(chemin));

            Commande suivante = new Commande(102);
            suivante.Ajouter(new Produit(2, "Sandwich", 9m), 1);
            suivante.Confirmer();
            depot.Ajouter(suivante);

            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(chemin));
            JsonElement commande = document.RootElement[0];
            Assert.Equal(new[] { "Numero", "EstConfirmee", "Lignes" },
                commande.EnumerateObject().Select(propriete => propriete.Name).ToArray());
            Assert.Equal(new[] { "NumeroProduit", "Nom", "Prix", "Quantite" },
                commande.GetProperty("Lignes")[0].EnumerateObject().Select(propriete => propriete.Name).ToArray());

            DepotCommandesJson relecture = new DepotCommandesJson(chemin);
            Assert.Equal(13m, relecture.Obtenir(101)!.Total);
            Assert.Equal(9m, relecture.Obtenir(102)!.Total);
            Assert.NotSame(ancienne, relecture.Obtenir(101));
        }
        finally
        {
            Directory.Delete(dossier, true);
        }
    }
}
