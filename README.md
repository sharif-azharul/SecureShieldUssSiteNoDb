# USS Security Services Ltd. — .NET 10 / Blazor Premium Corporate Website

> **Tagline:** *Your Security, Our Commitment*

A premium, production-quality corporate website for a Bangladesh-based security personnel supplier. Built with **.NET 10 / C# / ASP.NET Core / Blazor Web App (Interactive Server) / Bootstrap 5 / MudBlazor / EF Core / SQL Server / ASP.NET Core Identity / Clean Architecture / Repository Pattern / FluentValidation**.

---

## Brand Theme

| Token | Hex | Usage |
|---|---|---|
| Primary (Royal Gold) | `#C8A23A` | Buttons, accents, icons, dividers, highlights |
| Primary Dark | `#A57C1B` | Hover, gradients |
| Secondary (Matte Black) | `#111111` | Nav, footer, text, dark sections |
| Accent (Royal Blue) | `#1F5FBF` | Selected accents |
| Background (Off-White) | `#F7F7F5` | Page background |
| Card Background | `#FFFFFF` | Cards, forms |
| Text | `#222222` | Body text |
| Border | `#D6C48A` | Card borders, form borders |
| Success | `#2E7D32` | Success messages |
| Warning | `#F9A825` | Warning badges |

**Design principles:** Premium · Luxury Corporate · Military-inspired clean layout · Minimal but powerful

**Buttons:** Gold background + black text → Hover: black background + gold text + gold shimmer
**Navigation:** Matte black with gold highlights
**Footer:** Black background + gold headings + white text
**Cards:** White + gold top border + soft shadow + gold icon + black heading

---

## Solution Structure (Clean Architecture)

```
SecureShield.sln  (USS Security solution)
└── src/
    ├── SecureShield.Domain/             # Domain entities (no deps)
    ├── SecureShield.Application/        # DTOs, interfaces, validators, services
    │   ├── Services/
    │   │   ├── EmailTemplates.cs        # USS-branded HTML email templates
    │   │   ├── Interfaces.cs            # IEmailService, IRecaptchaService, IAdminAuthService
    │   │   └── LocalizationService.cs
    │   └── Validators/                  # FluentValidation
    ├── SecureShield.Infrastructure/     # EF Core, Identity, SMTP
    │   ├── Data/
    │   │   ├── ApplicationDbContext.cs
    │   │   └── DbInitializer.cs         # Auto-migrate + seed 70+ records
    │   ├── Email/SmtpEmailService.cs
    │   ├── Identity/AdminAuthService.cs
    │   └── Repositories/
    └── SecureShield.Web/                # Blazor Web App
        ├── Program.cs
        ├── Components/
        │   ├── Layout/ (MainLayout, AdminLayout)
        │   ├── Pages/
        │   │   ├── _Host.cshtml         # Loading screen + USS favicon + Poppins/Inter
        │   │   ├── Index.razor          # Home (12 sections)
        │   │   ├── Error.razor
        │   │   └── Admin/ (Login, Dashboard, Careers, Messages, Services, Clients, Guards, GalleryManager, TestimonialsManager, Settings, Seo)
        │   ├── Sections/                # 12 public sections
        │   │   ├── Hero.razor           # "YOUR SECURITY OUR COMMITMENT" + 3 CTAs
        │   │   ├── Stats.razor          # 4 animated counters
        │   │   ├── About.razor
        │   │   ├── Services.razor       # 16 service cards
        │   │   ├── SecurityProcess.razor # NEW: 5-step process
        │   │   ├── Industries.razor     # 12 industries (dark)
        │   │   ├── WhyChooseUs.razor    # 8 advantages
        │   │   ├── Training.razor       # 7 modules (dark)
        │   │   ├── Guards.razor         # Filterable guard gallery
        │   │   ├── Clients.razor        # Marquee logos + testimonials
        │   │   ├── Gallery.razor        # Filterable photo grid
        │   │   ├── Career.razor         # Application form + CV upload
        │   │   └── Contact.razor        # Contact form + map + info cards
        │   └── Shared/ (Header, Footer, SectionHeading)
        ├── Controllers/ (Career, Contact, Admin)
        └── wwwroot/
            ├── css/site.css             # Premium Royal Gold/Black theme + luxury animations
            ├── js/site.js               # Smooth-scroll, reveal, counters, active nav
            ├── uss-logo.svg             # USS logo (header, footer, login, admin)
            ├── favicon.svg              # USS favicon
            ├── robots.txt
            └── sitemap.xml
```

