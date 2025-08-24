using Microsoft.Extensions.Logging;

namespace Branchly.Auth.Services;
public class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{
    public Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
    {
        logger.LogInformation("DEV EMAIL -> To:{To} | Subject:{Subject}\n{Body}", to, subject, htmlBody);
        return Task.CompletedTask;
    }
}