using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecureShield.Application.Services;

namespace SecureShield.Infrastructure.Email;

public class SmtpSettings
{
    public string Host { get; set; } = "smtp.secureshield.com.bd";
    public int Port { get; set; } = 587;
    public string User { get; set; } = "noreply@secureshield.com.bd";
    public string Password { get; set; } = "";
    public bool UseSsl { get; set; } = true;
    public string FromName { get; set; } = "SecureShield Website";
}

public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _settings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(IOptions<SmtpSettings> settings, ILogger<SmtpEmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<(bool Ok, string? Error)> SendEmailAsync(
        string to, string from, string? replyTo, string subject, string text, string? html = null)
    {
        try
        {
            using var msg = new MailMessage
            {
                From = new MailAddress(from, _settings.FromName),
                Subject = subject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                BodyEncoding = System.Text.Encoding.UTF8,
            };
            msg.To.Add(to);
            if (!string.IsNullOrEmpty(replyTo)) msg.ReplyToList.Add(replyTo);
            msg.Body = html ?? text;
            msg.IsBodyHtml = !string.IsNullOrEmpty(html);

            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                EnableSsl = _settings.UseSsl,
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 30_000,
            };

            await client.SendMailAsync(msg);
            _logger.LogInformation("Email sent to {To} subject={Subject}", to, subject);
            return (true, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);
            return (false, ex.Message);
        }
    }
}

public class RecaptchaService : IRecaptchaService
{
    private readonly HttpClient _http;
    private readonly string _secretKey;
    private readonly ILogger<RecaptchaService> _logger;

    public RecaptchaService(IConfiguration config, IHttpClientFactory factory, ILogger<RecaptchaService> logger)
    {
        _http = factory.CreateClient();
        _secretKey = config["Recaptcha:SecretKey"] ?? "";
        _logger = logger;
    }

    public async Task<(bool Success, double Score)> VerifyAsync(string token)
    {
        if (string.IsNullOrEmpty(_secretKey))
        {
            _logger.LogWarning("reCAPTCHA secret key not configured — skipping verification");
            return (true, 1.0);
        }
        if (string.IsNullOrEmpty(token)) return (false, 0);

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("secret", _secretKey),
            new KeyValuePair<string, string>("response", token),
        });

        try
        {
            var res = await _http.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            var json = await res.Content.ReadAsStringAsync();
            // Simple parse — replace with System.Text.Json
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var success = doc.RootElement.GetProperty("success").GetBoolean();
            var score = doc.RootElement.TryGetProperty("score", out var s) ? s.GetDouble() : 0.5;
            return (success, score);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "reCAPTCHA verify failed");
            return (false, 0);
        }
    }
}
