namespace WTE.TintTrack.Application.Shared.ServiceAbstractions;

public interface ICodedEntityService<TEntityDto>
    where TEntityDto : class
{
    Task<TEntityDto?> GetByCodeAsync(string code);
}