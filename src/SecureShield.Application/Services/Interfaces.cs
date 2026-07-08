namespace SecureShield.Application.Services;

public interface IEmailService
{
    Task<(bool Ok, string? Error)> SendEmailAsync(
        string to, string from, string? replyTo,
        string subject, string text, string? html = null);
}

public interface IRecaptchaService
{
    Task<(bool Success, double Score)> VerifyAsync(string token);
}

public interface IAdminAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
}
