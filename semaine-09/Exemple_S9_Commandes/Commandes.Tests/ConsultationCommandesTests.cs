using Commandes.Application;
using Commandes.Domaine;
using Commandes.Infrastructure;
using Xunit;

namespace Commandes.Tests;

public sealed class ConsultationCommandesTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ObtenirToutes_ListeVidePuisCopiesTrieesEtRelecture(bool json)
    {
        string dossier = Path.Combine(Path.GetTempPath(), "s9-consultation-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            string chemin = Path.Combine(dossier, "commandes.json");
            IDepotCommandes depot = json ? new DepotCommandesJson(chemin) : new DepotCommandesMemoire();
            Assert.Empty(depot.ObtenirToutes());
            Commande originale = new Commande(1043);
            originale.Ajouter(new Produit(2, "Sandwich", 8m), 1);
            depot.Ajouter(originale);
            depot.Ajouter(new Commande(1042));
            originale.Ajouter(new Produit(2, "Sandwich", 8m), 1);

            Commande[] premiereLecture = depot.ObtenirToutes().ToArray();
            Assert.Equal(new[] { 1042, 1043 }, premiereLecture.Select(commande => commande.Numero));
            premiereLecture[1].Ajouter(new Produit(2, "Sandwich", 8m), 2);
            IDepotCommandes nouveauLecteur = json ? new DepotCommandesJson(chemin) : depot;
            Commande[] secondeLecture = nouveauLecteur.ObtenirToutes().ToArray();
            Assert.Equal(8m, secondeLecture[1].Total);
            Assert.NotSame(premiereLecture[1], secondeLecture[1]);
        }
        finally
        {
            Directory.Delete(dossier, recursive: true);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Consultation_RetourneSeulementLesEnregistreesSousFormeDInstantanes(bool json)
    {
        string dossier = Path.Combine(Path.GetTempPath(), "s9-liste-" + Guid.NewGuid());
        Directory.CreateDirectory(dossier);
        try
        {
            IDepotCommandes depot = json
                ? new DepotCommandesJson(Path.Combine(dossier, "commandes.json"))
                : new DepotCommandesMemoire();
            ServiceConsultationCommandes consultation = new ServiceConsultationCommandes(depot);
            Assert.Empty(consultation.ConsulterEnregistrees());
            depot.Ajouter(new Commande(1041));
            ServiceCommandes saisie = new ServiceCommandes(depot, new CatalogueProduitsMemoire());
            saisie.Creer(1043);
            saisie.AjouterProduit(2, 2);
            saisie.Enregistrer();
            IReadOnlyCollection<CommandeDto> avant = consultation.ConsulterEnregistrees();
            Assert.Equal(1043, Assert.Single(avant).Numero);
            Assert.Equal(16m, avant.Single().Total);
            saisie.Creer(1042);
            saisie.AjouterProduit(1, 1);
            saisie.Enregistrer();
            Assert.Single(avant);
            Assert.Equal(new[] { 1042, 1043 }, consultation.ConsulterEnregistrees().Select(commande => commande.Numero));
        }
        finally
        {
            Directory.Delete(dossier, recursive: true);
        }
    }

    [Fact]
    public void Consultation_IncidentLectureEstPropageEtNonTransformeEnListeVide()
    {
        ServiceConsultationCommandes consultation = new ServiceConsultationCommandes(new DepotIncident { EchouerLecture = true });
        Assert.Throws<IOException>(() => consultation.ConsulterEnregistrees());
    }

    [Fact]
    public void Consultation_DepotNulEstRefuse()
    {
        Assert.Throws<ArgumentNullException>(() => new ServiceConsultationCommandes(null!));
    }
}
