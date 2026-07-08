using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureShield.Application.DTOs;
using SecureShield.Application.Interfaces;
using SecureShield.Application.Services;

namespace SecureShield.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CareerController : ControllerBase
{
    private readonly ICareerRepository _careers;
    private readonly IEmailService _email;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<CareerApplicationDto> _validator;
    private readonly IConfiguration _config;

    public CareerController(
        ICareerRepository careers,
        IEmailService email,
        IUnitOfWork uow,
        IValidator<CareerApplicationDto> validator,
        IConfiguration config)
    {
        _careers = careers;
        _email = email;
        _uow = uow;
        _validator = validator;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CareerApplicationDto dto)
    {
        var v = await _validator.ValidateAsync(dto);
        if (!v.IsValid)
            return BadRequest(new { ok = false, error = v.Errors[0].ErrorMessage });

        var cvPath = !string.IsNullOrEmpty(dto.CvFileName)
            ? $"/uploads/cv/{Guid.NewGuid()}_{dto.CvFileName}"
            : null;

        var app = new Domain.Entities.CareerApplication
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,
            Education = dto.Education,
            Experience = dto.Experience,
            Position = dto.Position,
            CvFileName = dto.CvFileName,
            CvFilePath = cvPath,
        };

        await _careers.AddAsync(app);
        await _uow.SaveChangesAsync();

        var hrEmail = _config["Site:HrEmail"] ?? "hr@uss-security.com.bd";
        var html = SecureShield.Application.Services.EmailTemplates.CareerNotification(
            dto.Name, dto.Phone, dto.Email, dto.Position, dto.Education, dto.Experience, dto.Address, dto.CvFileName);
        await _email.SendEmailAsync(
            hrEmail,
            "noreply@uss-security.com.bd",
            dto.Email,
            $"New career application — {dto.Position}",
            $"New application:\n\nName: {dto.Name}\nPhone: {dto.Phone}\nEmail: {dto.Email}\nPosition: {dto.Position}\nEducation: {dto.Education}\nExperience: {dto.Experience}\nAddress: {dto.Address}\nCV: {dto.CvFileName ?? "—"}\n\nApplication ID: {app.Id}",
            html);

        return Ok(new { ok = true, id = app.Id });
    }
}
