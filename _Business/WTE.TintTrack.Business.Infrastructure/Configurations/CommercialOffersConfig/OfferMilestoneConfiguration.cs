using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;

public class OfferMilestoneConfiguration(string schema = "dbo")
    : EntityConfiguration<OfferMilestone, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<OfferMilestone> entityBuilder)
    {
        base.OnModelCreating(entityBuilder);

        entityBuilder.DefineDbField(p => p.Code, true, FieldLengths.General.CODE);
        entityBuilder.DefineDbField(p => p.Name, true, FieldLengths.General.Name);
        entityBuilder.DefineDbField(p => p.Description, false, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.ExpectedStartDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.ExpectedEndDate, false, "datetime2");
        entityBuilder.DefineDbField(p => p.EstimatedAmount, false, "decimal(18,2)");
        
    }
}