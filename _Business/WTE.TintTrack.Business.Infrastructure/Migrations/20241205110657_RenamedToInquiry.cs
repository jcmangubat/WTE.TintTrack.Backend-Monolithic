using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedToInquiry : Migration
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
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 180, DateTimeKind.Local).AddTicks(1188),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 386, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 182, DateTimeKind.Local).AddTicks(7858),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 388, DateTimeKind.Local).AddTicks(8481));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 170, DateTimeKind.Local).AddTicks(2256),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 379, DateTimeKind.Local).AddTicks(9718));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 185, DateTimeKind.Local).AddTicks(1713),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 391, DateTimeKind.Local).AddTicks(1177));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 187, DateTimeKind.Local).AddTicks(5775),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 392, DateTimeKind.Local).AddTicks(9392));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 167, DateTimeKind.Local).AddTicks(8423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 377, DateTimeKind.Local).AddTicks(4279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 164, DateTimeKind.Local).AddTicks(6815),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 374, DateTimeKind.Local).AddTicks(5008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 177, DateTimeKind.Local).AddTicks(1061),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 384, DateTimeKind.Local).AddTicks(9159));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 174, DateTimeKind.Local).AddTicks(5470),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 383, DateTimeKind.Local).AddTicks(6525));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 151, DateTimeKind.Local).AddTicks(286),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 364, DateTimeKind.Local).AddTicks(7536));

            migrationBuilder.CreateTable(
                name: "Inquiries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 157, DateTimeKind.Local).AddTicks(405)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    ContactMethod = table.Column<int>(type: "int", nullable: false),
                    ConsultationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultationDetails = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true),
                    SpecialRequests = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: true),
                    ProposalCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    SalesRepUserCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerId",
                schema: "dbo",
                table: "Inquiries",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inquiries",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 386, DateTimeKind.Local).AddTicks(5900),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 180, DateTimeKind.Local).AddTicks(1188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 388, DateTimeKind.Local).AddTicks(8481),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 182, DateTimeKind.Local).AddTicks(7858));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 379, DateTimeKind.Local).AddTicks(9718),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 170, DateTimeKind.Local).AddTicks(2256));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 391, DateTimeKind.Local).AddTicks(1177),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 185, DateTimeKind.Local).AddTicks(1713));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 392, DateTimeKind.Local).AddTicks(9392),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 187, DateTimeKind.Local).AddTicks(5775));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 377, DateTimeKind.Local).AddTicks(4279),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 167, DateTimeKind.Local).AddTicks(8423));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 374, DateTimeKind.Local).AddTicks(5008),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 164, DateTimeKind.Local).AddTicks(6815));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 384, DateTimeKind.Local).AddTicks(9159),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 177, DateTimeKind.Local).AddTicks(1061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 383, DateTimeKind.Local).AddTicks(6525),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 174, DateTimeKind.Local).AddTicks(5470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 364, DateTimeKind.Local).AddTicks(7536),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 151, DateTimeKind.Local).AddTicks(286));

            migrationBuilder.CreateTable(
                name: "CustomerInquiries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 12, 5, 15, 47, 2, 369, DateTimeKind.Local).AddTicks(350)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ConsultationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsultationDetails = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    ContactMethod = table.Column<int>(type: "int", nullable: false),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    ProposalCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    SalesRepUserCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    SpecialRequests = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInquiries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_CustomerId",
                schema: "dbo",
                table: "CustomerInquiries",
                column: "CustomerId");
        }
    }
}
