using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CommercialOfferLineItems : Migration
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
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 766, DateTimeKind.Local).AddTicks(1610),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 51, DateTimeKind.Local).AddTicks(4925));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 773, DateTimeKind.Local).AddTicks(1273),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 57, DateTimeKind.Local).AddTicks(2734));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 782, DateTimeKind.Local).AddTicks(165),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 69, DateTimeKind.Local).AddTicks(1424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 777, DateTimeKind.Local).AddTicks(3564),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 61, DateTimeKind.Local).AddTicks(9389));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 776, DateTimeKind.Local).AddTicks(464),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 60, DateTimeKind.Local).AddTicks(4924));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 775, DateTimeKind.Local).AddTicks(4119),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 59, DateTimeKind.Local).AddTicks(7720));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 783, DateTimeKind.Local).AddTicks(1867),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 70, DateTimeKind.Local).AddTicks(8380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 786, DateTimeKind.Local).AddTicks(3933),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 75, DateTimeKind.Local).AddTicks(1376));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 760, DateTimeKind.Local).AddTicks(3346),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 45, DateTimeKind.Local).AddTicks(7472));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 770, DateTimeKind.Local).AddTicks(7739),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 54, DateTimeKind.Local).AddTicks(5778));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 784, DateTimeKind.Local).AddTicks(7797),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 72, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 752, DateTimeKind.Local).AddTicks(5697),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 37, DateTimeKind.Local).AddTicks(8929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 758, DateTimeKind.Local).AddTicks(7806),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 44, DateTimeKind.Local).AddTicks(2089));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 757, DateTimeKind.Local).AddTicks(807),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 42, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 748, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 33, DateTimeKind.Local).AddTicks(9372));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 764, DateTimeKind.Local).AddTicks(9427),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 50, DateTimeKind.Local).AddTicks(1880));

            migrationBuilder.CreateTable(
                name: "EstimateItem",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimateItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimateItem_Estimates_EstimateId",
                        column: x => x.EstimateId,
                        principalSchema: "dbo",
                        principalTable: "Estimates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstimateItem_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstimateItem_TintServices_TintServiceId",
                        column: x => x.TintServiceId,
                        principalSchema: "dbo",
                        principalTable: "TintServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProposalItem",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalItem_Proposals_ProposalId",
                        column: x => x.ProposalId,
                        principalSchema: "dbo",
                        principalTable: "Proposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalItem_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProposalItem_TintServices_TintServiceId",
                        column: x => x.TintServiceId,
                        principalSchema: "dbo",
                        principalTable: "TintServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuoteItem",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteItem_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalSchema: "dbo",
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteItem_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteItem_TintServices_TintServiceId",
                        column: x => x.TintServiceId,
                        principalSchema: "dbo",
                        principalTable: "TintServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstimateItem_EstimateId",
                schema: "dbo",
                table: "EstimateItem",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateItem_TintMaterialId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateItem_TintServiceId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalItem_ProposalId",
                schema: "dbo",
                table: "ProposalItem",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalItem_TintMaterialId",
                schema: "dbo",
                table: "ProposalItem",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalItem_TintServiceId",
                schema: "dbo",
                table: "ProposalItem",
                column: "TintServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItem_QuoteId",
                schema: "dbo",
                table: "QuoteItem",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItem_TintMaterialId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItem_TintServiceId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstimateItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProposalItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "QuoteItem",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 51, DateTimeKind.Local).AddTicks(4925),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 766, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 57, DateTimeKind.Local).AddTicks(2734),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 773, DateTimeKind.Local).AddTicks(1273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 69, DateTimeKind.Local).AddTicks(1424),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 782, DateTimeKind.Local).AddTicks(165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 61, DateTimeKind.Local).AddTicks(9389),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 777, DateTimeKind.Local).AddTicks(3564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 60, DateTimeKind.Local).AddTicks(4924),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 776, DateTimeKind.Local).AddTicks(464));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 59, DateTimeKind.Local).AddTicks(7720),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 775, DateTimeKind.Local).AddTicks(4119));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 70, DateTimeKind.Local).AddTicks(8380),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 783, DateTimeKind.Local).AddTicks(1867));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 75, DateTimeKind.Local).AddTicks(1376),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 786, DateTimeKind.Local).AddTicks(3933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 45, DateTimeKind.Local).AddTicks(7472),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 760, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 54, DateTimeKind.Local).AddTicks(5778),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 770, DateTimeKind.Local).AddTicks(7739));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 72, DateTimeKind.Local).AddTicks(9910),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 784, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 37, DateTimeKind.Local).AddTicks(8929),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 752, DateTimeKind.Local).AddTicks(5697));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 44, DateTimeKind.Local).AddTicks(2089),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 758, DateTimeKind.Local).AddTicks(7806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 42, DateTimeKind.Local).AddTicks(4609),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 757, DateTimeKind.Local).AddTicks(807));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 33, DateTimeKind.Local).AddTicks(9372),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 748, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 50, DateTimeKind.Local).AddTicks(1880),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 21, 55, 35, 764, DateTimeKind.Local).AddTicks(9427));
        }
    }
}
