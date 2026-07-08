namespace SecureShield.Domain.Entities;

/// <summary>
/// Service offered by SecureShield (Industrial, Bank, VIP, etc.).
/// </summary>
public class Service
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Slug { get; set; } = default!;
    public string TitleEn { get; set; } = default!;
    public string TitleBn { get; set; } = default!;
    public string DescriptionEn { get; set; } = default!;
    public string DescriptionBn { get; set; } = default!;
    public string Icon { get; set; } = "Shield";
    public bool Featured { get; set; }
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
