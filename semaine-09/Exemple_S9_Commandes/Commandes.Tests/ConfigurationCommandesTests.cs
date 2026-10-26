using Commandes.Domaine;
using Commandes.Infrastructure;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Commandes.Tests;

public sealed class ConfigurationCommandesTests
{
    [Fact]
    public void SansConfiguration_ChoisitLeDepotMemoire()
    {
        using ConfigurationManager configuration = new ConfigurationManager();
        ConfigurationCommandes options = new ConfigurationCommandes(configuration, Path.GetTempPath());
        Assert.IsType<DepotCommandesMemoire>(options.CreerDepot());
    }

    [Fact]
    public void CheminsRelatifs_PartentDuDossierDeBasePourLesDonneesEtLeJournal()
    {
        string dossier = Path.Combine(Path.GetTempPath(), "s9-configuration-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            using ConfigurationManager configuration = new ConfigurationManager();
            configuration["Donnees:Format"] = "json";
            configuration["Donnees:Chemin"] = "commandes-test.json";
            configuration["Journal:Chemin"] = "journal-test.log";
            ConfigurationCommandes options = new ConfigurationCommandes(configuration, dossier);
            Commande commande = new Commande(1);
            options.CreerDepot().Ajouter(commande);
            options.CreerJournal().Consigner("test", new IOException("détail confidentiel simulé"));
            Assert.True(File.Exists(Path.Combine(dossier, "commandes-test.json")));
            string journal = File.ReadAllText(Path.Combine(dossier, "journal-test.log"));
            Assert.Contains(nameof(IOException), journal);
            Assert.DoesNotContain("confidentiel", journal);
        }
        finally
        {
            Directory.Delete(dossier, recursive: true);
        }
    }

    [Fact]
    public void CheminsIdentiques_SontRefuses()
    {
        using ConfigurationManager configuration = new ConfigurationManager();
        configuration["Donnees:Chemin"] = "commun.json";
        configuration["Journal:Chemin"] = "commun.json";
        Assert.Throws<InvalidOperationException>(() => new ConfigurationCommandes(configuration, Path.GetTempPath()));
    }

    [Fact]
    public void FormatInconnu_EstRefuse()
    {
        using ConfigurationManager configuration = new ConfigurationManager();
        configuration["Donnees:Format"] = "inconnu";
        ConfigurationCommandes options = new ConfigurationCommandes(configuration, Path.GetTempPath());
        Assert.Throws<InvalidOperationException>(() => options.CreerDepot());
    }
}
