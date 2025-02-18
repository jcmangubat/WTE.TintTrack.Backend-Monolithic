using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedToRefreshTokenExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Expiration",
                schema: "dbo",
                table: "Tokens",
                newName: "RefreshTokenExpiration");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 583, DateTimeKind.Local).AddTicks(9155),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 282, DateTimeKind.Local).AddTicks(1181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 585, DateTimeKind.Local).AddTicks(5048),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 283, DateTimeKind.Local).AddTicks(5539));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 576, DateTimeKind.Local).AddTicks(4400),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 274, DateTimeKind.Local).AddTicks(9371));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 582, DateTimeKind.Local).AddTicks(4676),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 280, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 566, DateTimeKind.Local).AddTicks(8663),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 265, DateTimeKind.Local).AddTicks(3666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 578, DateTimeKind.Local).AddTicks(1801),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 276, DateTimeKind.Local).AddTicks(5297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 581, DateTimeKind.Local).AddTicks(4099),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 279, DateTimeKind.Local).AddTicks(6016));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 579, DateTimeKind.Local).AddTicks(8535),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 278, DateTimeKind.Local).AddTicks(1068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 574, DateTimeKind.Local).AddTicks(5775),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 273, DateTimeKind.Local).AddTicks(3380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 568, DateTimeKind.Local).AddTicks(9169),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 267, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 570, DateTimeKind.Local).AddTicks(4972),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 269, DateTimeKind.Local).AddTicks(3072));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 573, DateTimeKind.Local).AddTicks(1453),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 271, DateTimeKind.Local).AddTicks(6845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 563, DateTimeKind.Local).AddTicks(2414),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 262, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 560, DateTimeKind.Local).AddTicks(5210),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 259, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 2, 42, 25, 524, DateTimeKind.Utc).AddTicks(4124),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 240, DateTimeKind.Utc).AddTicks(5238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 4, 2, 42, 25, 522, DateTimeKind.Utc).AddTicks(577),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 237, DateTimeKind.Utc).AddTicks(9626));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiration",
                schema: "dbo",
                table: "Tokens",
                newName: "Expiration");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 282, DateTimeKind.Local).AddTicks(1181),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 583, DateTimeKind.Local).AddTicks(9155));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 283, DateTimeKind.Local).AddTicks(5539),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 585, DateTimeKind.Local).AddTicks(5048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 274, DateTimeKind.Local).AddTicks(9371),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 576, DateTimeKind.Local).AddTicks(4400));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 280, DateTimeKind.Local).AddTicks(7207),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 582, DateTimeKind.Local).AddTicks(4676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 265, DateTimeKind.Local).AddTicks(3666),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 566, DateTimeKind.Local).AddTicks(8663));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 276, DateTimeKind.Local).AddTicks(5297),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 578, DateTimeKind.Local).AddTicks(1801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 279, DateTimeKind.Local).AddTicks(6016),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 581, DateTimeKind.Local).AddTicks(4099));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 278, DateTimeKind.Local).AddTicks(1068),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 579, DateTimeKind.Local).AddTicks(8535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 273, DateTimeKind.Local).AddTicks(3380),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 574, DateTimeKind.Local).AddTicks(5775));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 267, DateTimeKind.Local).AddTicks(3413),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 568, DateTimeKind.Local).AddTicks(9169));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 269, DateTimeKind.Local).AddTicks(3072),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 570, DateTimeKind.Local).AddTicks(4972));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 271, DateTimeKind.Local).AddTicks(6845),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 573, DateTimeKind.Local).AddTicks(1453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 262, DateTimeKind.Local).AddTicks(5929),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 563, DateTimeKind.Local).AddTicks(2414));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 259, DateTimeKind.Local).AddTicks(1939),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 10, 42, 25, 560, DateTimeKind.Local).AddTicks(5210));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 240, DateTimeKind.Utc).AddTicks(5238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 2, 42, 25, 524, DateTimeKind.Utc).AddTicks(4124));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 237, DateTimeKind.Utc).AddTicks(9626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 12, 4, 2, 42, 25, 522, DateTimeKind.Utc).AddTicks(577));
        }
    }
}
