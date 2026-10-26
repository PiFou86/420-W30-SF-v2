using Commandes.Application;

namespace Commandes.Tests;

internal sealed class JournalSilencieux : IJournalIncidents
{
    public void Consigner(string operation, Exception exception)
    {
    }
}