---

## Prerequisites

- **.NET 10 SDK** — https://dotnet.microsoft.com/download/dotnet/10.0
- **SQL Server 2019+** (LocalDB works for dev)
- **Visual Studio 2022 v17.10+** (recommended)

---

## Quick Start

```bash
# Restore
dotnet restore SecureShield.sln

# Run (auto-migrates + seeds DB on first launch)
dotnet run --project src/SecureShield.Web
# → opens at https://localhost:5001
```

### Default Admin Login
```
URL:      /admin/login
Email:    admin@uss-security.com.bd
Password: Admin@123
```

The `DbInitializer.SeedAsync` runs on startup and seeds:
- 1 admin user (with "admin" role)
- 16 services, 12 industries, 6 testimonials, 12 clients
- 12 gallery items, 10 guard profiles
- 8 site settings (site.title, site.email, seo.title, etc.)

---

## Configuration (appsettings.json)

```json
{
  "ConnectionStrings": { "Default": "Server=...;Database=UssSecurityDb;..." },
  "Jwt": { "Secret": "64-char-random", "Issuer": "UssSecurity" },
  "Smtp": { "Host": "smtp.yourprovider.com", "Port": 587, "User": "noreply@uss-security.com.bd", "Password": "***" },
  "Recaptcha": { "SiteKey": "...", "SecretKey": "..." },
  "Site": {
    "ContactEmail": "info@uss-security.com.bd",
    "HrEmail": "hr@uss-security.com.bd",
    "BaseUrl": "https://uss-security.com.bd"
  }
}
```

---

## Features

### Public Website (12 sections)
1. **Hero** — "YOUR SECURITY / OUR COMMITMENT" + 3 CTAs (Get Free Quotation, Contact Us, Call Now) + dark control-room background with black gradient overlay
2. **Stats** — 4 animated counters (500+ guards, 200+ clients, 15+ years, 98% satisfaction)
3. **About** — Mission/vision + 4 credential bullets + image collage
4. **Services** — 16 premium white cards with gold top border + gold icons + black headings
5. **Security Process** — NEW 5-step (Requirement Analysis → Risk Assessment → Personnel Selection → Deployment → 24/7 Monitoring)
6. **Industries** — 12 industries on matte black background
7. **Why Choose Us** — 8 advantage cards
8. **Training** — 7 modules on dark background with sticky info panel
9. **Guards** — Filterable gallery (Male/Female/Armed/VIP/Supervisors)
10. **Clients** — Scrolling marquee logos + testimonials carousel
11. **Gallery** — Filterable photo grid (Duty/Training/Events/Office)
12. **Career** — Application form with CV upload → DB + branded email to HR
13. **Contact** — Contact form + OpenStreetMap + 6 info cards → DB + branded email to info@

### Admin Panel (`/admin/login` → `/admin/dashboard`)
- Secure login (ASP.NET Core Identity + JWT)
- Dashboard (8 stat cards + tips)
- Manage Career Applications (status workflow, delete)
- Manage Contact Messages (mark read, reply, delete)
- Full CRUD: Services, Clients, Guards, Gallery, Testimonials
- Edit Website Settings + SEO Settings

