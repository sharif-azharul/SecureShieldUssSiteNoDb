namespace SecureShield.Domain.Entities;

public class CareerApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Education { get; set; } = default!;
    public string Experience { get; set; } = default!;
    public string Position { get; set; } = default!;
    public string? CvFileName { get; set; }
    public string? CvFilePath { get; set; }
    public string Status { get; set; } = "new"; // new | reviewed | shortlisted | rejected
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
