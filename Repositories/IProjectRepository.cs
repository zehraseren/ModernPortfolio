using modernportfolio.Models;

namespace modernportfolio.Repositories;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<IEnumerable<Project>> GetActiveProjectsAsync();
}
