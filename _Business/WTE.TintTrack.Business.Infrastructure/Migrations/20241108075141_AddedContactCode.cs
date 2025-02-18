using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedContactCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 639, DateTimeKind.Local).AddTicks(9745),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 881, DateTimeKind.Local).AddTicks(574));

            migrationBuilder.AddColumn<string>(
                name: "ContactCode",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ContactOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 640, DateTimeKind.Local).AddTicks(3417),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 881, DateTimeKind.Local).AddTicks(4111));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 639, DateTimeKind.Local).AddTicks(7602),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 880, DateTimeKind.Local).AddTicks(8523));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactCode",
                schema: "dbo",
                table: "Contacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 881, DateTimeKind.Local).AddTicks(574),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 639, DateTimeKind.Local).AddTicks(9745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ContactOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 881, DateTimeKind.Local).AddTicks(4111),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 640, DateTimeKind.Local).AddTicks(3417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 8, 12, 50, 6, 880, DateTimeKind.Local).AddTicks(8523),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 8, 15, 51, 40, 639, DateTimeKind.Local).AddTicks(7602));
        }
    }
}
