using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureShield.Application.DTOs;
using SecureShield.Application.Services;
using SecureShield.Infrastructure.Data;

namespace SecureShield.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IValidator<LoginDto> _validator;
    private readonly IAdminAuthService _authService;

    public AdminController(IValidator<LoginDto> validator, IAdminAuthService authService)
    {
        _validator = validator;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var v = await _validator.ValidateAsync(dto);
        if (!v.IsValid)
            return BadRequest(new { ok = false, error = v.Errors[0].ErrorMessage });

        var token = await _authService.AuthenticateAsync(dto.Email, dto.Password);
        if (string.IsNullOrEmpty(token))
            return Unauthorized(new { ok = false, error = "Invalid email or password" });

        return Ok(new
        {
            ok = true,
            token,
            admin = new { email = StaticData.AdminEmail, name = StaticData.AdminName, role = StaticData.AdminRole },
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me() => Ok(new
    {
        ok = true,
        email = User.FindFirst(ClaimTypes.Email)?.Value,
        name = User.FindFirst(ClaimTypes.Name)?.Value
    });
}
