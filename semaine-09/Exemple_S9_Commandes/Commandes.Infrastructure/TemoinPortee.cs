using System.Diagnostics;

namespace Commandes.Infrastructure;

// Repère de démonstration : une instance par portée, libérée avec celle-ci.
// Cet identifiant appartient au témoin; le conteneur ne fournit pas d'identifiant public de portée.
public sealed class TemoinPortee : IDisposable
{
    public Guid Identifiant { get; } = Guid.NewGuid();
    public bool EstLibere { get; private set; }

    public TemoinPortee()
    {
        Debug.WriteLine($"[Portée {Identifiant}] Témoin créé.");
    }

    public void Dispose()
    {
        if (EstLibere)
        {
            return;
        }

        EstLibere = true;
        Debug.WriteLine($"[Portée {Identifiant}] Témoin libéré.");
    }
}
