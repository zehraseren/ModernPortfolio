using modernportfolio.Models;
using modernportfolio.Repositories;

namespace modernportfolio.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _repository;

    public ProjectService(IProjectRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> CreateProjectAsync(Project project)
    {
        if (project is null) throw new ArgumentNullException("Project cannot be null!", nameof(project));

        ValidateProject(project);
        project.CreatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(project.ProjectUrl)) project.ProjectUrl = NormalizeUrl(project.ProjectUrl);
        if (!string.IsNullOrWhiteSpace(project.GithubUrl)) project.GithubUrl = NormalizeUrl(project.GithubUrl);
        var result = await _repository.CreateAsync(project);
        return result;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("Project ID must be greater than zero!", nameof(id));
        var project = await _repository.GetByIdAsync(id);
        if (project is null) return false;
        var result = await _repository.DeleteAsync(id);
        return result;
    }

    public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
    {
        var projects = await _repository.GetActiveProjectsAsync();
        var result = projects.OrderByDescending(p => p.CreatedAt);
        return result;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var projects = await _repository.GetAllAsync();
        var result = projects.OrderByDescending(p => p.CreatedAt);
        return result;
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("ID must be greater than zero.", nameof(id));

        var result = await _repository.GetByIdAsync(id);
        return result;
    }

    public async Task<bool> UpdateProjectAsync(Project project)
    {
        if (project is null) throw new ArgumentNullException("Project cannot be null!", nameof(project));
        if (project.Id <= 0) throw new ArgumentException("Project ID must be greater than zero!", nameof(project));

        var existingProject = await _repository.GetByIdAsync(project.Id);
        if (existingProject is null) throw new InvalidOperationException($"Project with ID {project.Id} does not exist!");
        ValidateProject(project);
        if (!string.IsNullOrWhiteSpace(project.ProjectUrl)) project.ProjectUrl = NormalizeUrl(project.ProjectUrl);
        if (!string.IsNullOrWhiteSpace(project.GithubUrl)) project.GithubUrl = NormalizeUrl(project.GithubUrl);
        project.CreatedAt = existingProject.CreatedAt; // Preserve original creation date
        var result = await _repository.UpdateAsync(project);
        return result;
    }

    private void ValidateProject(Project project)
    {
        // Title
        if (string.IsNullOrWhiteSpace(project.Title))
            throw new ArgumentException("Project title cannot be empty or whitespace!", nameof(project));

        // Title length
        if (project.Title.Length > 200)
            throw new ArgumentException("Project title cannot exceed 200 characters!", nameof(project));

        // Description
        if (string.IsNullOrEmpty(project.Description))
            throw new ArgumentException("Project description cannot be empty or whitespace!", nameof(project));

        // ImageUrl
        if (!string.IsNullOrEmpty(project.ImageUrl) && project.ImageUrl.Length > 500)
            throw new ArgumentException("Project image URL cannot exceed 500 characters!", nameof(project));

        // Project URL
        if (!string.IsNullOrWhiteSpace(project.ProjectUrl) && !IsValidUrl(project.ProjectUrl))
            throw new ArgumentException("Project URL format is invalid!", nameof(project));

        // Github URL
        if (!string.IsNullOrWhiteSpace(project.GithubUrl) && !IsValidUrl(project.GithubUrl))
            throw new ArgumentException("Github URL format is invalid!", nameof(project));

        // Project URL Length
        if (!string.IsNullOrWhiteSpace(project.ProjectUrl) && project.ProjectUrl.Length > 500)
            throw new ArgumentException("Project URL cannot exceed 500 characters!", nameof(project));

        // Github URL Length
        if (!string.IsNullOrWhiteSpace(project.GithubUrl) && project.GithubUrl.Length > 500)
            throw new ArgumentException("Github URL cannot exceed 500 characters!", nameof(project));
    }

    private bool IsValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return false;

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    private string NormalizeUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) return url;

        url = url.Trim();
        if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            url = "https://" + url;
        }
        return url;
    }
}
