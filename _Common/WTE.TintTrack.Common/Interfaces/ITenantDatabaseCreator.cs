namespace WTE.TintTrack.Common.Interfaces;

public interface ITenantDatabaseCreator
{
    Task CreateDatabaseAsync(string connectionString);
}
