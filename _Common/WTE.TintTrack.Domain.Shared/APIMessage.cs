namespace WTE.TintTrack.Domain.Shared;

public class APIMessage(string code, string message)
{
    public string Code { get; set; } = code;
    public string Message { get; set; } = message;
}