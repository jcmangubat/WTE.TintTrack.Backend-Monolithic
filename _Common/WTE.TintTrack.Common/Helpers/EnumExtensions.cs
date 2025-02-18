using System.ComponentModel.DataAnnotations;

namespace WTE.TintTrack.Common.Helpers;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum value)
    {
        var displayAttribute = value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttributes(false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        return displayAttribute?.Name ?? value.ToString();
    }

    public static string GetShortName(this Enum value)
    {
        var displayAttribute = value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttributes(false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        return displayAttribute?.ShortName ?? string.Empty;
    }
}