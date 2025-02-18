using SMEAppHouse.Core.Patterns.EF.EntityCompositing.Abstractions;
using System.ComponentModel.DataAnnotations;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Core.Domain.Entities;

public class Tenant : GuidKeyedAuditableEntity
{
    /// <summary>
    /// Unique identifier code for the tenant
    /// </summary>
    public required string TenantCode { get; set; }

    /// <summary>
    /// Name of the tenant's business
    /// </summary>
    [Required]
    [StringLength(FieldLengths.Tenant.Name, MinimumLength = 3)]
    public required string Name { get; set; }

    [StringLength(500)]
    public required string Description { get; set; }

    /// <summary>
    /// Name of the tenant (e.g., custom URLs)
    /// </summary>
    public string? Domain { get; set; }

    public string? LogoImageUrl { get; set; }

    /*[EmailAddress]
    public required string Email { get; set; }*/

    /*[Phone]
    public required string ContactNumber { get; set; }*/

    /// <summary>
    /// Status of the tenant(Active, Inactive, Suspended)
    /// </summary>
    public required Consts.TenantStatusEnum TenantStatus { get; set; }

    public string? ConnectionString { get; set; }

    public string? CountryOfHost { get; set; }
    
    /// <summary>
    /// Many-to-Many Relationship with Tenant: 
    /// This tenant may have several users associated
    /// </summary>
    public virtual ICollection<UserTenant> UserTenants { get; set; } = new HashSet<UserTenant>();

    public virtual ICollection<TenantSubscription>? TenantSubscriptions { get; set; } = new HashSet<TenantSubscription>();

    public virtual ICollection<Token> Tokens { get; set; } = new HashSet<Token>();
    
    public virtual ICollection<UserTenantInvitation> UserTenantInvitations { get; set; } = new HashSet<UserTenantInvitation>();
}
