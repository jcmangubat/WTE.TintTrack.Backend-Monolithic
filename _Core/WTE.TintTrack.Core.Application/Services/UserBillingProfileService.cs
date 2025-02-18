using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Common.Exceptions;
using WTE.TintTrack.Core.Application.DTOs.CoreEntityRelated;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using WTE.TintTrack.Core.Domain.Interfaces.Repositories;
using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Core.Application.Services;

public class UserBillingProfileService(IMapper mapper,
                    ILogger<UserBillingProfileService> logger,
                    IMessageProviderService messageProviderService,
                    IUserRepository userRepository,
                    IUserBillingProfileRepository userBillingProfileRepository)
    : MappedLoggingService<IUserBillingProfileService>(mapper, logger, messageProviderService), IUserBillingProfileService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserBillingProfileRepository _userBillingProfileRepository = userBillingProfileRepository;

    /// <inheritdoc />
    public async Task DeleteBillingProfileAsync(Guid profileId)
    {
        try
        {
            await _userBillingProfileRepository.DeleteAsync(profileId);
            await _userBillingProfileRepository.CommitAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<UserBillingProfileDto?> GetBillingProfileByIdAsync(Guid userBillingProfileId)
    {
        try
        {
            var billingProfile = await _userBillingProfileRepository.GetSingleAsync(p => p.Id == userBillingProfileId) ??
                            throw RecordNotFoundException("ERR087");

            var billingProfileDto = Mapper.Map<UserBillingProfileDto>(billingProfile);
            return billingProfileDto;
        }
        catch (RecordNotFoundException ex)
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

    /// <inheritdoc />
    public async Task<UserBillingProfileDto> GetActiveBillingProfileByUserCodeAsync(string userCode, BillingProfileTypesEnum? billingProfileType = null)
    {
        try
        {
            var billingProfile = await _userBillingProfileRepository.GetSingleAsync(p => p.User.UserCode == userCode &&
                                                                                            (billingProfileType == null || (billingProfileType != null && p.BillingProfileType == billingProfileType)) &&
                                                                                            (p.IsActive ?? false),
                                                                                            include: p => p.Include(x => x.User))
                ?? throw RecordNotFoundException("ERR088");

            var billingProfileDto = Mapper.Map<UserBillingProfileDto>(billingProfile);
            return billingProfileDto;
        }
        catch (RecordNotFoundException ex)
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

    /// <inheritdoc />
    public async Task<IEnumerable<UserBillingProfileDto>> GetBillingProfilesByUserCodeAsync(string userCode, BillingProfileTypesEnum? billingProfileType = null, ActiveInclusionOptionsEnum? activeInclusionOption = null)
    {
        try
        {
            IEnumerable<UserBillingProfile> billingProfiles = await _userBillingProfileRepository.GetListAsync(p => p.User.UserCode == userCode &&
                (billingProfileType == null || (billingProfileType != null && p.BillingProfileType == billingProfileType)) &&
                (activeInclusionOption == null || (activeInclusionOption != null && ((activeInclusionOption == ActiveInclusionOptionsEnum.ALL) || p.IsActive == (activeInclusionOption == ActiveInclusionOptionsEnum.ACTIVE_ONLY)))),
                                        include: p => p.Include(x => x.User));

            if (billingProfiles == null || !billingProfiles.Any())
                throw RecordNotFoundException("ERR088");

            var billingProfilesDto = Mapper.Map<IEnumerable<UserBillingProfileDto>>(billingProfiles);
            return billingProfilesDto;
        }
        catch (RecordNotFoundException ex)
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

    /// <inheritdoc />
    public async Task<UserBillingProfileDto> RegisterBillingProfileAsync(UserBillingProfileDto billingProfileDto)
    {
        try
        {
            var userDto = await _userRepository.GetByUserCodeAsync(billingProfileDto.UserCode) ??
                            throw RecordNotFoundException("ERR064");

            var billingProfile = Mapper.Map<UserBillingProfile>(billingProfileDto);
            billingProfile.UserId = userDto.Id;

            await _userBillingProfileRepository.AddAsync(billingProfile);
            await _userBillingProfileRepository.CommitAsync();

            billingProfileDto = Mapper.Map<UserBillingProfileDto>(billingProfile);
            return billingProfileDto;
        }
        catch (RecordNotFoundException ex)
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
}
