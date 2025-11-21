using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkedServicesAndMaterialsToOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "ProposalItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "QuoteItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "QuoteItem");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 676, DateTimeKind.Local).AddTicks(7932),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 638, DateTimeKind.Local).AddTicks(5318));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 689, DateTimeKind.Local).AddTicks(4420),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 643, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 705, DateTimeKind.Local).AddTicks(4663),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 653, DateTimeKind.Local).AddTicks(2056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 697, DateTimeKind.Local).AddTicks(2254),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 648, DateTimeKind.Local).AddTicks(4383));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 694, DateTimeKind.Local).AddTicks(8862),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 647, DateTimeKind.Local).AddTicks(1381));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 693, DateTimeKind.Local).AddTicks(8300),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 646, DateTimeKind.Local).AddTicks(2828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 708, DateTimeKind.Local).AddTicks(2982),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 656, DateTimeKind.Local).AddTicks(177));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 713, DateTimeKind.Local).AddTicks(9109),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 662, DateTimeKind.Local).AddTicks(6691));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 670, DateTimeKind.Local).AddTicks(7596),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 632, DateTimeKind.Local).AddTicks(8574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 728, DateTimeKind.Local).AddTicks(7627),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 678, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 731, DateTimeKind.Local).AddTicks(5927),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 681, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 683, DateTimeKind.Local).AddTicks(7423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 641, DateTimeKind.Local).AddTicks(3175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 711, DateTimeKind.Local).AddTicks(564),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 659, DateTimeKind.Local).AddTicks(5640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 660, DateTimeKind.Local).AddTicks(1664),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 624, DateTimeKind.Local).AddTicks(9588));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 668, DateTimeKind.Local).AddTicks(7455),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 631, DateTimeKind.Local).AddTicks(4667));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 719, DateTimeKind.Local).AddTicks(1462),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 668, DateTimeKind.Local).AddTicks(7285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 666, DateTimeKind.Local).AddTicks(1436),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 629, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 656, DateTimeKind.Local).AddTicks(408),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 621, DateTimeKind.Local).AddTicks(509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 675, DateTimeKind.Local).AddTicks(1007),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 636, DateTimeKind.Local).AddTicks(9926));

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintServiceId",
                principalSchema: "dbo",
                principalTable: "TintServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "ProposalItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintServiceId",
                principalSchema: "dbo",
                principalTable: "TintServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "EstimateItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "ProposalItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "QuoteItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuoteItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "QuoteItem");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 638, DateTimeKind.Local).AddTicks(5318),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 676, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 643, DateTimeKind.Local).AddTicks(7970),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 689, DateTimeKind.Local).AddTicks(4420));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 653, DateTimeKind.Local).AddTicks(2056),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 705, DateTimeKind.Local).AddTicks(4663));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 648, DateTimeKind.Local).AddTicks(4383),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 697, DateTimeKind.Local).AddTicks(2254));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 647, DateTimeKind.Local).AddTicks(1381),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 694, DateTimeKind.Local).AddTicks(8862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 646, DateTimeKind.Local).AddTicks(2828),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 693, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 656, DateTimeKind.Local).AddTicks(177),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 708, DateTimeKind.Local).AddTicks(2982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 662, DateTimeKind.Local).AddTicks(6691),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 713, DateTimeKind.Local).AddTicks(9109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 632, DateTimeKind.Local).AddTicks(8574),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 670, DateTimeKind.Local).AddTicks(7596));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 678, DateTimeKind.Local).AddTicks(7650),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 728, DateTimeKind.Local).AddTicks(7627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 681, DateTimeKind.Local).AddTicks(8465),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 731, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 641, DateTimeKind.Local).AddTicks(3175),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 683, DateTimeKind.Local).AddTicks(7423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 659, DateTimeKind.Local).AddTicks(5640),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 711, DateTimeKind.Local).AddTicks(564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 624, DateTimeKind.Local).AddTicks(9588),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 660, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 631, DateTimeKind.Local).AddTicks(4667),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 668, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 668, DateTimeKind.Local).AddTicks(7285),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 719, DateTimeKind.Local).AddTicks(1462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 629, DateTimeKind.Local).AddTicks(6261),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 666, DateTimeKind.Local).AddTicks(1436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 621, DateTimeKind.Local).AddTicks(509),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 656, DateTimeKind.Local).AddTicks(408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 36, 18, 636, DateTimeKind.Local).AddTicks(9926),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 675, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "EstimateItem",
                column: "TintServiceId",
                principalSchema: "dbo",
                principalTable: "TintServices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "ProposalItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "QuoteItem",
                column: "TintServiceId",
                principalSchema: "dbo",
                principalTable: "TintServices",
                principalColumn: "Id");
        }
    }
}
