using modernportfolio.Models;

namespace modernportfolio.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<IEnumerable<Project>> GetActiveProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int id);
    Task<int> CreateProjectAsync(Project project);
    Task<bool> UpdateProjectAsync(Project project);
    Task<bool> DeleteProjectAsync(int id);
}
