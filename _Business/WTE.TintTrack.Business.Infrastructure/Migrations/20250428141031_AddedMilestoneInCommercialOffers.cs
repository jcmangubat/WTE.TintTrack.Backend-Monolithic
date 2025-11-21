using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMilestoneInCommercialOffers : Migration
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
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 821, DateTimeKind.Local).AddTicks(4862),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 766, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 826, DateTimeKind.Local).AddTicks(6439),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 773, DateTimeKind.Local).AddTicks(1273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 835, DateTimeKind.Local).AddTicks(4273),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 782, DateTimeKind.Local).AddTicks(165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 830, DateTimeKind.Local).AddTicks(6636),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 777, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 829, DateTimeKind.Local).AddTicks(4339),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 776, DateTimeKind.Local).AddTicks(464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 828, DateTimeKind.Local).AddTicks(8061),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 775, DateTimeKind.Local).AddTicks(4119));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 836, DateTimeKind.Local).AddTicks(8011),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 783, DateTimeKind.Local).AddTicks(1867));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 840, DateTimeKind.Local).AddTicks(5746),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 786, DateTimeKind.Local).AddTicks(3933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 816, DateTimeKind.Local).AddTicks(5602),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 760, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 824, DateTimeKind.Local).AddTicks(2244),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 770, DateTimeKind.Local).AddTicks(7739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 838, DateTimeKind.Local).AddTicks(7386),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 784, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 808, DateTimeKind.Local).AddTicks(9927),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 752, DateTimeKind.Local).AddTicks(5697));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 815, DateTimeKind.Local).AddTicks(1259),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 758, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 813, DateTimeKind.Local).AddTicks(4133),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 757, DateTimeKind.Local).AddTicks(807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 805, DateTimeKind.Local).AddTicks(2363),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 748, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 820, DateTimeKind.Local).AddTicks(3928),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 764, DateTimeKind.Local).AddTicks(9427));

            migrationBuilder.CreateTable(
                name: "OfferMilestone",
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferMilestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferMilestone_Estimates_EstimateId",
                        column: x => x.EstimateId,
                        principalSchema: "dbo",
                        principalTable: "Estimates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferMilestone_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalSchema: "dbo",
                        principalTable: "Proposals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferMilestone_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalSchema: "dbo",
                        principalTable: "Quotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferMilestone_EstimateId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferMilestone_ProposalId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferMilestone_QuoteId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "QuoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferMilestone",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 766, DateTimeKind.Local).AddTicks(1610),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 821, DateTimeKind.Local).AddTicks(4862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 773, DateTimeKind.Local).AddTicks(1273),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 826, DateTimeKind.Local).AddTicks(6439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 782, DateTimeKind.Local).AddTicks(165),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 835, DateTimeKind.Local).AddTicks(4273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 777, DateTimeKind.Local).AddTicks(3564),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 830, DateTimeKind.Local).AddTicks(6636));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 776, DateTimeKind.Local).AddTicks(464),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 829, DateTimeKind.Local).AddTicks(4339));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 775, DateTimeKind.Local).AddTicks(4119),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 828, DateTimeKind.Local).AddTicks(8061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 783, DateTimeKind.Local).AddTicks(1867),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 836, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 786, DateTimeKind.Local).AddTicks(3933),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 840, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 760, DateTimeKind.Local).AddTicks(3346),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 816, DateTimeKind.Local).AddTicks(5602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 770, DateTimeKind.Local).AddTicks(7739),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 824, DateTimeKind.Local).AddTicks(2244));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 784, DateTimeKind.Local).AddTicks(7797),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 838, DateTimeKind.Local).AddTicks(7386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 752, DateTimeKind.Local).AddTicks(5697),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 808, DateTimeKind.Local).AddTicks(9927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 758, DateTimeKind.Local).AddTicks(7806),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 815, DateTimeKind.Local).AddTicks(1259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 757, DateTimeKind.Local).AddTicks(807),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 813, DateTimeKind.Local).AddTicks(4133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 748, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 805, DateTimeKind.Local).AddTicks(2363));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 764, DateTimeKind.Local).AddTicks(9427),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 10, 30, 820, DateTimeKind.Local).AddTicks(3928));
        }
    }
}
