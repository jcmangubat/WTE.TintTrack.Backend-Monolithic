using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MovedCustInqryToTenantDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInquiries",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 282, DateTimeKind.Local).AddTicks(1181),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 470, DateTimeKind.Local).AddTicks(3684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 283, DateTimeKind.Local).AddTicks(5539),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 472, DateTimeKind.Local).AddTicks(1883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 274, DateTimeKind.Local).AddTicks(9371),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 461, DateTimeKind.Local).AddTicks(1631));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 280, DateTimeKind.Local).AddTicks(7207),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 468, DateTimeKind.Local).AddTicks(4429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 265, DateTimeKind.Local).AddTicks(3666),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 451, DateTimeKind.Local).AddTicks(5441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 276, DateTimeKind.Local).AddTicks(5297),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 462, DateTimeKind.Local).AddTicks(7070));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 279, DateTimeKind.Local).AddTicks(6016),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 466, DateTimeKind.Local).AddTicks(9324));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 278, DateTimeKind.Local).AddTicks(1068),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 464, DateTimeKind.Local).AddTicks(4156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 273, DateTimeKind.Local).AddTicks(3380),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 459, DateTimeKind.Local).AddTicks(4954));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 267, DateTimeKind.Local).AddTicks(3413),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 453, DateTimeKind.Local).AddTicks(6088));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 269, DateTimeKind.Local).AddTicks(3072),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 455, DateTimeKind.Local).AddTicks(6337));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 271, DateTimeKind.Local).AddTicks(6845),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 458, DateTimeKind.Local).AddTicks(805));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 262, DateTimeKind.Local).AddTicks(5929),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 442, DateTimeKind.Local).AddTicks(4993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 259, DateTimeKind.Local).AddTicks(1939),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 439, DateTimeKind.Local).AddTicks(6724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 240, DateTimeKind.Utc).AddTicks(5238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 407, DateTimeKind.Utc).AddTicks(4235));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 237, DateTimeKind.Utc).AddTicks(9626),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 404, DateTimeKind.Utc).AddTicks(3130));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 282, DateTimeKind.Local).AddTicks(1181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantRoles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 472, DateTimeKind.Local).AddTicks(1883),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 283, DateTimeKind.Local).AddTicks(5539));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserTenantInvitations",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 461, DateTimeKind.Local).AddTicks(1631),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 274, DateTimeKind.Local).AddTicks(9371));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "UserBillingProfiles",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 468, DateTimeKind.Local).AddTicks(4429),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 280, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tokens",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 451, DateTimeKind.Local).AddTicks(5441),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 265, DateTimeKind.Local).AddTicks(3666));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 462, DateTimeKind.Local).AddTicks(7070),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 276, DateTimeKind.Local).AddTicks(5297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 466, DateTimeKind.Local).AddTicks(9324),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 279, DateTimeKind.Local).AddTicks(6016));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 464, DateTimeKind.Local).AddTicks(4156),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 278, DateTimeKind.Local).AddTicks(1068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Tenants",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 459, DateTimeKind.Local).AddTicks(4954),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 273, DateTimeKind.Local).AddTicks(3380));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlans",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 453, DateTimeKind.Local).AddTicks(6088),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 267, DateTimeKind.Local).AddTicks(3413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanFeatures",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 455, DateTimeKind.Local).AddTicks(6337),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 269, DateTimeKind.Local).AddTicks(3072));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 458, DateTimeKind.Local).AddTicks(805),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 271, DateTimeKind.Local).AddTicks(6845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "RolePermissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 442, DateTimeKind.Local).AddTicks(4993),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 262, DateTimeKind.Local).AddTicks(5929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Permissions",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 439, DateTimeKind.Local).AddTicks(6724),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 17, 25, 57, 259, DateTimeKind.Local).AddTicks(1939));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 407, DateTimeKind.Utc).AddTicks(4235),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 240, DateTimeKind.Utc).AddTicks(5238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 28, 12, 22, 18, 404, DateTimeKind.Utc).AddTicks(3130),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 9, 25, 57, 237, DateTimeKind.Utc).AddTicks(9626));

            migrationBuilder.CreateTable(
                name: "CustomerInquiries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 28, 20, 22, 18, 447, DateTimeKind.Local).AddTicks(5965)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    SalesRepresentativeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ConsultationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultationDetails = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    ContactMethod = table.Column<int>(type: "int", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    ProposalCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    SpecialRequests = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInquiries_AspNetUsers_SalesRepresentativeId",
                        column: x => x.SalesRepresentativeId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_SalesRepresentativeId",
                schema: "dbo",
                table: "CustomerInquiries",
                column: "SalesRepresentativeId");
        }
    }
}
