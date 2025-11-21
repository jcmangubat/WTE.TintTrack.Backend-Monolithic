using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkedServicesAndMaterialsToOffers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItem_TintMaterials_ProductId",
                schema: "dbo",
                table: "InventoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "ProposalItem");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "dbo",
                table: "InventoryItem",
                newName: "TintMaterialId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItem_ProductId",
                schema: "dbo",
                table: "InventoryItem",
                newName: "IX_InventoryItem_TintMaterialId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 175, DateTimeKind.Local).AddTicks(6391),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 676, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 186, DateTimeKind.Local).AddTicks(8953),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 689, DateTimeKind.Local).AddTicks(4420));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 199, DateTimeKind.Local).AddTicks(9700),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 705, DateTimeKind.Local).AddTicks(4663));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 193, DateTimeKind.Local).AddTicks(5614),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 697, DateTimeKind.Local).AddTicks(2254));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 191, DateTimeKind.Local).AddTicks(8696),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 694, DateTimeKind.Local).AddTicks(8862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 190, DateTimeKind.Local).AddTicks(8989),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 693, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 202, DateTimeKind.Local).AddTicks(4707),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 708, DateTimeKind.Local).AddTicks(2982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 211, DateTimeKind.Local).AddTicks(1144),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 713, DateTimeKind.Local).AddTicks(9109));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 165, DateTimeKind.Local).AddTicks(5614),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 670, DateTimeKind.Local).AddTicks(7596));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 234, DateTimeKind.Local).AddTicks(3941),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 728, DateTimeKind.Local).AddTicks(7627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 237, DateTimeKind.Local).AddTicks(8671),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 731, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 181, DateTimeKind.Local).AddTicks(5134),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 683, DateTimeKind.Local).AddTicks(7423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 206, DateTimeKind.Local).AddTicks(9810),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 711, DateTimeKind.Local).AddTicks(564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 154, DateTimeKind.Local).AddTicks(1855),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 660, DateTimeKind.Local).AddTicks(1664));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 163, DateTimeKind.Local).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 668, DateTimeKind.Local).AddTicks(7455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 219, DateTimeKind.Local).AddTicks(3421),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 719, DateTimeKind.Local).AddTicks(1462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 161, DateTimeKind.Local).AddTicks(1548),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 666, DateTimeKind.Local).AddTicks(1436));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 148, DateTimeKind.Local).AddTicks(1396),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 656, DateTimeKind.Local).AddTicks(408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 171, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 675, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "InventoryItem",
                column: "TintMaterialId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "ProposalItem",
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
                name: "FK_InventoryItem_TintMaterials_TintMaterialId",
                schema: "dbo",
                table: "InventoryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "ProposalItem");

            migrationBuilder.RenameColumn(
                name: "TintMaterialId",
                schema: "dbo",
                table: "InventoryItem",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItem_TintMaterialId",
                schema: "dbo",
                table: "InventoryItem",
                newName: "IX_InventoryItem_ProductId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 676, DateTimeKind.Local).AddTicks(7932),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 175, DateTimeKind.Local).AddTicks(6391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 689, DateTimeKind.Local).AddTicks(4420),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 186, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 705, DateTimeKind.Local).AddTicks(4663),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 199, DateTimeKind.Local).AddTicks(9700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 697, DateTimeKind.Local).AddTicks(2254),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 193, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 694, DateTimeKind.Local).AddTicks(8862),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 191, DateTimeKind.Local).AddTicks(8696));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 693, DateTimeKind.Local).AddTicks(8300),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 190, DateTimeKind.Local).AddTicks(8989));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 708, DateTimeKind.Local).AddTicks(2982),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 202, DateTimeKind.Local).AddTicks(4707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 713, DateTimeKind.Local).AddTicks(9109),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 211, DateTimeKind.Local).AddTicks(1144));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 670, DateTimeKind.Local).AddTicks(7596),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 165, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 728, DateTimeKind.Local).AddTicks(7627),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 234, DateTimeKind.Local).AddTicks(3941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 731, DateTimeKind.Local).AddTicks(5927),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 237, DateTimeKind.Local).AddTicks(8671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 683, DateTimeKind.Local).AddTicks(7423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 181, DateTimeKind.Local).AddTicks(5134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 711, DateTimeKind.Local).AddTicks(564),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 206, DateTimeKind.Local).AddTicks(9810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 660, DateTimeKind.Local).AddTicks(1664),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 154, DateTimeKind.Local).AddTicks(1855));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 668, DateTimeKind.Local).AddTicks(7455),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 163, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 719, DateTimeKind.Local).AddTicks(1462),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 219, DateTimeKind.Local).AddTicks(3421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 666, DateTimeKind.Local).AddTicks(1436),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 161, DateTimeKind.Local).AddTicks(1548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 656, DateTimeKind.Local).AddTicks(408),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 148, DateTimeKind.Local).AddTicks(1396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 55, 47, 675, DateTimeKind.Local).AddTicks(1007),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 171, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItem_TintMaterials_ProductId",
                schema: "dbo",
                table: "InventoryItem",
                column: "ProductId",
                principalSchema: "dbo",
                principalTable: "TintMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItem_TintServices_TintServiceId",
                schema: "dbo",
                table: "ProposalItem",
                column: "TintServiceId",
                principalSchema: "dbo",
                principalTable: "TintServices",
                principalColumn: "Id");
        }
    }
}
