using AutoMapper;
using Microsoft.Extensions.Logging;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Common.Exceptions;

namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public abstract class MappedLoggingService<TService>(IMapper mapper, ILogger<TService> logger, IMessageProviderService messageProviderService)
    : IMappedLoggingService<TService>
    where TService : class
{

    public IMapper Mapper { get; } = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public ILogger<TService> Logger { get; } = logger
        ?? throw new ArgumentNullException(nameof(logger));

    protected IMessageProviderService MessageProviderService = messageProviderService
        ?? throw new ArgumentNullException(nameof(messageProviderService));

    protected void ValidateTenantCode(string tenantCode)
    {
        if (string.IsNullOrEmpty(tenantCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR037");
            throw new CustomValidationException(apiMsg.Message);
        }
    }

    protected void ValidateUserCode(string userCode)
    {
        if (string.IsNullOrEmpty(userCode))
        {
            var apiMsg = MessageProviderService.GetMessage("ERR038");
            throw new CustomValidationException(apiMsg.Message); //"User code is required."
        }
    }

    protected Exception Default(Exception ex, string? errMsg = null)
    {
        if (!string.IsNullOrEmpty(errMsg))
        {
            return new ApplicationException(errMsg, ex);
        }
        else
        {
            var apiMsg = MessageProviderService.GetMessage("ERR000");
            return new ApplicationException(apiMsg.Message, ex);
        }
    }

    protected RecordNotFoundException RecordNotFoundException(string errorCode, Dictionary<string, string>? templateValueOps = null)
    {
        var apiMsg = MessageProviderService.GetMessage(errorCode);
        if (templateValueOps != null && templateValueOps.Keys.Count > 0)
        {
            foreach (var key in templateValueOps.Keys)
            {
                apiMsg.Message = apiMsg.Message.Replace(key, templateValueOps[key]);
            }
        }
        return new RecordNotFoundException(apiMsg.Code, apiMsg.Message);
    }

    protected ServiceOperationException ServiceOperationException(string errorCode, Dictionary<string, string>? templateValueOps = null)
    {
        var apiMsg = MessageProviderService.GetMessage(errorCode);
        if (templateValueOps != null && templateValueOps.Keys.Count > 0)
        {
            foreach (var key in templateValueOps.Keys)
            {
                apiMsg.Message = apiMsg.Message.Replace(key, templateValueOps[key]);
            }
        }
        return new ServiceOperationException(apiMsg.Code, apiMsg.Message);
    }
    protected CustomSecurityTokenException CustomSecurityTokenException(string errorCode, Dictionary<string, string>? templateValueOps = null)
    {
        var apiMsg = MessageProviderService.GetMessage(errorCode);
        if (templateValueOps != null && templateValueOps.Keys.Count > 0)
        {
            foreach (var key in templateValueOps.Keys)
            {
                apiMsg.Message = apiMsg.Message.Replace(key, templateValueOps[key]);
            }
        }
        return new CustomSecurityTokenException(apiMsg.Message, apiMsg.Code);
    }

    protected CustomInvalidOperationException CustomInvalidOperationException(string errorCode, Dictionary<string, string>? templateValueOps = null)
    {
        var apiMsg = MessageProviderService.GetMessage(errorCode);
        if (templateValueOps != null && templateValueOps.Keys.Count > 0)
        {
            foreach (var key in templateValueOps.Keys)
            {
                apiMsg.Message = apiMsg.Message.Replace(key, templateValueOps[key]);
            }
        }
        return new CustomInvalidOperationException(apiMsg.Message, apiMsg.Code);
    }

    protected CustomKeyNotFoundException CustomKeyNotFoundException(string errorCode, Dictionary<string, string>? templateValueOps = null)
    {
        var apiMsg = MessageProviderService.GetMessage(errorCode);
        if (templateValueOps != null && templateValueOps.Keys.Count > 0)
        {
            foreach (var key in templateValueOps.Keys)
            {
                apiMsg.Message = apiMsg.Message.Replace(key, templateValueOps[key]);
            }
        }
        return new CustomKeyNotFoundException(apiMsg.Message, apiMsg.Code);
    }
}