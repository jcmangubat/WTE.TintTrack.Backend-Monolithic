using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCustCodeLengthTo15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 970, DateTimeKind.Local).AddTicks(5319),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 456, DateTimeKind.Local).AddTicks(3064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 973, DateTimeKind.Local).AddTicks(5393),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 457, DateTimeKind.Local).AddTicks(9193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 961, DateTimeKind.Local).AddTicks(7060),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 450, DateTimeKind.Local).AddTicks(700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 976, DateTimeKind.Local).AddTicks(5509),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 460, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 979, DateTimeKind.Local).AddTicks(4752),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 462, DateTimeKind.Local).AddTicks(726));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 959, DateTimeKind.Local).AddTicks(5799),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 448, DateTimeKind.Local).AddTicks(1987));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 956, DateTimeKind.Local).AddTicks(4598),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 446, DateTimeKind.Local).AddTicks(6334));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 948, DateTimeKind.Local).AddTicks(9556),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 441, DateTimeKind.Local).AddTicks(9140));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 967, DateTimeKind.Local).AddTicks(2955),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 454, DateTimeKind.Local).AddTicks(6373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 965, DateTimeKind.Local).AddTicks(2783),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 453, DateTimeKind.Local).AddTicks(3263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 942, DateTimeKind.Local).AddTicks(8271),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 437, DateTimeKind.Local).AddTicks(2824));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 456, DateTimeKind.Local).AddTicks(3064),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 970, DateTimeKind.Local).AddTicks(5319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 457, DateTimeKind.Local).AddTicks(9193),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 973, DateTimeKind.Local).AddTicks(5393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 450, DateTimeKind.Local).AddTicks(700),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 961, DateTimeKind.Local).AddTicks(7060));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 460, DateTimeKind.Local).AddTicks(1121),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 976, DateTimeKind.Local).AddTicks(5509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 462, DateTimeKind.Local).AddTicks(726),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 979, DateTimeKind.Local).AddTicks(4752));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 448, DateTimeKind.Local).AddTicks(1987),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 959, DateTimeKind.Local).AddTicks(5799));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 446, DateTimeKind.Local).AddTicks(6334),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 956, DateTimeKind.Local).AddTicks(4598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 441, DateTimeKind.Local).AddTicks(9140),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 948, DateTimeKind.Local).AddTicks(9556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 454, DateTimeKind.Local).AddTicks(6373),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 967, DateTimeKind.Local).AddTicks(2955));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 453, DateTimeKind.Local).AddTicks(3263),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 965, DateTimeKind.Local).AddTicks(2783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 21, 26, 46, 437, DateTimeKind.Local).AddTicks(2824),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 21, 30, 25, 942, DateTimeKind.Local).AddTicks(8271));
        }
    }
}
