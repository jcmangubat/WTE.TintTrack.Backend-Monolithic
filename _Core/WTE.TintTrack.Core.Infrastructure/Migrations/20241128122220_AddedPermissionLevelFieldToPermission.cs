using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPermissionLevelFieldToPermission : Migration
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
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 470, DateTimeKind.Local).AddTicks(3684),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 747, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 472, DateTimeKind.Local).AddTicks(1883),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 749, DateTimeKind.Local).AddTicks(1273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 461, DateTimeKind.Local).AddTicks(1631),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 738, DateTimeKind.Local).AddTicks(4311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 468, DateTimeKind.Local).AddTicks(4429),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 746, DateTimeKind.Local).AddTicks(731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 451, DateTimeKind.Local).AddTicks(5441),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 728, DateTimeKind.Local).AddTicks(9611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 462, DateTimeKind.Local).AddTicks(7070),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 740, DateTimeKind.Local).AddTicks(800));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 466, DateTimeKind.Local).AddTicks(9324),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 744, DateTimeKind.Local).AddTicks(6096));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 464, DateTimeKind.Local).AddTicks(4156),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 742, DateTimeKind.Local).AddTicks(3187));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 459, DateTimeKind.Local).AddTicks(4954),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 736, DateTimeKind.Local).AddTicks(7204));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 453, DateTimeKind.Local).AddTicks(6088),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 731, DateTimeKind.Local).AddTicks(898));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 455, DateTimeKind.Local).AddTicks(6337),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 732, DateTimeKind.Local).AddTicks(7554));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 458, DateTimeKind.Local).AddTicks(805),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 735, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 442, DateTimeKind.Local).AddTicks(4993),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 720, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 439, DateTimeKind.Local).AddTicks(6724),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 716, DateTimeKind.Local).AddTicks(2721));

            migrationBuilder.AddColumn<int>(
                name: "PermissionLevel",
                schema: "dbo",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 447, DateTimeKind.Local).AddTicks(5965),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 725, DateTimeKind.Local).AddTicks(1474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 407, DateTimeKind.Utc).AddTicks(4235),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 2, 54, 657, DateTimeKind.Utc).AddTicks(2335));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 404, DateTimeKind.Utc).AddTicks(3130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 2, 54, 654, DateTimeKind.Utc).AddTicks(8786));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionLevel",
                schema: "dbo",
                table: "Permissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 747, DateTimeKind.Local).AddTicks(6424),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 470, DateTimeKind.Local).AddTicks(3684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 749, DateTimeKind.Local).AddTicks(1273),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 472, DateTimeKind.Local).AddTicks(1883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 738, DateTimeKind.Local).AddTicks(4311),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 461, DateTimeKind.Local).AddTicks(1631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 746, DateTimeKind.Local).AddTicks(731),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 468, DateTimeKind.Local).AddTicks(4429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 728, DateTimeKind.Local).AddTicks(9611),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 451, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 740, DateTimeKind.Local).AddTicks(800),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 462, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 744, DateTimeKind.Local).AddTicks(6096),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 466, DateTimeKind.Local).AddTicks(9324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 742, DateTimeKind.Local).AddTicks(3187),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 464, DateTimeKind.Local).AddTicks(4156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 736, DateTimeKind.Local).AddTicks(7204),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 459, DateTimeKind.Local).AddTicks(4954));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 731, DateTimeKind.Local).AddTicks(898),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 453, DateTimeKind.Local).AddTicks(6088));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 732, DateTimeKind.Local).AddTicks(7554),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 455, DateTimeKind.Local).AddTicks(6337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 735, DateTimeKind.Local).AddTicks(2731),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 458, DateTimeKind.Local).AddTicks(805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 720, DateTimeKind.Local).AddTicks(4906),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 442, DateTimeKind.Local).AddTicks(4993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 716, DateTimeKind.Local).AddTicks(2721),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 439, DateTimeKind.Local).AddTicks(6724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 2, 54, 725, DateTimeKind.Local).AddTicks(1474),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 447, DateTimeKind.Local).AddTicks(5965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 2, 54, 657, DateTimeKind.Utc).AddTicks(2335),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 407, DateTimeKind.Utc).AddTicks(4235));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 2, 54, 654, DateTimeKind.Utc).AddTicks(8786),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 404, DateTimeKind.Utc).AddTicks(3130));
        }
    }
}
