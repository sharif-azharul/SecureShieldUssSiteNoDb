namespace SecureShield.Domain.Entities;

public class SiteSetting
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
}
