using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SMEAppHouse.Core.CodeKits;
using SMEAppHouse.Core.CodeKits.Extensions;
using SMEAppHouse.Core.CodeKits.Helpers;
using System.Linq.Expressions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Constants;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Common.Helpers;
using WTE.TintTrack.Common.Models;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;
using ValidationException = FluentValidation.ValidationException;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace WTE.TintTrack.Core.Application.Services;

public class UserService(IMapper mapper,
                    ILogger<UserService> logger,
                    IOptions<ApplicationSettings> appSettings,
                    IMessageProviderService messageProviderService,
                    IValidator<ApplicationUserDto> userRegistrationValidator,

                    IUserRepository userRepository,
                    ITenantRepository tenantRepository,
                    IUserTenantRepository userTenantRepository,
                    IUserTenantInvitationRepository userTenantInvitationRepository,
                    ISubscriptionPlanRepository subscriptionPlanRepository,
                    UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager,
                    RoleManager<ApplicationRole> roleManager,

                    IImageKitUploadService imageKitUploadService,
                    IEmailSenderService emailSenderService,
                    ITenantSubscriptionService tenantSubscriptionService)

    : MappedLoggingService<IUserService>(mapper, logger, messageProviderService), IUserService
{
    private readonly PasswordHasher<ApplicationUser> _passwordHasher = new();

    private readonly ApplicationSettings _appSettings = appSettings.Value;

    private readonly IValidator<ApplicationUserDto> _userRegistrationValidator = userRegistrationValidator;

    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IUserTenantRepository _userTenantRepository = userTenantRepository;
    private readonly IUserTenantInvitationRepository _userTenantInvitationRepository = userTenantInvitationRepository;
    private readonly ISubscriptionPlanRepository _subscriptionPlanRepository = subscriptionPlanRepository; // Repository for subscription plans

    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    private readonly IImageKitUploadService _imageKitUploadService = imageKitUploadService;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly ITenantSubscriptionService _tenantSubscriptionService = tenantSubscriptionService;

    public async Task<string> UploadUserProfileImage(string userCode, IFormFile? avatarFormFile)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                ?? throw new RecordNotFoundException("No user found associated with the given code");

            if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                await _imageKitUploadService.DeleteFileAsync(user.ProfileImageUrl);

            var cdnUrlPath = string.Empty;
            var uploadFolderPath = _appSettings.ImgKitUserAvatarsPath;

            if (avatarFormFile != null)
            {
                cdnUrlPath = await _imageKitUploadService.UploadFileAsync(avatarFormFile, uploadFolderPath ?? string.Empty);
                user.ProfileImageUrl = cdnUrlPath;
            }
            else user.ProfileImageUrl = string.Empty;

            await _userRepository.UpdateAsync(user);
            await _userRepository.CommitChangesAsync();

            return cdnUrlPath;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeleteAsync(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw RecordNotFoundException("ERR083");

            if (user != null)
                await _userRepository.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task DeleteByUserCodeAsync(string userCode)
    {
        try
        {
            var user = await _userRepository.GetByUserCodeAsync(userCode)
                ?? throw RecordNotFoundException("ERR064");

            // Other validations required :
            // - prevent deletion of user when user is a tenant/account owner and an invoice is still not closed

            if (user != null)
                await _userRepository.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllAsync(bool? activeOnly = null)
    {
        try
        {
            var users = await _userRepository.GetAllAsync(activeOnly);
            var usersDto = Mapper.Map<List<ApplicationUserDto>>(users);
            return usersDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllAsync(Expression<Func<ApplicationUserDto, bool>> dtoPredicate)
    {
        try
        {
            var converter = new PredicateConverter<ApplicationUser, ApplicationUserDto>();
            Expression<Func<ApplicationUser, bool>> entityPredicate = converter.Convert(dtoPredicate);
            var users = await _userRepository.GetAllAsync(entityPredicate);
            var usersDto = Mapper.Map<List<ApplicationUserDto>>(users);
            return usersDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllByTenantAsync(string tenantCode, bool? activeOnly = null)
    {
        try
        {
            ValidateTenantCode(tenantCode);

            var users = await _userRepository.GetAllByTenantAsync(tenantCode, activeOnly);
            var usersDto = Mapper.Map<List<ApplicationUserDto>>(users);
            return usersDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<ApplicationUserDto>> GetAllByTenantAsync(Guid tenantId, bool? activeOnly = null)
    {
        try
        {
            var users = await _userRepository.GetAllByTenantAsync(tenantId, activeOnly);
            var usersDto = Mapper.Map<List<ApplicationUserDto>>(users);
            return usersDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<ApplicationUserDto?> GetByEmailAsync(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            var userDto = Mapper.Map<ApplicationUserDto>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<ApplicationUserDto?> GetByUserCodeAsync(string userCode)
    {
        try
        {
            ValidateUserCode(userCode);

            var user = await _userRepository.GetByUserCodeAsync(userCode)
                ?? throw RecordNotFoundException("ERR064");

            var userDto = Mapper.Map<ApplicationUserDto>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<ApplicationUserDto?> GetByIdAsync(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var userDto = Mapper.Map<ApplicationUserDto>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IEnumerable<TenantDto>> GetTenantsForUserAsync(string userCode, bool? activesOnly = null)
    {
        var user = await _userRepository.GetByUserCodeAsync(userCode)
            ?? throw RecordNotFoundException("ERR064"); ;

        return await GetTenantsForUserAsync(user.Id, activesOnly);
    }

    public async Task<IEnumerable<TenantDto>> GetTenantsForUserAsync(Guid userId, bool? activesOnly = null)
    {
        try
        {
            var tenants = await _tenantRepository.GetTenantsForUserAsync(userId, activesOnly);
            return Mapper.Map<IEnumerable<TenantDto>>(tenants);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task UpdateAsync(ApplicationUserDto userDto)
    {
        try
        {
            var user = Mapper.Map<ApplicationUser>(userDto);
            await _userRepository.UpdateAsync(user);
            await _userRepository.CommitChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<bool> UserExistsAsync(Guid tenantId, string email)
    {
        try
        {
            // Check if the user exists in the specified tenant
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            // Check if the user is associated with the specified tenant
            return await _userTenantRepository.UserExistsInTenantAsync(user.Id, tenantId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<IdentityResult> ResetPasswordAsync(string email, string resetToken, string newPassword)
    {
        try
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(email);

            // Reset the user's password
            return await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<ApplicationUserDto?> AuthenticateAsync(string email, string password, Guid? tenantId = null)
    {
        try
        {
            // Find the user by email
            ApplicationUser user = await _userManager.FindByEmailAsync(email)
                ?? throw RecordNotFoundException("ERR073");

            if (tenantId.HasValue && !await _userTenantRepository.UserExistsInTenantAsync(user.Id, tenantId.Value))
                throw RecordNotFoundException("ERR072");

            // Check password
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw ServiceOperationException("ERR004");

            var userDto = Mapper.Map<ApplicationUserDto>(user);
            return result.Succeeded ? userDto : null; // Return user if authenticated
        }
        catch (RecordNotFoundException ex)
        {
            // Log and rethrow specific exceptions
            Logger.LogWarning(ex.Message);
            throw;
        }
        catch (ServiceOperationException ex)
        {
            // Log and rethrow specific exceptions
            Logger.LogWarning(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <summary>
    /// Registers a user to a new or an existing tenant
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="RecordNotFoundException"></exception>
    /// <exception cref="ServiceOperationException"></exception>
    /// <exception cref="ApplicationException"></exception>
    public async Task<(ApplicationUserDto User, TenantDto Tenant)> RegisterUserAsync(ApplicationUserDto applicationUserDto,
                                                            TenantEntryDto tenantEntry, string password)
    {
        try
        {
            var validationFailures = new List<ValidationFailure>();

            // 1. Validate input data 
            ValidationResult? validationResult = await _userRegistrationValidator.ValidateAsync(applicationUserDto);
            if (!validationResult.IsValid)
                validationFailures.AddRange(validationResult.Errors);

            // 2. Check if the email already exists
            if (await _userRepository.AnyAsync(p => p.Email == applicationUserDto.Email))
                validationFailures.Add(new ValidationFailure() { PropertyName = "email", ErrorMessage = "Email is already registered." });

            // 3. Validate tenant entry data
            if (tenantEntry == null ||
                (string.IsNullOrEmpty(tenantEntry.TenantCode) && string.IsNullOrEmpty(tenantEntry.TenantName)))
                validationFailures.Add(new ValidationFailure() { ErrorMessage = "Either a code or a name of a tenant must be provided." });

            if (validationFailures != null && validationFailures.Count > 0)
                throw new ValidationException(validationFailures);

            // 4. Handle Tenant Logic
            Tenant? tenant = null;
            SubscriptionPlan? subscriptionPlan = null;
            ApplicationRole? userRole = null;

            var joiningTenant = false;
            var creatingTenant = false;

            // Check if tenant code is provided
            if (!string.IsNullOrEmpty(tenantEntry?.TenantCode))
            {
                joiningTenant = true;
                tenant = await _tenantRepository.GetSingleAsync(p => p.TenantCode == tenantEntry.TenantCode)
                    ?? throw new ValidationException([new ValidationFailure("tenantCode", "Tenant code is invalid or non-existent.")]);
                userRole = await _roleManager.FindByNameAsync(UserRolesEnum.TenantViewer.ToString());
            }
            // Check if tenant name is provided (for new tenant)
            else if (!string.IsNullOrEmpty(tenantEntry?.TenantName))
            {
                creatingTenant = true;

                var tenantName = tenantEntry?.TenantName;
                tenant = await _tenantRepository.GetSingleAsync(p => p.Name.ToLower() == tenantName.ToLower());

                if (tenant == null)
                {
                    var tenantCode = CodeGenerator.GenerateUniqueCode(tenantEntry.TenantName, FieldLengths.Tenant.TenantCode);
                    tenant = new Tenant
                    {
                        Id = Guid.NewGuid(),
                        Name = tenantEntry.TenantName,
                        Description = tenantEntry.TenantName,
                        TenantCode = tenantCode,
                        TenantStatus = TenantStatusEnum.PendingApproval
                    };
                    await _tenantRepository.AddAsync(tenant); // Create new tenant
                    await _tenantRepository.CommitAsync();
                }

                /* create tenant subscription plan since this is a new tenant */
                // get the default subscription plan which is free
                subscriptionPlan = await _subscriptionPlanRepository.GetSingleAsync(p => p.Level == 0);

                userRole = await _roleManager.FindByNameAsync(UserRolesEnum.TenantOwner.ToString());
            }

            // 5. Create User
            var makeCodeResult = await TryMakeCodeAsync();
            var newUser = new ApplicationUser
            {
                Id = makeCodeResult.UserId,
                UserCode = makeCodeResult.UserCode,
                Email = applicationUserDto.Email,
                EmailConfirmed = false, // Set to true after email confirmation
                NormalizedEmail = applicationUserDto.Email.ToUpper(),
                UserName = applicationUserDto.Email,
                NormalizedUserName = applicationUserDto.Email.ToUpper(), // Normalize username
                PhoneNumber = applicationUserDto.PhoneNumber, // Set phone number
                PhoneNumberConfirmed = false, // Confirm after verification (if applicable)
                SecurityStamp = Guid.NewGuid().ToString(), // Generate a unique security stamp
                ConcurrencyStamp = Guid.NewGuid().ToString(), // Helps in optimistic concurrency control
                LockoutEnabled = false, // Enable lockout after failed login attempts
                AccessFailedCount = 0 // Initialize to 0 failed attempts
            };
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);

            var identityResult = await _userManager.CreateAsync(newUser);

            if (!identityResult.Succeeded)
            {
                var errsDic = new Dictionary<string, string[]>();
                foreach (var error in identityResult.Errors)
                {
                    errsDic.Add(error.Code, [error.Description]);
                }
                throw new ServiceOperationException("User creation failed.", errsDic);
            }

            // 6. process joining tenant
            if (joiningTenant)
            {
                var userTenantInvitation = new UserTenantInvitation
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = applicationUserDto.Email,
                    InvitationStatus = TenantInvitationStatusEnum.Pending,
                    InvitationSource = InvitationSourcesEnum.FromUser,
                    UserId = newUser.Id,
                    TenantId = tenant.Id
                };
                await _userTenantInvitationRepository.AddAsync(userTenantInvitation);
                await _userTenantInvitationRepository.CommitAsync();
            }

            // 7. Associate user to tenant created
            var userTenant = new UserTenant()
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                UserId = newUser.Id,
                IsDefault = creatingTenant,
                IsActive = joiningTenant ? false : true, // not active yet until admin or tenant joining has approved the join request.
            };
            await _userTenantRepository.AddAsync(userTenant);
            await _userTenantRepository.CommitAsync();

            // 8. Add the TenantOwner role to the user tenant record
            await _userTenantRepository.AssignRoleToUserInTenantAsync(newUser.Id, userTenant.TenantId, userRole.Id);
            await _userTenantRepository.CommitAsync();

            // 9. Register subscription for the user if this is a new tenant
            if (subscriptionPlan != null)
            {
                var tenantSubscriptionDto = new TenantSubscriptionDto
                {
                    Id = Guid.NewGuid(),
                    SubscriptionPlanId = subscriptionPlan.Id,
                    SubscriptionStatus = SubscriptionStatusEnum.ForReview,
                    TenantId = tenant.Id
                };

                await _tenantSubscriptionService.RegisterTenantSubscriptionAsync(tenantSubscriptionDto);
            }

            // 10. Return success result
            return (Mapper.Map<ApplicationUserDto>(newUser), Mapper.Map<TenantDto>(tenant));
        }
        catch (RecordNotFoundException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (ValidationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <summary>
    /// Request for a user to join an existing tenant
    /// </summary>
    public async Task JoinUserToATenantAsync(string userCode, string tenantCode)
    {
        try
        {
            ApplicationUser? user = null;
            Tenant? tenant = null;

            var errors = new List<ValidationFailure>();

            if (!string.IsNullOrEmpty(userCode))
            {
                user = await _userRepository.GetByUserCodeAsync(userCode);
                if (user == null)
                    errors.Add(new ValidationFailure("userCode", "No user found given the code."));
            }
            else errors.Add(new ValidationFailure("userCode", "User code required."));

            if (!string.IsNullOrEmpty(tenantCode))
            {
                tenant = await _tenantRepository.GetByTenantCodeAsync(tenantCode);
                if (tenant == null)
                    errors.Add(new ValidationFailure("tenantDto", "No tenant found given the code."));
            }
            else errors.Add(new ValidationFailure("tenantCode", "Tenant code required."));

            if (errors.Count > 0)
                throw new ValidationException(errors);

            var userTenant = await _userTenantRepository.GetSingleAsync(p => p.User.UserCode == userCode && p.Tenant.TenantCode == tenantCode,
                                                                        p => p.Include(x => x.User).Include(x => x.Tenant));
            if (userTenant != null)
                throw new ServiceOperationException($"Cannot request to join a tenant you are already a member of or own.");

            var userTenantInvitation = await _userTenantInvitationRepository.GetSingleAsync(
                                                                                p => p.User.UserCode == userCode &&
                                                                                        p.Tenant.TenantCode == tenantCode,
                                                                                p => p.Include(x => x.User).Include(x => x.Tenant)
                                                                            );
            if (userTenantInvitation != null)
                throw new ServiceOperationException($"Request to join tenant {tenantCode} for user {userCode} is already active.");

            userTenantInvitation = new UserTenantInvitation
            {
                Id = Guid.NewGuid(),
                EmailAddress = user.Email,
                InvitationStatus = TenantInvitationStatusEnum.Pending,
                InvitationSource = InvitationSourcesEnum.FromUser,
                UserId = user.Id,
                TenantId = tenant.Id
            };
            await _userTenantInvitationRepository.AddAsync(userTenantInvitation);
            await _userTenantInvitationRepository.CommitAsync();

            // 7. Associate user to tenant 
            userTenant = new UserTenant()
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                UserId = user.Id,
                IsDefault = false,
                IsActive = false, // not active yet until admin or tenant joining has approved the join request.
            };
            await _userTenantRepository.AddAsync(userTenant);
            await _userTenantRepository.CommitAsync();

            // 8. Add the TenantViewer role to the user tenant record
            var userRole = await _roleManager.FindByNameAsync(UserRolesEnum.TenantViewer.ToString());
            await _userTenantRepository.AssignRoleToUserInTenantAsync(user.Id, userTenant.TenantId, userRole.Id);
            await _userTenantRepository.CommitAsync();
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (ValidationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    public async Task<bool> SendEmailConfirmationAsync(ApplicationUserDto userDto, HttpRequest request)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(userDto);

            // Generate email confirmation token
            var user = Mapper.Map<ApplicationUser>(userDto);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Generate the confirmation link using the request to get scheme and host
            var confirmationLink = GenerateEmailConfirmationLink(user, token, request);

            // Call the email sending service to send the link
            var apiMsgConfirmEmail = MessageProviderService.GetMessage("INF013").Message;

            var apiMsgConfirmEmailContent = MessageProviderService.GetMessage("INF014", 
                                                templateVals: new() { { "{{confirmationLink}}", confirmationLink } }).Message;

            var apiMsgConfirmEmailContentHtml = MessageProviderService.GetMessage("INF015",
                                                templateVals: new() { { "{{confirmationLink}}", confirmationLink } }).Message;

            await _emailSenderService.SendEmailAsync(
                _appSettings.NoReplyEmailAddress,
                apiMsgConfirmEmail, apiMsgConfirmEmailContent,
                apiMsgConfirmEmailContentHtml,
                [new EmailContact() { EmailAddress = userDto.Email }]);

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            return false;
        }
    }

    public async Task<IEnumerable<UserRolesEnum>> GetInternalRolesAsync(ApplicationUserDto user)
    {
        var globalRoles = Enum.GetValues(typeof(UserRolesEnum))
                                .Cast<UserRolesEnum>()
                                .Where(r => r.IsRoleInternal())
                                .Select(r => r);

        var existingRoles = await _userManager.GetRolesAsync(Mapper.Map<ApplicationUser>(user));
        var theRoles = globalRoles.Where(role => existingRoles.Any(gr => role.ToString().EqualsCaseInsensitive(gr)));

        return theRoles;
    }

    public async Task<IEnumerable<UserRolesEnum>> GetTenantRolesAsync(ApplicationUserDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserTenantStripDto>> GetUserTenantsAssociationsAsync(ApplicationUserDto user)
    {
        var userTenants = await _userTenantRepository.GetListAsync(p => p.UserId == user.Id,
                                                                    p => p.Include(x => x.Tenant));
        var userTenantStrips = userTenants.Select(p => new UserTenantStripDto()
        {
            IsDefault = p.IsDefault,
            UserIsOwner = p.UserIsOwner,
            TenantCode = p.Tenant.TenantCode,
            Description = p.Tenant.Description,
            Name = p.Tenant.Name,
            Status = p.Tenant.TenantStatus,
            StatusText = $"Tenant status is {p.Tenant.TenantStatus.ToString()}"
        }).ToList();
        return userTenantStrips;
    }

    public async Task ConfirmEmailAsync(string token, string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw ServiceOperationException("ERR089");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                var errorsDic = result.Errors
                                        .GroupBy(p => p.Code)
                                        .ToDictionary(
                                            group => group.Key,              // Use the error code as the dictionary key
                                            group => group.Select(p => p.Description).ToArray() // Collect descriptions as an array
                                        );

                throw new ServiceOperationException("Email confirmation failed.", errorsDic);
            }
            //throw new ServiceOperationException("");
        }
        catch (ServiceOperationException ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }


    public async Task<IdentityResult> GeneratePasswordResetTokenAsync(string email)
    {
        try
        {
            if (!CodeKit.IsValidEmail(email))
                throw new ValidationException([new ValidationFailure(nameof(email), "Email address is invalid")]);

            ApplicationUser user = await _userManager.FindByEmailAsync(email) ??
                       throw RecordNotFoundException("ERR010");

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //TODO: Refactor to move to Controller.
            //Send an email with a link containing the token
            var resetLink = $"https://wte-tinttrack-backend-dev.azurewebsites.net/reset-password?token={token}&email={email}";
            var apiMsg = MessageProviderService.GetMessage("INF016");
            var subjectTemplate = apiMsg.Message;
            await _emailSenderService.SendEmailAsync(
                         _appSettings.NoReplyEmailAddress,
                        subjectTemplate, resetLink, $"<a>{resetLink}</a>",
                        [new EmailContact() { EmailAddress = email }]);

            return IdentityResult.Success;
        }
        catch (ValidationException ex)
        {
            Logger.LogWarning(ex, ex.GetExceptionMessages());
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw;
        }

    }

    /// <summary>
    /// Use ASP.NET Identity's PasswordHasher to hash the password
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public string HashPassword(ApplicationUserDto user, string password)
    {
        var applicationUser = Mapper.Map<ApplicationUser>(user);
        return _passwordHasher.HashPassword(applicationUser, password);
    }

    /// <summary>
    /// Verify if the provided password matches the hashed password
    /// </summary>
    /// <param name="user"></param>
    /// <param name="hashedPassword"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool VerifyPassword(ApplicationUserDto user, string hashedPassword, string password)
    {
        var applicationUser = Mapper.Map<ApplicationUser>(user);
        var result = _passwordHasher.VerifyHashedPassword(applicationUser, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

    public async Task<bool> IsUserMemberOf(string? userCode, string tenantCode)
    {
        var userTenant = await _userTenantRepository.GetSingleAsync(p => p.User.UserCode == userCode && p.Tenant.TenantCode == tenantCode,
                p => p.Include(x => x.User).Include(x => x.Tenant));
        return userTenant != null;
    }

    #region private methods

    private static string GenerateEmailConfirmationLink(ApplicationUser user, string token, HttpRequest request)
    {
        // First, try to use Referer for the full base URL
        var baseUrl = request.Headers["Referer"].ToString();

        if (string.IsNullOrEmpty(baseUrl))
        {
            // Fallback to Origin if Referer is not available
            baseUrl = request.Headers["Origin"].ToString();
        }

        if (string.IsNullOrEmpty(baseUrl))
        {
            // If both Referer and Origin are unavailable, use a known default URL
            baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
        }

        // Ensure the email and token are URL encoded
        var encodedEmail = Uri.EscapeDataString(user.Email);
        var encodedToken = Uri.EscapeDataString(token);

        // Construct the confirmation link
        var confirmationLink = $"{baseUrl}/confirm-email?token={encodedToken}&email={encodedEmail}";
        return confirmationLink;
    }

    private async Task<(Guid UserId, string UserCode)> TryMakeCodeAsync()
    {
        var newUserId = Guid.NewGuid();
        var newUserCode = CodeGenerator.GenerateUniqueCode(newUserId.ToString(), FieldLengths.ApplicationUser.UserCode);

        if (await _userRepository.AnyAsync(p => p.UserCode == newUserCode))
            return await TryMakeCodeAsync();

        return new(newUserId, newUserCode);
    }

    #endregion
}