namespace SecureShield.Domain.Entities;

public class GuardProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!; // male | female | armed | vip | supervisor
    public string Role { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
