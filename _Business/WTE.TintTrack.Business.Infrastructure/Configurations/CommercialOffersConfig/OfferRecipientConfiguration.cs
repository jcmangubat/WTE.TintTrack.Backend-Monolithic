using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMEAppHouse.Core.Patterns.EF.EntityConfigurationAbstractions;
using SMEAppHouse.Core.Patterns.EF.Helpers;
using WTE.TintTrack.Business.Domain.Entities.CommercialOffersEntities;

namespace WTE.TintTrack.Business.Infrastructure.Configurations.CommercialOffersConfig;

public class OfferRecipientConfiguration(string schema = "dbo")
    : EntityConfiguration<OfferRecipient, Guid>(prefixEntityNameToId: false, prefixAltTblNameToEntity: false, schema: schema, pluralizeTblName: true)
{
    public override void OnModelCreating(EntityTypeBuilder<OfferRecipient> entityBuilder)
    {
        entityBuilder.DefineDbField(p => p.OfferDocumentRecipientRole, true);

        entityBuilder
            .HasMany(r => r.OfferHistories)
            .WithOne(h => h.OfferRecipient)
            .HasForeignKey(h => h.OfferRecipientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}