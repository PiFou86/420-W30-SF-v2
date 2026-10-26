namespace Contacts.Application;

public interface IJournalIncidents
{
    void Consigner(string operation, Exception exception);
}
