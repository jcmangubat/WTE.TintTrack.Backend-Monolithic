using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Interfaces;

public interface IUserService : IMappedLoggingService<IUserService>
{
    Task<ApplicationUserDto?> GetByIdAsync(Guid userId);
    Task<ApplicationUserDto?> GetByUserCodeAsync(string userCode);
    Task<ApplicationUserDto?> GetByEmailAsync(string email);
    Task UpdateAsync(ApplicationUserDto userDto);
    Task<string> UploadUserProfileImage(string userCode, IFormFile? avatarFormFile);
    Task DeleteAsync(Guid userId);
    Task DeleteByUserCodeAsync(string userCode);
    Task<IEnumerable<TenantDto>> GetTenantsForUserAsync(Guid userId, bool? activesOnly = null);
    Task<IEnumerable<TenantDto>> GetTenantsForUserAsync(string userCode, bool? activesOnly = null);

    Task<IEnumerable<ApplicationUserDto>> GetAllAsync(bool? activeOnly = null);
    Task<IEnumerable<ApplicationUserDto>> GetAllAsync(Expression<Func<ApplicationUserDto, bool>> dtoPredicate);
    Task<IEnumerable<ApplicationUserDto>> GetAllByTenantAsync(string tenantCode, bool? activeOnly = null);
    Task<IEnumerable<ApplicationUserDto>> GetAllByTenantAsync(Guid tenantId, bool? activeOnly = null);

    Task<bool> UserExistsAsync(Guid tenantId, string email);
    Task<(ApplicationUserDto User, TenantDto Tenant)> RegisterUserAsync(ApplicationUserDto applicationUserDto,
                                                TenantEntryDto tenantEntry,
                                                string password);
    Task JoinUserToATenantAsync(string userCode, string tenantCode);

    Task<ApplicationUserDto?> AuthenticateAsync(string email, string password, Guid? tenantId = null);
    Task<IdentityResult> GeneratePasswordResetTokenAsync(string email);
    Task<IdentityResult> ResetPasswordAsync(string email, string resetToken, string newPassword);
    Task<bool> SendEmailConfirmationAsync(ApplicationUserDto userDto, HttpRequest request);
    Task<IEnumerable<UserRolesEnum>> GetInternalRolesAsync(ApplicationUserDto user);
    Task<IEnumerable<UserRolesEnum>> GetTenantRolesAsync(ApplicationUserDto user);
    Task ConfirmEmailAsync(string token, string email);

    Task<IEnumerable<UserTenantStripDto>> GetUserTenantsAssociationsAsync(ApplicationUserDto user);

    /// <summary>
    /// Use ASP.NET Identity's PasswordHasher to hash the password
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    string HashPassword(ApplicationUserDto user, string password);

    /// <summary>
    /// Verify if the provided password matches the hashed password
    /// </summary>
    /// <param name="user"></param>
    /// <param name="hashedPassword"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    bool VerifyPassword(ApplicationUserDto user, string hashedPassword, string password);
    Task<bool> IsUserMemberOf(string? userCode, string tenantCode);
}
