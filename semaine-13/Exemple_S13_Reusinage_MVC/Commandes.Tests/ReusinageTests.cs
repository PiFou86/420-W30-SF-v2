using System.Globalization;
using System.Text.Json;
using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Commandes.Presentation;
using Commandes.Presentation.WinForms;
using Xunit;

namespace Commandes.Tests;

public sealed class ReusinageTests
{
    [Theory]
    [InlineData(1, 30)]
    [InlineData(2, 0)]
    public void ConsulterTotal_CommandeConnue_ConserveLeTotal(int identifiant, int total)
    {
        ServiceCommandes service = CreerService(CheminExemple());
        Result<decimal> resultat = service.ConsulterTotal(identifiant);

        Assert.True(resultat.EstSucces);
        Assert.Equal((decimal)total, resultat.Valeur);
    }

    [Theory]
    [InlineData("fr-CA")]
    [InlineData("en-CA")]
    public void Consulter_MemeFichierEtCulture_ConserveLesTroisAffichages(string culture)
    {
        CultureInfo precedente = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            FormCommande depart = new FormCommande();
            VueEspionne vue = new VueEspionne();
            ControleurCommande controleur = CreerControleur(CheminExemple(), vue);
            foreach (int identifiant in new int[] { 1, 2, 99 })
            {
                string avant = depart.ConsulterTotal(identifiant, CheminExemple());
                controleur.Consulter(identifiant);
                ResultatConsultation notification = vue.Notifications[^1];
                string texte = notification.EstSucces
                    ? notification.Total!.Value.ToString("C2")
                    : notification.Raison!;
                Assert.Equal(avant, texte);
            }
            Assert.Equal(3, vue.Notifications.Count);
        }
        finally
        {
            CultureInfo.CurrentCulture = precedente;
        }
    }

    [Fact]
    public void ConsulterTotal_IdentifiantAbsent_RetourneUnRefus()
    {
        Result<decimal> resultat = CreerService(CheminExemple()).ConsulterTotal(99);
        Assert.False(resultat.EstSucces);
        Assert.Equal("Commande introuvable.", resultat.MessageErreur);
    }

    [Fact]
    public void Consulter_LectureDesTroisVersions_NeModifiePasLeFichier()
    {
        string chemin = CheminExemple();
        byte[] avant = File.ReadAllBytes(chemin);
        new FormCommande().ConsulterTotal(1, chemin);
        CreerService(chemin).ConsulterTotal(1);
        CreerControleur(chemin, new VueEspionne()).Consulter(1);
        Assert.Equal(avant, File.ReadAllBytes(chemin));
    }

    [Theory]
    [InlineData("{")]
    [InlineData("null")]
    public void Consulter_JsonInvalide_ConserveLIncidentSansAfficherUnTotal(string contenu)
    {
        string chemin = Path.GetTempFileName();
        try
        {
            File.WriteAllText(chemin, contenu);
            Assert.Throws<JsonException>(() => new FormCommande().ConsulterTotal(1, chemin));
            Assert.Throws<JsonException>(() => CreerService(chemin).ConsulterTotal(1));
            VueEspionne vue = new VueEspionne();
            CreerControleur(chemin, vue).Consulter(1);
            ResultatConsultation notification = Assert.Single(vue.Notifications);
            Assert.False(notification.EstSucces);
            Assert.Equal("Consultation impossible pour le moment.", notification.Raison);
            Assert.DoesNotContain(chemin, notification.Raison);
        }
        finally
        {
            File.Delete(chemin);
        }
    }

    [Fact]
    public void Consulter_FichierAbsent_ConserveLIncidentDeLecture()
    {
        string chemin = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "absent.json");
        Assert.Throws<DirectoryNotFoundException>(() => new FormCommande().ConsulterTotal(1, chemin));
        Assert.Throws<DirectoryNotFoundException>(() => CreerService(chemin).ConsulterTotal(1));
        VueEspionne vue = new VueEspionne();
        CreerControleur(chemin, vue).Consulter(1);
        ResultatConsultation notification = Assert.Single(vue.Notifications);
        Assert.False(notification.EstSucces);
        Assert.Equal("Consultation impossible pour le moment.", notification.Raison);
    }

    [Fact]
    public void Consulter_DepotSubstitue_FonctionneSansFenetreEtNActualiseQuUneFois()
    {
        DepotMemoire depot = new DepotMemoire();
        ServiceCommandes service = new ServiceCommandes(depot);
        ModeleConsultation modele = new ModeleConsultation(service);
        VueEspionne vue = new VueEspionne();
        VueEspionne secondeVue = new VueEspionne();
        using IDisposable abonnement = modele.Subscribe(vue);
        using IDisposable secondAbonnement = modele.Subscribe(secondeVue);
        ControleurCommande controleur = new ControleurCommande(modele);
        controleur.Consulter(7);
        Assert.Equal(7, depot.DernierIdentifiant);
        ResultatConsultation notification = Assert.Single(vue.Notifications);
        Assert.True(notification.EstSucces);
        Assert.Equal(8m, notification.Total);

        Assert.Same(notification, Assert.Single(secondeVue.Notifications));
        abonnement.Dispose();
        abonnement.Dispose();
        controleur.Consulter(7);
        Assert.Single(vue.Notifications);
        Assert.Equal(2, secondeVue.Notifications.Count);
    }

    [Fact]
    public void CalculerTotal_CopieDeLaListe_ProtegeLaCollection()
    {
        List<LigneCommande> lignes = new List<LigneCommande> { new LigneCommande(3m, 2) };
        Commande commande = new Commande(1, lignes);
        lignes.Clear();
        Assert.Equal(6m, commande.CalculerTotal());
    }

    private static string CheminExemple()
    {
        return Path.Combine(AppContext.BaseDirectory, "commandes.json");
    }

    private static ServiceCommandes CreerService(string chemin)
    {
        return new ServiceCommandes(new DepotCommandesJson(chemin));
    }

    private static ControleurCommande CreerControleur(string chemin, VueEspionne vue)
    {
        ModeleConsultation modele = new ModeleConsultation(CreerService(chemin));
        modele.Subscribe(vue);
        return new ControleurCommande(modele);
    }
}
