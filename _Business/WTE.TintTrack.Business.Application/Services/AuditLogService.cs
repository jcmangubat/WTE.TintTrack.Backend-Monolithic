using AutoMapper;
using Microsoft.Extensions.Logging;
using SMEAppHouse.Core.CodeKits.Helpers;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Application.Shared.ServiceAbstractions;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;

namespace WTE.TintTrack.Business.Application.Services;

/// <summary>
/// Provides business logic related to audit logs.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AuditLogService"/> class.
/// </remarks>
/// <param name="auditLogRepository">The repository for audit log data access.</param>
public class AuditLogService(IMapper mapper,
                    ILogger<AuditLogService> logger,
                    IMessageProviderService messageProviderService,
                    IAuditLogRepository auditLogRepository)
    : MappedLoggingService<IAuditLogService>(mapper, logger, messageProviderService), IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository = auditLogRepository;

    /// <inheritdoc />
    public async Task<AuditLogDto?> GetByIdAsync(Guid id)
    {
        try
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(id);
            var auditLogDto = Mapper.Map<AuditLogDto>(auditLog);
            return auditLogDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLogDto>> GetByIdsAsync(IEnumerable<Guid> auditLogIds)
    {
        try
        {
            var auditLogs = await _auditLogRepository.GetByIdsAsync(auditLogIds);
            var auditLogsDto = Mapper.Map<List<AuditLogDto>>(auditLogs);
            return auditLogsDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<AuditLogDto>> GetByUserAsync(Guid userId)
    {
        try
        {
            var auditLogs = await _auditLogRepository.GetByUserAsync(userId);
            var auditLogsDto = Mapper.Map<List<AuditLogDto>>(auditLogs);
            return auditLogsDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid auditLogId)
    {
        try
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(auditLogId);
            if (auditLog == null)
            {
                var apiMsg = MessageProviderService.GetMessage("ERR047");
                throw new KeyNotFoundException(apiMsg.Message.Replace("{{auditLogId}}", auditLogId.ToString())); //"Audit log with id {auditLogId} not found."
            }

            await _auditLogRepository.DeleteAsync(auditLogId);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }

    /// <inheritdoc />
    public async Task<AuditLogDto> CreateLog(AuditLogDto auditLogDto)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(auditLogDto);

            var auditLog = Mapper.Map<AuditLog>(auditLogDto);

            await _auditLogRepository.AddAsync(auditLog);
            await _auditLogRepository.CommitAsync();

            auditLogDto = Mapper.Map<AuditLogDto>(auditLog);
            return auditLogDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.GetExceptionMessages());
            throw Default(ex);
        }
    }
}
