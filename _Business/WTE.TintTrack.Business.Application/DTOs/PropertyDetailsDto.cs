using SMEAppHouse.Core.Patterns.EF.DtoModelAbstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WTE.TintTrack.Business.Domain.Entities;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs;

/// <summary>
/// EF Core's Table-per-Hierarchy (TPH) implementation.
/// </summary>
[Table("CustomerPropertyDetails")]
public abstract class PropertyDetailsDto : GuidKeyedAuditableModel
{
    public abstract PropertyTypesEnum PropertyType { get; set; }

    [Key]
    public Guid CustomerPropertyId { get; set; }
    public virtual Property CustomerProperty { get; set; }
}
