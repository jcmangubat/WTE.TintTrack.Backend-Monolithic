using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RevisedApplicationPermissionFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 709, DateTimeKind.Local).AddTicks(1056),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 940, DateTimeKind.Local).AddTicks(1649));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 710, DateTimeKind.Local).AddTicks(6771),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 941, DateTimeKind.Local).AddTicks(9218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 701, DateTimeKind.Local).AddTicks(3525),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 930, DateTimeKind.Local).AddTicks(9623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 707, DateTimeKind.Local).AddTicks(5966),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 938, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 692, DateTimeKind.Local).AddTicks(490),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 919, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 702, DateTimeKind.Local).AddTicks(9397),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 933, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 706, DateTimeKind.Local).AddTicks(4466),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 937, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 704, DateTimeKind.Local).AddTicks(6294),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 935, DateTimeKind.Local).AddTicks(3914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 699, DateTimeKind.Local).AddTicks(5393),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 928, DateTimeKind.Local).AddTicks(988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 694, DateTimeKind.Local).AddTicks(1161),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 921, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 695, DateTimeKind.Local).AddTicks(7548),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 923, DateTimeKind.Local).AddTicks(3875));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 698, DateTimeKind.Local).AddTicks(1265),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 926, DateTimeKind.Local).AddTicks(1345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 682, DateTimeKind.Local).AddTicks(3303),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 910, DateTimeKind.Local).AddTicks(320));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 679, DateTimeKind.Local).AddTicks(473),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 906, DateTimeKind.Local).AddTicks(8982));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Permissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Permissions",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 688, DateTimeKind.Local).AddTicks(3572),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 916, DateTimeKind.Local).AddTicks(4653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 36, 34, 631, DateTimeKind.Utc).AddTicks(8193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 862, DateTimeKind.Utc).AddTicks(907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 36, 34, 629, DateTimeKind.Utc).AddTicks(2237),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 859, DateTimeKind.Utc).AddTicks(806));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Permissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 940, DateTimeKind.Local).AddTicks(1649),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 709, DateTimeKind.Local).AddTicks(1056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 941, DateTimeKind.Local).AddTicks(9218),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 710, DateTimeKind.Local).AddTicks(6771));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 930, DateTimeKind.Local).AddTicks(9623),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 701, DateTimeKind.Local).AddTicks(3525));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 938, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 707, DateTimeKind.Local).AddTicks(5966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 919, DateTimeKind.Local).AddTicks(4787),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 692, DateTimeKind.Local).AddTicks(490));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 933, DateTimeKind.Local).AddTicks(1149),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 702, DateTimeKind.Local).AddTicks(9397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 937, DateTimeKind.Local).AddTicks(2127),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 706, DateTimeKind.Local).AddTicks(4466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 935, DateTimeKind.Local).AddTicks(3914),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 704, DateTimeKind.Local).AddTicks(6294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 928, DateTimeKind.Local).AddTicks(988),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 699, DateTimeKind.Local).AddTicks(5393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 921, DateTimeKind.Local).AddTicks(6161),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 694, DateTimeKind.Local).AddTicks(1161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 923, DateTimeKind.Local).AddTicks(3875),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 695, DateTimeKind.Local).AddTicks(7548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 926, DateTimeKind.Local).AddTicks(1345),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 698, DateTimeKind.Local).AddTicks(1265));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 910, DateTimeKind.Local).AddTicks(320),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 682, DateTimeKind.Local).AddTicks(3303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 906, DateTimeKind.Local).AddTicks(8982),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 679, DateTimeKind.Local).AddTicks(473));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Permissions",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 916, DateTimeKind.Local).AddTicks(4653),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 36, 34, 688, DateTimeKind.Local).AddTicks(3572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 862, DateTimeKind.Utc).AddTicks(907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 36, 34, 631, DateTimeKind.Utc).AddTicks(8193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 859, DateTimeKind.Utc).AddTicks(806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 36, 34, 629, DateTimeKind.Utc).AddTicks(2237));
        }
    }
}
