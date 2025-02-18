using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WTE.TintTrack.Api.Helpers.ControllerAbstractions;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Interfaces;

namespace WTE.TintTrack.Api.Controllers.Business;

[ApiController]
////[ApiExplorerSettings(GroupName = "businessmodules")]
[Route("api/[controller]")]
public class AuditLogsController(ILogger<AuditLogsController> logger,
IMapper mapper, IMessageProviderService messageProviderService,
IAuditLogService auditLogService)
    : LoggingMappedControllerBase<AuditLogsController>(logger, mapper, messageProviderService)
{
    private readonly IAuditLogService _auditLogService = auditLogService ?? throw new ArgumentNullException(nameof(auditLogService));

    /// <summary>
    /// Retrieves an audit log by its unique identifier.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AuditLogDto>> GetById(Guid id)
    {
        var auditLog = await _auditLogService.GetByIdAsync(id);
        if (auditLog == null)
        {
            Logger.LogWarning("Audit log with ID {Id} not found", id);
            return NotFound();
        }
        return Ok(auditLog);
    }

    /// <summary>
    /// Retrieves a collection of audit logs based on their identifiers.
    /// </summary>
    [HttpPost("by-ids")]
    public async Task<ActionResult<IEnumerable<AuditLogDto>>> GetByIds([FromBody] IEnumerable<Guid> auditLogIds)
    {
        if (auditLogIds == null)
        {
            return BadRequest("Audit log IDs are required.");
        }

        var auditLogs = await _auditLogService.GetByIdsAsync(auditLogIds);
        return Ok(auditLogs);
    }

    /// <summary>
    /// Retrieves a collection of audit logs associated with a specific user.
    /// </summary>
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<AuditLogDto>>> GetByUser(Guid userId)
    {
        var auditLogs = await _auditLogService.GetByUserAsync(userId);
        return Ok(auditLogs);
    }

    /// <summary>
    /// Deletes an audit log by its unique identifier.
    /// </summary>
    [HttpDelete("{auditLogId:guid}")]
    public async Task<IActionResult> Delete(Guid auditLogId)
    {
        await _auditLogService.DeleteAsync(auditLogId);
        Logger.LogInformation("Audit log with ID {AuditLogId} deleted", auditLogId);
        return NoContent();
    }

    /// <summary>
    /// Creates a new audit log entry.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AuditLogDto>> Create([FromBody] AuditLogDto auditLogDto)
    {
        if (auditLogDto == null)
            return BadRequest("Audit log data is required.");

        var createdAuditLog = await _auditLogService.CreateLog(auditLogDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAuditLog.Id }, createdAuditLog);
    }
}
