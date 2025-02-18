using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTblNamesforAppFeatureAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationFeatureRoleAccesses_ApplicationFeatures_ApplicationFeatureId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationFeatureRoleAccesses_AspNetRoles_RoleId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationFeatures_ApplicationFeatures_ParentId",
                schema: "dbo",
                table: "ApplicationFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationFeatures",
                schema: "dbo",
                table: "ApplicationFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationFeatureRoleAccesses",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses");

            migrationBuilder.RenameTable(
                name: "ApplicationFeatures",
                schema: "dbo",
                newName: "AppFeatures",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "ApplicationFeatureRoleAccesses",
                schema: "dbo",
                newName: "AppFeatureRoleAccesses",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationFeatures_ParentId",
                schema: "dbo",
                table: "AppFeatures",
                newName: "IX_AppFeatures_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationFeatureRoleAccesses_RoleId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                newName: "IX_AppFeatureRoleAccesses_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationFeatureRoleAccesses_ApplicationFeatureId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                newName: "IX_AppFeatureRoleAccesses_ApplicationFeatureId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 982, DateTimeKind.Local).AddTicks(1619),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 261, DateTimeKind.Local).AddTicks(4982));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 983, DateTimeKind.Local).AddTicks(9493),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 263, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 967, DateTimeKind.Local).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 252, DateTimeKind.Local).AddTicks(3226));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 979, DateTimeKind.Local).AddTicks(8781),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 259, DateTimeKind.Local).AddTicks(8795));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 954, DateTimeKind.Local).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 242, DateTimeKind.Local).AddTicks(3730));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 969, DateTimeKind.Local).AddTicks(733),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 253, DateTimeKind.Local).AddTicks(8617));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 978, DateTimeKind.Local).AddTicks(611),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 258, DateTimeKind.Local).AddTicks(20));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 972, DateTimeKind.Local).AddTicks(9159),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 255, DateTimeKind.Local).AddTicks(5440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 964, DateTimeKind.Local).AddTicks(3863),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 250, DateTimeKind.Local).AddTicks(6189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 956, DateTimeKind.Local).AddTicks(6336),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 244, DateTimeKind.Local).AddTicks(1424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 958, DateTimeKind.Local).AddTicks(6805),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 245, DateTimeKind.Local).AddTicks(7966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 962, DateTimeKind.Local).AddTicks(8572),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 249, DateTimeKind.Local).AddTicks(1650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 947, DateTimeKind.Local).AddTicks(5675),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 239, DateTimeKind.Local).AddTicks(991));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 943, DateTimeKind.Utc).AddTicks(4963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 36, 13, 236, DateTimeKind.Utc).AddTicks(8844));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 942, DateTimeKind.Utc).AddTicks(9447),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 36, 13, 236, DateTimeKind.Utc).AddTicks(6062));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AppFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 928, DateTimeKind.Local).AddTicks(5622),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 223, DateTimeKind.Local).AddTicks(3326));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 939, DateTimeKind.Local).AddTicks(5635),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 234, DateTimeKind.Local).AddTicks(811));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppFeatures",
                schema: "dbo",
                table: "AppFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppFeatureRoleAccesses",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatureRoleAccesses_AppFeatures_ApplicationFeatureId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                column: "ApplicationFeatureId",
                principalSchema: "dbo",
                principalTable: "AppFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatureRoleAccesses_AspNetRoles_RoleId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatures_AppFeatures_ParentId",
                schema: "dbo",
                table: "AppFeatures",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "AppFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatureRoleAccesses_AppFeatures_ApplicationFeatureId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatureRoleAccesses_AspNetRoles_RoleId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatures_AppFeatures_ParentId",
                schema: "dbo",
                table: "AppFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppFeatures",
                schema: "dbo",
                table: "AppFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppFeatureRoleAccesses",
                schema: "dbo",
                table: "AppFeatureRoleAccesses");

            migrationBuilder.RenameTable(
                name: "AppFeatures",
                schema: "dbo",
                newName: "ApplicationFeatures",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AppFeatureRoleAccesses",
                schema: "dbo",
                newName: "ApplicationFeatureRoleAccesses",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_AppFeatures_ParentId",
                schema: "dbo",
                table: "ApplicationFeatures",
                newName: "IX_ApplicationFeatures_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppFeatureRoleAccesses_RoleId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                newName: "IX_ApplicationFeatureRoleAccesses_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AppFeatureRoleAccesses_ApplicationFeatureId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                newName: "IX_ApplicationFeatureRoleAccesses_ApplicationFeatureId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 261, DateTimeKind.Local).AddTicks(4982),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 982, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 263, DateTimeKind.Local).AddTicks(2285),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 983, DateTimeKind.Local).AddTicks(9493));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 252, DateTimeKind.Local).AddTicks(3226),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 967, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 259, DateTimeKind.Local).AddTicks(8795),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 979, DateTimeKind.Local).AddTicks(8781));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 242, DateTimeKind.Local).AddTicks(3730),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 954, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 253, DateTimeKind.Local).AddTicks(8617),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 969, DateTimeKind.Local).AddTicks(733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 258, DateTimeKind.Local).AddTicks(20),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 978, DateTimeKind.Local).AddTicks(611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 255, DateTimeKind.Local).AddTicks(5440),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 972, DateTimeKind.Local).AddTicks(9159));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 250, DateTimeKind.Local).AddTicks(6189),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 964, DateTimeKind.Local).AddTicks(3863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 244, DateTimeKind.Local).AddTicks(1424),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 956, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 245, DateTimeKind.Local).AddTicks(7966),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 958, DateTimeKind.Local).AddTicks(6805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 249, DateTimeKind.Local).AddTicks(1650),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 962, DateTimeKind.Local).AddTicks(8572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 239, DateTimeKind.Local).AddTicks(991),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 947, DateTimeKind.Local).AddTicks(5675));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 36, 13, 236, DateTimeKind.Utc).AddTicks(8844),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 943, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 36, 13, 236, DateTimeKind.Utc).AddTicks(6062),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 942, DateTimeKind.Utc).AddTicks(9447));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ApplicationFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 223, DateTimeKind.Local).AddTicks(3326),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 928, DateTimeKind.Local).AddTicks(5622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 36, 13, 234, DateTimeKind.Local).AddTicks(811),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 939, DateTimeKind.Local).AddTicks(5635));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationFeatures",
                schema: "dbo",
                table: "ApplicationFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationFeatureRoleAccesses",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationFeatureRoleAccesses_ApplicationFeatures_ApplicationFeatureId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                column: "ApplicationFeatureId",
                principalSchema: "dbo",
                principalTable: "ApplicationFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationFeatureRoleAccesses_AspNetRoles_RoleId",
                schema: "dbo",
                table: "ApplicationFeatureRoleAccesses",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationFeatures_ApplicationFeatures_ParentId",
                schema: "dbo",
                table: "ApplicationFeatures",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "ApplicationFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
