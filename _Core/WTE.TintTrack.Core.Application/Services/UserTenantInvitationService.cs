using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.Core.Application.Services;

public class UserTenantInvitationService(
                        IMapper mapper,
                        ILogger<UserTenantInvitationService> logger,
                        IMessageProviderService messageProviderService)
    : MappedLoggingService<IUserTenantInvitationService>(mapper, logger, messageProviderService), IUserTenantInvitationService
{
}