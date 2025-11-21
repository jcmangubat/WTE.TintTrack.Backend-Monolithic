using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecipientsInCommercialOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 593, DateTimeKind.Local).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 821, DateTimeKind.Local).AddTicks(4862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 598, DateTimeKind.Local).AddTicks(5342),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 826, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 607, DateTimeKind.Local).AddTicks(8556),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 835, DateTimeKind.Local).AddTicks(4273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 603, DateTimeKind.Local).AddTicks(368),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 830, DateTimeKind.Local).AddTicks(6636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 601, DateTimeKind.Local).AddTicks(6237),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 829, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 600, DateTimeKind.Local).AddTicks(7071),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 828, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 609, DateTimeKind.Local).AddTicks(3619),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 836, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 613, DateTimeKind.Local).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 840, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 588, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 816, DateTimeKind.Local).AddTicks(5602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 596, DateTimeKind.Local).AddTicks(3091),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 824, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 611, DateTimeKind.Local).AddTicks(6277),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 838, DateTimeKind.Local).AddTicks(7386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 580, DateTimeKind.Local).AddTicks(6936),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 808, DateTimeKind.Local).AddTicks(9927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 587, DateTimeKind.Local).AddTicks(622),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 815, DateTimeKind.Local).AddTicks(1259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 585, DateTimeKind.Local).AddTicks(845),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 813, DateTimeKind.Local).AddTicks(4133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 576, DateTimeKind.Local).AddTicks(7136),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 805, DateTimeKind.Local).AddTicks(2363));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 592, DateTimeKind.Local).AddTicks(2898),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 820, DateTimeKind.Local).AddTicks(3928));

            migrationBuilder.CreateTable(
                name: "OfferRecipient",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    OfferDocumentRecipientRole = table.Column<int>(type: "int", nullable: false),
                    CustomerContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferRecipient_CustomerContacts_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalSchema: "dbo",
                        principalTable: "CustomerContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferRecipient_Estimates_EstimateId",
                        column: x => x.EstimateId,
                        principalSchema: "dbo",
                        principalTable: "Estimates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferRecipient_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalSchema: "dbo",
                        principalTable: "Proposals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferRecipient_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalSchema: "dbo",
                        principalTable: "Quotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfferHistory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    OfferDocumentStatus = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedByUserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferRecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferHistory_OfferRecipient_OfferRecipientId",
                        column: x => x.OfferRecipientId,
                        principalSchema: "dbo",
                        principalTable: "OfferRecipient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferHistory_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistory",
                column: "OfferRecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRecipient_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "CustomerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRecipient_EstimateId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRecipient_ProposalId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRecipient_QuoteId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "QuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferHistory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OfferRecipient",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 821, DateTimeKind.Local).AddTicks(4862),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 593, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 826, DateTimeKind.Local).AddTicks(6439),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 598, DateTimeKind.Local).AddTicks(5342));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 835, DateTimeKind.Local).AddTicks(4273),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 607, DateTimeKind.Local).AddTicks(8556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 830, DateTimeKind.Local).AddTicks(6636),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 603, DateTimeKind.Local).AddTicks(368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 829, DateTimeKind.Local).AddTicks(4339),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 601, DateTimeKind.Local).AddTicks(6237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 828, DateTimeKind.Local).AddTicks(8061),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 600, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 836, DateTimeKind.Local).AddTicks(8011),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 609, DateTimeKind.Local).AddTicks(3619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 840, DateTimeKind.Local).AddTicks(5746),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 613, DateTimeKind.Local).AddTicks(9039));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 816, DateTimeKind.Local).AddTicks(5602),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 588, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 824, DateTimeKind.Local).AddTicks(2244),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 596, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 838, DateTimeKind.Local).AddTicks(7386),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 611, DateTimeKind.Local).AddTicks(6277));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 808, DateTimeKind.Local).AddTicks(9927),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 580, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 815, DateTimeKind.Local).AddTicks(1259),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 587, DateTimeKind.Local).AddTicks(622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 813, DateTimeKind.Local).AddTicks(4133),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 585, DateTimeKind.Local).AddTicks(845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 805, DateTimeKind.Local).AddTicks(2363),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 576, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 820, DateTimeKind.Local).AddTicks(3928),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 592, DateTimeKind.Local).AddTicks(2898));
        }
    }
}
