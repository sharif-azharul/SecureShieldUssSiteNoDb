using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SecureShield.Application.DTOs;
using SecureShield.Application.Interfaces;
using SecureShield.Application.Services;

namespace SecureShield.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactMessageRepository _messages;
    private readonly IEmailService _email;
    private readonly IRecaptchaService _recaptcha;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<ContactMessageDto> _validator;
    private readonly IConfiguration _config;

    public ContactController(
        IContactMessageRepository messages,
        IEmailService email,
        IRecaptchaService recaptcha,
        IUnitOfWork uow,
        IValidator<ContactMessageDto> validator,
        IConfiguration config)
    {
        _messages = messages;
        _email = email;
        _recaptcha = recaptcha;
        _uow = uow;
        _validator = validator;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactMessageDto dto)
    {
        var v = await _validator.ValidateAsync(dto);
        if (!v.IsValid)
            return BadRequest(new { ok = false, error = v.Errors[0].ErrorMessage });

        var token = dto.RecaptchaToken ?? "";
        var (recOk, score) = await _recaptcha.VerifyAsync(token);
        if (!recOk || score < 0.5)
            return BadRequest(new { ok = false, error = "reCAPTCHA verification failed." });

        var msg = new Domain.Entities.ContactMessage
        {
            Name = dto.Name,
            Company = dto.Company,
            Phone = dto.Phone,
            Email = dto.Email,
            Subject = dto.Subject,
            Message = dto.Message,
        };

        await _messages.AddAsync(msg);
        await _uow.SaveChangesAsync();

        var contactEmail = _config["Site:ContactEmail"] ?? "info@uss-security.com.bd";
        var html = SecureShield.Application.Services.EmailTemplates.ContactNotification(
            dto.Name, dto.Company, dto.Phone, dto.Email, dto.Subject, dto.Message);

        var (ok, err) = await _email.SendEmailAsync(
            contactEmail,
            "noreply@uss-security.com.bd",
            dto.Email,
            $"[Contact] {dto.Subject}",
            $"Name: {dto.Name}\nCompany: {dto.Company ?? "—"}\nPhone: {dto.Phone}\nEmail: {dto.Email}\nSubject: {dto.Subject}\n\n{dto.Message}",
            html);

        if (!ok)
            return StatusCode(500, new { ok = false, error = err });

        return Ok(new { ok = true, id = msg.Id });
    }
}
