using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Common.Models;

public class EmailContact
{
    public required string EmailAddress { get; set; }
    public string? Name { get; set; }

    public Consts.RecipientTypesEnum? RecipientType { get; set; }

    public override string ToString() => $"{Name} <{EmailAddress}>".Trim();
}
