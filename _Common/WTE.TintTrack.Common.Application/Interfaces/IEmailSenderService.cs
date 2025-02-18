using WTE.TintTrack.Common.Models;

namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(
        EmailContact sender,
        string subject,
        string textBody,
        string? htmlBody,
        IEnumerable<EmailContact> primaryEmailRecipients,
        IEnumerable<EmailContact>? ccEmailRecipients,
        IEnumerable<EmailContact>? bccEmailRecipients);

    Task SendEmailAsync(
        EmailContact sender,
        string subject,
        string textBody,
        string? htmlBody,
        IEnumerable<EmailContact> primaryEmailRecipients);
}
