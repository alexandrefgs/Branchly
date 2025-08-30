using Branchly.Auth.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _cfg;
    private readonly ILogger<EmailSender> _log;
    public EmailSender(IConfiguration cfg, ILogger<EmailSender> log)
    {
        _cfg = cfg; _log = log;
    }

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        var enabled = _cfg.GetValue<bool>("Smtp:Enabled");
        if (!enabled)
        {
            _log.LogInformation("[DEV MAIL] To={To} Subject={Subject} Body={Body}", to, subject, htmlBody);
            return;
        }

        // TODO: SMTP real (MailKit, etc.)
        // ...
        await Task.CompletedTask;
    }

    public Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
