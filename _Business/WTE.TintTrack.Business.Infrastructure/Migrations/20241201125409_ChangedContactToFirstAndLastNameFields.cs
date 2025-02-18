using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedContactToFirstAndLastNameFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "dbo",
                table: "Contacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 557, DateTimeKind.Local).AddTicks(1443),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 258, DateTimeKind.Local).AddTicks(8626));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 558, DateTimeKind.Local).AddTicks(8312),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 260, DateTimeKind.Local).AddTicks(3260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 550, DateTimeKind.Local).AddTicks(3563),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 253, DateTimeKind.Local).AddTicks(2031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 561, DateTimeKind.Local).AddTicks(553),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 262, DateTimeKind.Local).AddTicks(2737));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 562, DateTimeKind.Local).AddTicks(8881),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 264, DateTimeKind.Local).AddTicks(514));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 548, DateTimeKind.Local).AddTicks(9622),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 251, DateTimeKind.Local).AddTicks(2576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 547, DateTimeKind.Local).AddTicks(3981),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 249, DateTimeKind.Local).AddTicks(1237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 542, DateTimeKind.Local).AddTicks(5297),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 244, DateTimeKind.Local).AddTicks(1876));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 554, DateTimeKind.Local).AddTicks(6257),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 257, DateTimeKind.Local).AddTicks(2185));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 553, DateTimeKind.Local).AddTicks(720),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 255, DateTimeKind.Local).AddTicks(9090));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 537, DateTimeKind.Local).AddTicks(2941),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 239, DateTimeKind.Local).AddTicks(9227));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "dbo",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "dbo",
                table: "Contacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 258, DateTimeKind.Local).AddTicks(8626),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 557, DateTimeKind.Local).AddTicks(1443));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 260, DateTimeKind.Local).AddTicks(3260),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 558, DateTimeKind.Local).AddTicks(8312));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 253, DateTimeKind.Local).AddTicks(2031),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 550, DateTimeKind.Local).AddTicks(3563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 262, DateTimeKind.Local).AddTicks(2737),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 561, DateTimeKind.Local).AddTicks(553));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 264, DateTimeKind.Local).AddTicks(514),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 562, DateTimeKind.Local).AddTicks(8881));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 251, DateTimeKind.Local).AddTicks(2576),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 548, DateTimeKind.Local).AddTicks(9622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 249, DateTimeKind.Local).AddTicks(1237),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 547, DateTimeKind.Local).AddTicks(3981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 244, DateTimeKind.Local).AddTicks(1876),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 542, DateTimeKind.Local).AddTicks(5297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 257, DateTimeKind.Local).AddTicks(2185),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 554, DateTimeKind.Local).AddTicks(6257));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 255, DateTimeKind.Local).AddTicks(9090),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 553, DateTimeKind.Local).AddTicks(720));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 20, 42, 24, 239, DateTimeKind.Local).AddTicks(9227),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 20, 54, 7, 537, DateTimeKind.Local).AddTicks(2941));
        }
    }
}
