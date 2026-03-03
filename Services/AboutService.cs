using modernportfolio.Models;
using modernportfolio.Repositories;

namespace modernportfolio.Services;

public class AboutService : IAboutService
{
    private readonly IAboutRepository _repository;

    public AboutService(IAboutRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> CreateAboutAsync(About about)
    {
        if (about is null) throw new ArgumentNullException("About cannot be null!", nameof(about));
        ValidateAbout(about);
        var existingAbout = await GetAboutAsync();
        if (existingAbout is not null) throw new InvalidOperationException("An About entry already exists. Only one entry is allowed. Please update the existing entry instead of creating a new one.");
        about.CreatedAt = DateTime.UtcNow;
        var result = await _repository.CreateAsync(about);
        return result;
    }

    public async Task<About?> GetAboutAsync()
    {
        var about = await _repository.GetAllAsync();
        var result = about.FirstOrDefault();
        return result;
    }

    public async Task<bool> UpdateAboutAsync(About about)
    {
        if (about is null) throw new ArgumentNullException("About cannot be null!", nameof(about));
        if (about.Id <= 0) throw new ArgumentException("ID must be greater than zero!", nameof(about.Id));
        var existingAbout = await GetAboutAsync();
        if (existingAbout is null) throw new InvalidOperationException("No existing About entry found to update.");
        ValidateAbout(about);
        about.UpdatedAt = existingAbout.CreatedAt;
        about.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.UpdateAsync(about);
        return result;
    }

    private void ValidateAbout(About about)
    {
        // Title
        if (string.IsNullOrWhiteSpace(about.Title)) throw new ArgumentException("Title cannot be empty or whitespace!", nameof(about.Title));

        // Title Length
        if (about.Title.Length > 100) throw new ArgumentException("Title cannot exceed 100 characters!", nameof(about.Title));

        // Description
        if (string.IsNullOrWhiteSpace(about.Description)) throw new ArgumentException("Description cannot be empty or whitespace!", nameof(about.Description));
    }
}
