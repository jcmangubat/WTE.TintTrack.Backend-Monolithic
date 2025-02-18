using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Domain.Shared;

namespace WTE.TintTrack.Infrastructure.Shared.Services;

public class MessageProviderService(IOptions<ApplicationSettings> appSettings) : IMessageProviderService
{
    private readonly string _messagesPath = appSettings.Value.ErrorMessagesPath
            ?? throw new ArgumentNullException(nameof(appSettings.Value.ErrorMessagesPath), "ErrorMessagesPath must be configured in the Application Settings.");

    private readonly Dictionary<string, Lazy<Dictionary<string, string>>> _cachedMessages = [];

    public APIMessage GetMessage(string code, string? locale = null, Dictionary<string, string>? templateVals = null)
    {
        locale = locale ?? "en";
        if (!_cachedMessages.TryGetValue(locale, out Lazy<Dictionary<string, string>>? value))
        {
            value = new Lazy<Dictionary<string, string>>(() => LoadMessagesForLocale(locale));
            _cachedMessages[locale] = value;
        }

        var messages = value.Value;

        if (messages.TryGetValue(code, out var message))
        {
            if (templateVals != null && templateVals.Values.Count > 0)
            {
                foreach (var key in templateVals.Keys)
                {
                    message = message.Replace(key, templateVals[key]);
                }
            }
            return new APIMessage(code, message);
        }

        return new APIMessage("ERR000", $"Message for code '{code}' not found in locale '{locale}'.");
    }

    private Dictionary<string, string> LoadMessagesForLocale(string locale)
    {
        var assLocation = Assembly.GetExecutingAssembly().Location;
        var filePath = Path.Combine(Path.GetDirectoryName(assLocation), _messagesPath, $"{locale}.json");
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Locale file '{filePath}' not found.");

        var jsonData = File.ReadAllText(filePath);
        var localizedMessages = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);

        if (localizedMessages != null)
            return localizedMessages;

        throw new InvalidOperationException($"Invalid message format in locale file '{filePath}'.");
    }

    public ServiceOperationException ServiceOperationException(string errorCode, string locale = "en")
    {
        var apiMsg = GetMessage(errorCode, locale);
        return new ServiceOperationException(apiMsg.Code, apiMsg.Message);
    }

    public CustomSecurityTokenException CustomSecurityTokenException(string errorCode, string locale = "en")
    {
        var apiMsg = GetMessage(errorCode, locale);
        return new CustomSecurityTokenException(apiMsg.Message, apiMsg.Code);
    }

}