using modernportfolio.Models;

namespace modernportfolio.Repositories;

public class AboutRepository : GenericRepository<About>, IAboutRepository
{
    public AboutRepository(IConfiguration configuration) : base(configuration)
    {
    }
}
