namespace SecureShield.Domain.Entities;

public class Testimonial
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Company { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Message { get; set; } = default!;
    public int Rating { get; set; } = 5;
    public string? Avatar { get; set; }
    public bool Published { get; set; } = true;
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
