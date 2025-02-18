using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplicationPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFeatureRoleAccesses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppFeatures",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 940, DateTimeKind.Local).AddTicks(1649),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 982, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 941, DateTimeKind.Local).AddTicks(9218),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 983, DateTimeKind.Local).AddTicks(9493));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 930, DateTimeKind.Local).AddTicks(9623),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 967, DateTimeKind.Local).AddTicks(760));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 938, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 979, DateTimeKind.Local).AddTicks(8781));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 919, DateTimeKind.Local).AddTicks(4787),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 954, DateTimeKind.Local).AddTicks(2751));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 933, DateTimeKind.Local).AddTicks(1149),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 969, DateTimeKind.Local).AddTicks(733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 937, DateTimeKind.Local).AddTicks(2127),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 978, DateTimeKind.Local).AddTicks(611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 935, DateTimeKind.Local).AddTicks(3914),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 972, DateTimeKind.Local).AddTicks(9159));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 928, DateTimeKind.Local).AddTicks(988),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 964, DateTimeKind.Local).AddTicks(3863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 921, DateTimeKind.Local).AddTicks(6161),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 956, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 923, DateTimeKind.Local).AddTicks(3875),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 958, DateTimeKind.Local).AddTicks(6805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 926, DateTimeKind.Local).AddTicks(1345),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 962, DateTimeKind.Local).AddTicks(8572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 916, DateTimeKind.Local).AddTicks(4653),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 947, DateTimeKind.Local).AddTicks(5675));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 862, DateTimeKind.Utc).AddTicks(907),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 943, DateTimeKind.Utc).AddTicks(4963));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 859, DateTimeKind.Utc).AddTicks(806),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 942, DateTimeKind.Utc).AddTicks(9447));

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 906, DateTimeKind.Local).AddTicks(8982)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 910, DateTimeKind.Local).AddTicks(320)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "dbo",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "dbo",
                table: "RolePermissions",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 982, DateTimeKind.Local).AddTicks(1619),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 940, DateTimeKind.Local).AddTicks(1649));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 983, DateTimeKind.Local).AddTicks(9493),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 941, DateTimeKind.Local).AddTicks(9218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 967, DateTimeKind.Local).AddTicks(760),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 930, DateTimeKind.Local).AddTicks(9623));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 979, DateTimeKind.Local).AddTicks(8781),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 938, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 954, DateTimeKind.Local).AddTicks(2751),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 919, DateTimeKind.Local).AddTicks(4787));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 969, DateTimeKind.Local).AddTicks(733),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 933, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 978, DateTimeKind.Local).AddTicks(611),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 937, DateTimeKind.Local).AddTicks(2127));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 972, DateTimeKind.Local).AddTicks(9159),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 935, DateTimeKind.Local).AddTicks(3914));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 964, DateTimeKind.Local).AddTicks(3863),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 928, DateTimeKind.Local).AddTicks(988));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 956, DateTimeKind.Local).AddTicks(6336),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 921, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 958, DateTimeKind.Local).AddTicks(6805),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 923, DateTimeKind.Local).AddTicks(3875));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 962, DateTimeKind.Local).AddTicks(8572),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 926, DateTimeKind.Local).AddTicks(1345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 947, DateTimeKind.Local).AddTicks(5675),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 23, 16, 25, 916, DateTimeKind.Local).AddTicks(4653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 943, DateTimeKind.Utc).AddTicks(4963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 862, DateTimeKind.Utc).AddTicks(907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 25, 13, 42, 41, 942, DateTimeKind.Utc).AddTicks(9447),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 27, 15, 16, 25, 859, DateTimeKind.Utc).AddTicks(806));

            migrationBuilder.CreateTable(
                name: "AppFeatures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 928, DateTimeKind.Local).AddTicks(5622)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeatures_AppFeatures_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "AppFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppFeatureRoleAccesses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 25, 21, 42, 41, 939, DateTimeKind.Local).AddTicks(5635)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    ApplicationFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CRUDActionAccess = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeatureRoleAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeatureRoleAccesses_AppFeatures_ApplicationFeatureId",
                        column: x => x.ApplicationFeatureId,
                        principalSchema: "dbo",
                        principalTable: "AppFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFeatureRoleAccesses_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRoleAccesses_ApplicationFeatureId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                column: "ApplicationFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRoleAccesses_RoleId",
                schema: "dbo",
                table: "AppFeatureRoleAccesses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatures_ParentId",
                schema: "dbo",
                table: "AppFeatures",
                column: "ParentId");
        }
    }
}
