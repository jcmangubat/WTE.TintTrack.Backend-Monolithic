using SMEAppHouse.Core.Patterns.Repo.Paging.Interface;

namespace WTE.TintTrack.Application.Shared.Messaging;

public class DefaultApiResponsePaged<T>(T data, IPageRequest pageRequest, int totalRecords, int totalPages) 
    : DefaultApiResponse<T>(data), IPageResult
{
    public IPageRequest PageRequest { get; set; } = pageRequest;
    public long TotalRecords { get; set; } = totalRecords;
    public int TotalPages { get; set; } = totalPages;
}