namespace Contacts.Application;

public sealed class ConsulterContacts
{
    private readonly IDepotContacts m_depot;

    public ConsulterContacts(IDepotContacts depot)
    {
        ArgumentNullException.ThrowIfNull(depot);
        m_depot = depot;
    }

    public IReadOnlyCollection<ContactDto> ObtenirTous()
    {
        return m_depot.ObtenirTous().Select(contact => new ContactDto(contact)).ToArray();
    }

    public IReadOnlyCollection<ContactDto> Rechercher(string motClef)
    {
        ArgumentNullException.ThrowIfNull(motClef);
        return m_depot.Rechercher(motClef).Select(contact => new ContactDto(contact)).ToArray();
    }
}
