using System.Text.Json;
using Reservations.Application;
using Reservations.Domaine;
using Reservations.Infrastructure;
using Xunit;
using YamlDotNet.Core;

namespace S08E01E03_ReservationsFichier.Tests;

public sealed class DepotsFichierContratTests : IDisposable
{
    private readonly string m_repertoire = Path.Combine(Path.GetTempPath(), $"contrats-depot-{Guid.NewGuid():N}");
    private string Chemin => Path.Combine(m_repertoire, "donnees");

    public DepotsFichierContratTests()
    {
        Directory.CreateDirectory(m_repertoire);
    }

    private IDepotReservations Creer(string format)
    {
        return format == "json" ? new DepotReservationsJson(Chemin) : new DepotReservationsYaml(Chemin);
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void Ajouter_NouvelleInstanceRelitEtRechercheAbsence(string format)
    {
        Creer(format).Ajouter(new Reservation(1, "Réunion"));
        IDepotReservations autre = Creer(format);
        Assert.Equal("Réunion", autre.Obtenir(1)?.Titre);
        Assert.Null(autre.Obtenir(2));
        Assert.Single(autre.ObtenirToutes());
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void FichierAbsent_EstVideSansCreation(string format)
    {
        Assert.Empty(Creer(format).ObtenirToutes());
        Assert.Null(Creer(format).Obtenir(1));
        Assert.False(File.Exists(Chemin));
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void ListeVide_EstValide(string format)
    {
        File.WriteAllText(Chemin, "[]");
        Assert.Empty(Creer(format).ObtenirToutes());
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void Ajouter_DoublonRefuseSansModifierFichier(string format)
    {
        IDepotReservations depot = Creer(format);
        depot.Ajouter(new Reservation(1, "Réunion"));
        string avant = File.ReadAllText(Chemin);
        Assert.Throws<InvalidOperationException>(() => depot.Ajouter(new Reservation(1, "Autre")));
        Assert.Equal(avant, File.ReadAllText(Chemin));
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void Preconditions_RefusentNullEtNumeroNonPositif(string format)
    {
        IDepotReservations depot = Creer(format);
        Assert.Throws<ArgumentNullException>(() => depot.Ajouter(null!));
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(0));
        Assert.Throws<ArgumentOutOfRangeException>(() => depot.Obtenir(-1));
    }

    [Theory]
    [InlineData("json", "")]
    [InlineData("json", " ")]
    [InlineData("json", null)]
    [InlineData("yaml", "")]
    [InlineData("yaml", " ")]
    [InlineData("yaml", null)]
    public void Constructeur_RefuseCheminInvalide(string format, string? chemin)
    {
        Exception? erreur = Record.Exception(() =>
        {
            if (format == "json")
            {
                _ = new DepotReservationsJson(chemin!);
            }
            else
            {
                _ = new DepotReservationsYaml(chemin!);
            }
        });
        Assert.IsAssignableFrom<ArgumentException>(erreur);
    }

    [Theory]
    [InlineData("json", "null")]
    [InlineData("yaml", "null")]
    [InlineData("json", "[null]")]
    [InlineData("yaml", "- null")]
    [InlineData("json", "[{\"numero\":0,\"titre\":\"A\"}]")]
    [InlineData("yaml", "- numero: 0\n  titre: A")]
    [InlineData("json", "[{\"numero\":1,\"titre\":null}]")]
    [InlineData("yaml", "- numero: 1\n  titre: null")]
    [InlineData("json", "[{\"numero\":1,\"titre\":\" \"}]")]
    [InlineData("yaml", "- numero: 1\n  titre: ' '")]
    [InlineData("json", "[{\"numero\":1,\"titre\":\"A\"},{\"numero\":1,\"titre\":\"B\"}]")]
    [InlineData("yaml", "- numero: 1\n  titre: A\n- numero: 1\n  titre: B")]
    public void DonneesInvalides_RefusentLectureEtAjoutSansEcraser(string format, string contenu)
    {
        File.WriteAllText(Chemin, contenu);
        Assert.Throws<InvalidDataException>(() => Creer(format).ObtenirToutes());
        Assert.Throws<InvalidDataException>(() => Creer(format).Ajouter(new Reservation(2, "Valide")));
        Assert.Equal(contenu, File.ReadAllText(Chemin));
    }

    [Theory]
    [InlineData("json", "[{\"numero\":1,\"titre\":\"A\"},{\"numero\":0,\"titre\":\"B\"}]")]
    [InlineData("yaml", "- numero: 1\n  titre: A\n- numero: 0\n  titre: B")]
    public void Obtenir_ValideToutLeDocumentAvantRecherche(string format, string contenu)
    {
        File.WriteAllText(Chemin, contenu);
        Assert.Throws<InvalidDataException>(() => Creer(format).Obtenir(1));
    }

    [Theory]
    [InlineData("json", "{invalide")]
    [InlineData("yaml", "- numero: [")]
    public void SyntaxeInvalide_ConserveExceptionTechniqueEtFichier(string format, string contenu)
    {
        File.WriteAllText(Chemin, contenu);
        Exception? erreur = Record.Exception(() => Creer(format).Ajouter(new Reservation(1, "A")));
        if (format == "json")
        {
            Assert.IsType<JsonException>(erreur);
        }
        else
        {
            Assert.IsAssignableFrom<YamlException>(erreur);
        }

        Assert.Equal(contenu, File.ReadAllText(Chemin));
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void DossierManquant_NeDevientPasDepotVide(string format)
    {
        Directory.Delete(m_repertoire);
        Assert.Throws<DirectoryNotFoundException>(() => Creer(format).ObtenirToutes());
    }

    [Theory]
    [InlineData("json")]
    [InlineData("yaml")]
    public void ObtenirToutes_ModifierCopieNeModifiePasFichier(string format)
    {
        IDepotReservations depot = Creer(format);
        depot.Ajouter(new Reservation(1, "Réunion"));
        Reservation[] copie = Assert.IsType<Reservation[]>(depot.ObtenirToutes());
        copie[0] = new Reservation(2, "Autre");
        Assert.NotNull(depot.Obtenir(1));
        Assert.Null(depot.Obtenir(2));
    }

    public void Dispose()
    {
        if (Directory.Exists(m_repertoire))
        {
            Directory.Delete(m_repertoire, recursive: true);
        }
    }
}
