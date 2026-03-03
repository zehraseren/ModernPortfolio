using System;

namespace modernportfolio.Models;

public class Testimonial
{
    public int Id { get; set; }
    public string? ClientName { get; set; } = string.Empty;
    public string ClientPosition { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public string? ClientImageUrl { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}
