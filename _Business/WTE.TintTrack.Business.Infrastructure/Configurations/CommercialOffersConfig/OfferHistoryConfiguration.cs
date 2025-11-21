using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;
using WTE.TintTrack.Common.Constants;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;

public class OfferHistoryConfiguration(string schema = "dbo")
    : EntityConfiguration<OfferHistory, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<OfferHistory> entityBuilder)
    {
        entityBuilder.DefineDbField(p => p.OfferDocumentStatus, true);
        entityBuilder.DefineDbField(p => p.Comments, false, FieldLengths.General.SummaryParagraph);
        entityBuilder.DefineDbField(p => p.ChangedByUserCode, true, FieldLengths.General.CODE);
    }
}