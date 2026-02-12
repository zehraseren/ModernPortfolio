using System;

namespace modernportfolio.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string? ProjectUrl { get; set; } = string.Empty;
    public string? GitHubUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
