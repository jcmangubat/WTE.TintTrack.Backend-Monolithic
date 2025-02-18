using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Models;

namespace WTE.TintTrack.Infrastructure.Shared.Services;

public class EmailSenderService(ILogger<EmailSenderService> logger, SMTPSettings smtpSettings) : IEmailSenderService
{
    private readonly ILogger<EmailSenderService> _logger = logger;
    private readonly SMTPSettings _smtpSettings = smtpSettings;

    public async Task SendEmailAsync(EmailContact sender, string subject, string textBody, string? htmlBody, IEnumerable<EmailContact> primaryEmailRecipients)
    {
        await SendEmailAsync(sender, subject, textBody, htmlBody, primaryEmailRecipients, null, null);
    }

    public async Task SendEmailAsync(
        EmailContact sender,
        string subject,
        string textBody,
        string? htmlBody,
        IEnumerable<EmailContact> primaryEmailRecipients,
        IEnumerable<EmailContact>? ccEmailRecipients,
        IEnumerable<EmailContact>? bccEmailRecipients)
    {
        var smtpHost = _smtpSettings.Host;
        var smtpPort = _smtpSettings.Port;
        var smtpUsername = _smtpSettings.Username;
        var smtpPassword = _smtpSettings.Password;

        ArgumentNullException.ThrowIfNull(sender);
        ArgumentNullException.ThrowIfNull(primaryEmailRecipients);

        // Create email message
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(sender.Name ?? sender.EmailAddress, sender.EmailAddress));

        foreach (var primaryEmailRecipient in primaryEmailRecipients)
            message.To.Add(new MailboxAddress(primaryEmailRecipient.Name ?? primaryEmailRecipient.EmailAddress, primaryEmailRecipient.EmailAddress));

        var allEmailContacts = new List<EmailContact>(primaryEmailRecipients);

        if (ccEmailRecipients != null && ccEmailRecipients.Count() > 0)
        {
            allEmailContacts.AddRange(ccEmailRecipients);
            foreach (var ccEmailRecipient in ccEmailRecipients)
            {
                message.Cc.Add(new MailboxAddress(ccEmailRecipient.Name ?? ccEmailRecipient.EmailAddress, ccEmailRecipient.EmailAddress));
            }
        }

        if (bccEmailRecipients != null && bccEmailRecipients.Count() > 0)
        {
            allEmailContacts.AddRange(bccEmailRecipients);
            foreach (var bccEmailRecipient in bccEmailRecipients)
            {
                message.Bcc.Add(new MailboxAddress(bccEmailRecipient.Name ?? bccEmailRecipient.EmailAddress, bccEmailRecipient.EmailAddress));
            }
        }

        message.Subject = subject;

        // Create the body of the email
        var bodyBuilder = new BodyBuilder
        {
            TextBody = textBody,
            HtmlBody = htmlBody ?? string.Empty,
        };
        message.Body = bodyBuilder.ToMessageBody();

        var msgContacts = string.Join(", ", allEmailContacts);

        try
        {
            // Configure and send the email using MailKit's SmtpClient
            using var client = new SmtpClient();

            client.Connect(smtpHost, 465, true); // SMTP server address and port
            client.Authenticate(smtpUsername, smtpPassword); // SMTP credentials

            var result = await client.SendAsync(message);
            await client.DisconnectAsync(true);

            var logMsg = $"Message sent to {msgContacts} from {sender}";
            _logger.LogInformation(message: logMsg);
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            var logMsg = $"Error sending email to {msgContacts} : {ex.GetExceptionMessages()}";
            _logger.LogError(message: logMsg, ex);
            throw;
        }
    }
}
