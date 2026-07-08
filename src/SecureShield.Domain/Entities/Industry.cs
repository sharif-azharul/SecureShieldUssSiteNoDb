namespace SecureShield.Domain.Entities;

public class Industry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Slug { get; set; } = default!;
    public string TitleEn { get; set; } = default!;
    public string TitleBn { get; set; } = default!;
    public string DescriptionEn { get; set; } = default!;
    public string DescriptionBn { get; set; } = default!;
    public string Icon { get; set; } = "Building";
    public int Order { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
