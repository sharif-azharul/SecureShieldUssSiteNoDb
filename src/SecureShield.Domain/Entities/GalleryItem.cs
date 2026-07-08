namespace SecureShield.Domain.Entities;

public class GalleryItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string Category { get; set; } = default!; // duty | training | events | office
    public string ImageUrl { get; set; } = default!;
    public string? Caption { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
