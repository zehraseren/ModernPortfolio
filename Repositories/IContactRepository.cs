using modernportfolio.Models;

namespace modernportfolio.Repositories;

public interface IContactRepository : IGenericRepository<Contact>
{
    Task<IEnumerable<Contact>> GetUnreadMessagesAsync();
}
