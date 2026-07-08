using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using MudBlazor.Services;
using SecureShield.Application.Interfaces;
using SecureShield.Application.Services;
using SecureShield.Application.Validators;
using SecureShield.Infrastructure.Email;
using SecureShield.Infrastructure.Identity;
using SecureShield.Infrastructure.Repositories;
using SecureShield.Web.Components;
using SecureShield.Web.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- JWT for admin auth (no database, no Identity) ---
var jwtKey = builder.Configuration["Jwt:Secret"] ?? "dev-secret-change-me-32-chars-min!!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "UssSecurity";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "UssSecurity";

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
    };
})
.AddCookie("AdminCookie", opt =>
{
    opt.LoginPath = "/admin/login";
    opt.AccessDeniedPath = "/admin/login";
    opt.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// --- Blazor Server ---
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// --- MudBlazor ---
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.VisibleStateDuration = 4000;
});

// --- FluentValidation ---
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CareerApplicationValidator>();

// --- In-memory repositories (no database) ---
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IGuardRepository, GuardRepository>();
builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<ISiteSettingRepository, SiteSettingRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();
builder.Services.AddScoped<LanguageJsInterop>();

// --- Email / reCAPTCHA ---
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRecaptchaService, RecaptchaService>();

// --- Controllers (API) ---
builder.Services.AddControllers();

// --- Security headers, HSTS, HTTPS ---
builder.Services.AddHsts(opt =>
{
    opt.Preload = true;
    opt.IncludeSubDomains = true;
    opt.MaxAge = TimeSpan.FromDays(365);
});

var app = builder.Build();

// --- Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Security headers
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
    ctx.Response.Headers["X-Frame-Options"] = "DENY";
    ctx.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    ctx.Response.Headers["X-XSS-Protection"] = "1; mode=block";
    ctx.Response.Headers["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

public partial class Program { }
