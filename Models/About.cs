using System;

namespace modernportfolio.Models;

public class About
{
    public int Id { get; set; }
    // string.Empty yazarak varsayılan değeri boş olarak atamak
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
