namespace WTE.TintTrack.Application.Shared.Interfaces;

public interface ITenantProviderService
{
    Task<string?> GetTenantCodeAsync();

    Task<string?> GetTenantSQLDbConnectionAsync();
}
