using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Core.Infrastructure.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ApplicationUser?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == userId);
        //return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(p => p.Email == email);
        //return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> AnyAsync(Expression<Func<ApplicationUser, bool>> whereExpression)
    {
        return await _dbContext.Users.AnyAsync(whereExpression);
    }

    public async Task CreateAsync(ApplicationUser user, string password)
    {
        // Validate the user
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        // Ensure the user does not already exist
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
            throw new Exception("A user with the same email already exists.");

        // Hash the password (use a hashing library such as BCrypt.Net or ASP.NET Identity's PasswordHasher)
        var passwordHasher = _userManager.PasswordHasher;
        user.PasswordHash = passwordHasher.HashPassword(user, password);

        // Add the user to the database
        _dbContext.Users.Add(user);
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        // Validate the user
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Update the user 
        _dbContext.Users.Update(user);
    }

    public async Task CommitChangesAsync() { 
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ApplicationUser user)
    {
        // Validate the user
        ArgumentNullException.ThrowIfNull(user);

        // Add the user to the database
        await _dbContext.Users
                .Where(p => p.Id == user.Id)
                .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync(bool? activeOnly = null)
    {
        var users = await _dbContext.Users
                        .Include(p => p.UserTenants)
                            .ThenInclude(p => p.Tenant)
                        .Where(p => activeOnly == null || (p.IsActive ?? true) == activeOnly.Value)
                        .ToListAsync();
        return users;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>> entityPredicate)
    {
        var users = await _dbContext.Users
                        .Include(p => p.UserTenants)
                            .ThenInclude(p => p.Tenant)
                        .Where(entityPredicate)
                        .ToListAsync();
        return users;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllByTenantAsync(string tenantCode, bool? activeOnly = null)
    {
        var users = await _dbContext.Users
                        .Include(p => p.UserTenants)
                            .ThenInclude(p => p.Tenant)
                        .Where(p => p.UserTenants != null &&
                                p.UserTenants.Any(t => t.Tenant.TenantCode == tenantCode) &&
                                (activeOnly == null || (p.IsActive ?? true) == activeOnly.Value))
                        .ToListAsync();
        return users;
    }

    public async Task<ApplicationUser?> GetByUserCodeAsync(string userCode)
    {
        return await _dbContext.Users
                        .Include(p => p.UserTenants)
                            .ThenInclude(p => p.Tenant).AsNoTracking()
                        .FirstOrDefaultAsync(p => p.UserCode == userCode);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllByTenantAsync(Guid tenantId, bool? activeOnly = null)
    {
        var users = await _dbContext.Users
                        .Include(p => p.UserTenants)
                            .ThenInclude(p => p.Tenant)
                        .Where(p => p.UserTenants != null &&
                                p.UserTenants.Any(t => t.Tenant.Id == tenantId) &&
                                (activeOnly == null || (p.IsActive ?? true) == activeOnly.Value))
                        .ToListAsync();

        return users;
    }
}
