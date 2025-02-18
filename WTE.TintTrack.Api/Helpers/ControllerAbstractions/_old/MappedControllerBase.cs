using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WTE.TintTrack.Api.Helpers.ControllerAbstractions;

public class MappedControllerBase<TController>(IMapper mapper) : ControllerBase
{
    protected IMapper Mapper { get; } = mapper
        ?? throw new ArgumentNullException(nameof(mapper));
}
