using SMEAppHouse.Core.Patterns.Repo.Paging;

namespace WTE.TintTrack.Application.Shared.ModelAbstraction;

public class PagedResultForDTO<TEntityDto> : PagedResultBase where TEntityDto : class
{
    public IEnumerable<TEntityDto> Data { get; set; } = Array.Empty<TEntityDto>();

}