### Premium Design Touches
- **Loading screen** with USS shield + spinner (auto-hides after page load)
- **USS logo** in header, footer, login, admin dashboard, favicon, loading screen, email templates
- **Gold shimmer animation** on buttons
- **Hover lift** on all cards
- **Scroll-reveal** (fade-up + slide-left) via IntersectionObserver
- **Counter animation** on stats
- **Smooth scrolling** with active nav highlighting
- **Poppins** for headings, **Inter** for body
- **Loading screen** with branded shield logo

### Security & SEO
- ASP.NET Core Identity + JWT for admin auth
- FluentValidation on all inputs (Bangladeshi phone regex)
- reCAPTCHA v3 on contact form
- Security headers (X-Frame-Options, X-Content-Type-Options, Referrer-Policy, HSTS)
- Schema.org Organization JSON-LD in `_Host.cshtml`
- Open Graph + Twitter Card meta tags
- `sitemap.xml` + `robots.txt`

---

## Email Templates

USS-branded HTML emails with Royal Gold + Matte Black theme (see `EmailTemplates.cs`):
- **ContactNotification** — sent to `info@uss-security.com.bd` when contact form is submitted
- **CareerNotification** — sent to `hr@uss-security.com.bd` when career application is submitted

Both include USS logo header, gold-bordered content section, and black footer with full contact details.

---

## Add Real Images

Drop photos into `wwwroot/images/`:
- `hero-bg.jpg` (1920×1080) — security guards in formation or dark control room
- `about.jpg` (900×1125) — team briefing
- `gallery-1.jpg` … `gallery-12.jpg` (800×800)
- `guard-1.jpg` … `guard-10.jpg` (600×800)

The DbInitializer seeds these relative paths automatically.

---

## Production Deployment to IIS

### Step 1 — Install prerequisites on Windows Server
1. .NET 10 Hosting Bundle
2. URL Rewrite Module 2.1
3. SQL Server 2019+
4. IIS with WebSockets enabled (required for Blazor Server)

### Step 2 — Publish
```powershell
dotnet publish src/SecureShield.Web/SecureShield.Web.csproj -c Release -o C:\Deploy\UssSecurity
```

### Step 3 — Configure SQL Server
Create database `UssSecurityDb` + login `uss_app` with `db_owner`. Update `appsettings.json` connection string.

### Step 4 — Create IIS Site
- Site name: `UssSecurity`
- Physical path: `C:\Deploy\UssSecurity`
- App pool: `.NET CLR Version = No Managed Code` (out-of-process)
- Bindings: `https://uss-security.com.bd:443` with valid SSL cert
- Enable WebSockets (required for Blazor Server)

### Step 5 — Set folder permissions
- `IIS_IUSRS` → Read & Execute
- App pool identity → Modify on `wwwroot\uploads\`

### Step 6 — Use deploy-iis.ps1
```powershell
.\deploy-iis.ps1 -SiteName "UssSecurity" -PhysicalPath "C:\Deploy\UssSecurity" -HostName "uss-security.com.bd" -ConnectionString "Server=...;Database=UssSecurityDb;..." -JwtSecret "your-64-char-secret" -SmtpHost "smtp.yourprovider.com" -SmtpUser "noreply@uss-security.com.bd" -SmtpPassword "***"
```

---

## EF Migrations

```bash
# Add migration
dotnet ef migrations add YourMigration --project src/SecureShield.Infrastructure --startup-project src/SecureShield.Web

# Apply
dotnet ef database update --project src/SecureShield.Infrastructure --startup-project src/SecureShield.Web

# Generate SQL script for production
dotnet ef migrations script --project src/SecureShield.Infrastructure --startup-project src/SecureShield.Web --output migrate.sql
```

---

## Demo Credentials

```
Admin URL:  /admin/login
Email:      admin@uss-security.com.bd
Password:   Admin@123
```

⚠️ **Change the admin password immediately** after first login in production.

---

## License & Copyright

© 2026 USS Security Services Ltd. All rights reserved.

Built with .NET 10, Blazor, EF Core, MudBlazor, Bootstrap 5, FluentValidation.
