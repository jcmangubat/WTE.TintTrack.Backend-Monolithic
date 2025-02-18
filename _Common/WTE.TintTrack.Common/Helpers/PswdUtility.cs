namespace WTE.TintTrack.Common.Helpers;

public class PswdUtility {

    /// <summary>
    /// Generate a salt and hash the password
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string HashPassword(string password)
    {        
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Check if the provided plain password matches the hashed password
    /// </summary>
    /// <param name="plainPassword"></param>
    /// <param name="hashedPassword"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}
