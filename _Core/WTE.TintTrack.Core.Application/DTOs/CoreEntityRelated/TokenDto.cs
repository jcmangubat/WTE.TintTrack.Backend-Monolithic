using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Application.Shared.Validator.Attributes;

namespace WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;

public class TokenDto : GuidKeyedAuditableModel
{
    [Required]
    [MaxLength(FieldLengths.Token.RefreshToken)]
    public required string RefreshToken { get; set; }

    [Required]
    public required DateTime RefreshTokenExpiration { get; set; }
}