namespace SecureShield.Application.DTOs;

public class CareerApplicationDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Education { get; set; } = string.Empty;
    public string Experience { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string? CvFileName { get; set; }
    public string? CvFileBase64 { get; set; }
}

public class ContactMessageDto
{
    public string Name { get; set; } = string.Empty;
    public string? Company { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? RecaptchaToken { get; set; } = string.Empty;
}

public record LoginDto(string Email, string Password);

public record LoginResultDto(bool Success, string? Token, string? Error, string? Name, string? Role);

public record DashboardStatsDto(
    int Services, int Clients, int Gallery, int Testimonials,
    int Careers, int Messages, int Guards,
    int NewCareers, int UnreadMessages
);

public record SiteSettingsDto(Dictionary<string, string> Settings);
