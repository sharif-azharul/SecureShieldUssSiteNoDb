namespace SecureShield.Application.Services;

/// <summary>
/// USS-branded premium HTML email templates — Royal Gold + Matte Black theme.
/// </summary>
public static class EmailTemplates
{
    private static string Shell(string title, string body)
    {
        return $@"<!DOCTYPE html>
<html><head><meta charset='utf-8'/><meta name='viewport' content='width=device-width, initial-scale=1'/><title>{title}</title></head>
<body style='margin:0;padding:0;background:#F7F7F5;font-family:Inter,Arial,sans-serif;color:#222222;'>
<table width='100%' cellpadding='0' cellspacing='0' style='background:#F7F7F5;min-height:100vh;'>
  <tr><td align='center' style='padding:32px 16px;'>
    <table width='600' cellpadding='0' cellspacing='0' style='background:#FFFFFF;border-radius:16px;overflow:hidden;box-shadow:0 8px 24px rgba(17,17,17,0.08);border-top:4px solid #C8A23A;'>

      <!-- Header -->
      <tr><td style='background:#111111;padding:24px 32px;border-bottom:3px solid #C8A23A;'>
        <table width='100%'><tr>
          <td style='vertical-align:middle;'>
            <div style='font-family:Poppins,Arial,sans-serif;font-size:22px;font-weight:800;color:#C8A23A;letter-spacing:1px;'>
              USS <span style='color:#FFFFFF;'>Security</span>
            </div>
            <div style='font-family:Inter,Arial,sans-serif;font-size:10px;color:rgba(255,255,255,0.6);letter-spacing:2.5px;margin-top:2px;'>
              SERVICES LTD.
            </div>
          </td>
          <td align='right' style='vertical-align:middle;'>
            <div style='font-family:Poppins,Arial,sans-serif;font-size:10px;font-weight:700;color:#C8A23A;text-transform:uppercase;letter-spacing:2px;'>
              Your Security<br/>Our Commitment
            </div>
          </td>
        </tr></table>
      </td></tr>

      <!-- Body -->
      <tr><td style='padding:36px 32px 12px;'>{body}</td></tr>

      <!-- Footer -->
      <tr><td style='background:#111111;padding:24px 32px;'>
        <div style='font-family:Poppins,Arial,sans-serif;font-size:13px;font-weight:700;color:#C8A23A;margin-bottom:6px;'>USS Security Services Ltd.</div>
        <div style='font-family:Inter,Arial,sans-serif;font-size:11px;color:rgba(255,255,255,0.6);line-height:1.7;'>
          Level 6, Plot 37, Gulshan Avenue, Gulshan-1, Dhaka 1212, Bangladesh<br/>
          📞 +880 1700-000000 &nbsp; ✉ info@uss-security.com.bd<br/><br/>
          <span style='color:#C8A23A;'>YOUR SECURITY · OUR COMMITMENT</span>
        </div>
      </td></tr>

    </table>
    <div style='font-family:Inter,Arial,sans-serif;font-size:10px;color:#999999;margin-top:16px;text-align:center;'>
      © {DateTime.UtcNow.Year} USS Security Services Ltd. All rights reserved.
    </div>
  </td></tr>
</table>
</body></html>";
    }

    public static string ContactNotification(string name, string? company, string phone, string email, string subject, string message)
    {
        var body = $@"
        <div style='font-family:Poppins,Arial,sans-serif;font-size:18px;font-weight:700;color:#111111;margin-bottom:6px;'>New Contact Form Submission</div>
        <div style='font-family:Inter,Arial,sans-serif;font-size:13px;color:#6B6B6B;margin-bottom:24px;'>From USS Security website contact form.</div>
        <table width='100%' cellpadding='0' cellspacing='0' style='font-size:14px;line-height:1.7;'>
          <tr><td style='color:#A57C1B;width:130px;font-weight:600;vertical-align:top;'>Name</td><td style='color:#111111;font-weight:700;'>{name}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Company</td><td style='color:#222222;'>{company ?? "—"}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Phone</td><td style='color:#222222;'>{phone}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Email</td><td style='color:#222222;'>{email}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Subject</td><td style='color:#111111;font-weight:700;'>{subject}</td></tr>
        </table>
        <div style='margin-top:24px;font-family:Poppins,Arial,sans-serif;font-size:14px;font-weight:700;color:#111111;'>Message</div>
        <div style='background:#F7F7F5;border-left:3px solid #C8A23A;padding:16px 20px;border-radius:8px;margin-top:10px;color:#222222;line-height:1.7;font-size:14px;'>{message.Replace("\n", "<br/>")}</div>
        <div style='margin-top:24px;font-family:Inter,Arial,sans-serif;font-size:11px;color:#999999;'>Reply directly to {email}.</div>";
        return Shell("New Contact — USS Security", body);
    }

    public static string CareerNotification(string name, string phone, string email, string position, string education, string experience, string address, string? cvFileName)
    {
        var body = $@"
        <div style='font-family:Poppins,Arial,sans-serif;font-size:18px;font-weight:700;color:#111111;margin-bottom:6px;'>New Career Application</div>
        <div style='font-family:Inter,Arial,sans-serif;font-size:13px;color:#6B6B6B;margin-bottom:24px;'>A candidate has submitted an application via the USS Security career page.</div>
        <table width='100%' cellpadding='0' cellspacing='0' style='font-size:14px;line-height:1.7;'>
          <tr><td style='color:#A57C1B;width:140px;font-weight:600;vertical-align:top;'>Name</td><td style='color:#111111;font-weight:700;'>{name}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Phone</td><td style='color:#222222;'>{phone}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Email</td><td style='color:#222222;'>{email}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Position</td><td style='color:#111111;font-weight:700;'>{position}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Education</td><td style='color:#222222;'>{education}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Experience</td><td style='color:#222222;'>{experience}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>Address</td><td style='color:#222222;'>{address}</td></tr>
          <tr><td style='color:#A57C1B;font-weight:600;vertical-align:top;'>CV</td><td style='color:#222222;'>{cvFileName ?? "—"}</td></tr>
        </table>
        <div style='margin-top:24px;padding:14px 18px;background:rgba(200,162,58,0.1);border-left:3px solid #C8A23A;border-radius:8px;font-family:Inter,Arial,sans-serif;font-size:13px;color:#111111;'>
          📌 Please log in to the admin panel to review and update this application status.
        </div>";
        return Shell("New Career Application — USS Security", body);
    }
}
