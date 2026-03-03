using modernportfolio.Models;

namespace modernportfolio.Services;

public interface IContactService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<IEnumerable<Contact>> GetUnreadContactsAsync();
    Task<Contact?> GetContactByIdAsync(int id);
    Task<int> CreateContactAsync(Contact contact);
    Task<bool> MarkAsReadAsync(int id);
}
