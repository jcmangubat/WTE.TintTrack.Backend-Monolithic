using System.Security.Cryptography;
using System.Text;

namespace WTE.TintTrack.Common.Helpers;

public class CodeGenerator
{
    public static string GenerateUniqueCode(string input, int totalLength)
    {
        // Generate initials
        var words = input.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries);

        string? initials = (words.Length == 1) ?
                                (input.Length > 3 ? input.Substring(0, 3) : input) :
                                string.Concat(words.Select(w => w[0])).ToUpper(); // Uppercase initials
        // Generate hash
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper(); // Convert to uppercase string

        // Limit initials to 3 characters
        initials = initials[..Math.Min(initials.Length, 3)];

        // Ensure total length does not exceed specified length
        int maxHashLength = totalLength - initials.Length;
        if (maxHashLength < 0) maxHashLength = 0; // In case initials exceed totalLength

        // Combine initials and part of the hash
        return string.Concat(initials, hash.Substring(0, Math.Min(hash.Length, maxHashLength)));
    }

    private static readonly char[] separator = [' '];
    private static readonly char[] separatorArray = new[] { ' ', '.', ',' };

    private static string GetInitials(string input)
    {
        // Split the input into words and get the initials
        string[] words = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder initials = new();

        foreach (var word in words)
        {
            if (word.Length > 0)
                initials.Append(word[0]); // Append the first letter of each word
        }

        return initials.ToString().ToUpper(); // Return initials in uppercase
    }
}
