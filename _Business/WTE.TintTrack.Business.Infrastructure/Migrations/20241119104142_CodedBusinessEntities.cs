using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CodedBusinessEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCode",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CustomerCode",
                schema: "dbo",
                table: "Customers",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "PropertyCode",
                schema: "dbo",
                table: "CustomerProperties",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "ContactCode",
                schema: "dbo",
                table: "Contacts",
                newName: "Code");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 37, DateTimeKind.Local).AddTicks(657),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 588, DateTimeKind.Local).AddTicks(7659));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "dbo",
                table: "Quotes",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 39, DateTimeKind.Local).AddTicks(6082),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 589, DateTimeKind.Local).AddTicks(1053));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "dbo",
                table: "Proposals",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 42, DateTimeKind.Local).AddTicks(373),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 589, DateTimeKind.Local).AddTicks(4504));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "dbo",
                table: "Projects",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 44, DateTimeKind.Local).AddTicks(452),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 590, DateTimeKind.Local).AddTicks(2238));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "dbo",
                table: "Invoices",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 976, DateTimeKind.Local).AddTicks(3934),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(9278));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerPropertyDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 983, DateTimeKind.Local).AddTicks(4038),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 570, DateTimeKind.Local).AddTicks(6572));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerProperties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 978, DateTimeKind.Local).AddTicks(6823),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 569, DateTimeKind.Local).AddTicks(2564));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 972, DateTimeKind.Local).AddTicks(5311),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 35, DateTimeKind.Local).AddTicks(4181),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 588, DateTimeKind.Local).AddTicks(2640));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 34, DateTimeKind.Local).AddTicks(985),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 587, DateTimeKind.Local).AddTicks(5927));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 969, DateTimeKind.Local).AddTicks(3701),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(3271));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "dbo",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "dbo",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "dbo",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Customers",
                newName: "CustomerCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "CustomerProperties",
                newName: "PropertyCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "dbo",
                table: "Contacts",
                newName: "ContactCode");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 588, DateTimeKind.Local).AddTicks(7659),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 37, DateTimeKind.Local).AddTicks(657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 589, DateTimeKind.Local).AddTicks(1053),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 39, DateTimeKind.Local).AddTicks(6082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 589, DateTimeKind.Local).AddTicks(4504),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 42, DateTimeKind.Local).AddTicks(373));

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                schema: "dbo",
                table: "Projects",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 590, DateTimeKind.Local).AddTicks(2238),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 44, DateTimeKind.Local).AddTicks(452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(9278),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 976, DateTimeKind.Local).AddTicks(3934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerPropertyDetails",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 570, DateTimeKind.Local).AddTicks(6572),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 983, DateTimeKind.Local).AddTicks(4038));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerProperties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 569, DateTimeKind.Local).AddTicks(2564),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 978, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(5385),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 972, DateTimeKind.Local).AddTicks(5311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 588, DateTimeKind.Local).AddTicks(2640),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 35, DateTimeKind.Local).AddTicks(4181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 587, DateTimeKind.Local).AddTicks(5927),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 34, DateTimeKind.Local).AddTicks(985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 14, 15, 11, 34, 568, DateTimeKind.Local).AddTicks(3271),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 969, DateTimeKind.Local).AddTicks(3701));
        }
    }
}
