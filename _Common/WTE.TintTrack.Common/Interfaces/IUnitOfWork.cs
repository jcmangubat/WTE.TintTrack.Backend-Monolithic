namespace WTE.TintTrack.Common.Interfaces;

public interface IUnitOfWork<TDbContext>
{
    Task SaveChangesAsync();
}