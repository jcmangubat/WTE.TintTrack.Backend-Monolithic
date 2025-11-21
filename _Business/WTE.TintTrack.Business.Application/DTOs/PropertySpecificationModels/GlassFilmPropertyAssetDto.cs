using static WTE.TintTrack.Common.Constants.Consts;

namespace WTE.TintTrack.Business.Application.DTOs.PropertySpecificationModels;

public class GlassFilmPropertyAssetDto : PropertyAssetDto
{
    public override sealed required PropertyTypesEnum PropertyType { get; set; } = PropertyTypesEnum.GlassFilm;
    public string FilmType { get; set; }  // Type of film applied (e.g., UV blocking, heat-reflective)
    public double FilmThickness { get; set; }  // Thickness of the film in mils (thousandths of an inch)
    public bool IsTinted { get; set; }  // Is the glass film tinted?
}
