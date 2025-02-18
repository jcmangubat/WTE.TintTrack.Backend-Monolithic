using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class prefixEntityNameToIdSetToFalse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuoteId",
                schema: "dbo",
                table: "Quotes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProposalId",
                schema: "dbo",
                table: "Proposals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                schema: "dbo",
                table: "Properties",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "dbo",
                table: "Projects",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                schema: "dbo",
                table: "Invoices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerOwnershipId",
                schema: "dbo",
                table: "CustomerOwnerships",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CustomerContactId",
                schema: "dbo",
                table: "CustomerContacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                schema: "dbo",
                table: "Contacts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AuditLogId",
                schema: "dbo",
                table: "AuditLogs",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 735, DateTimeKind.Local).AddTicks(9151),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 877, DateTimeKind.Local).AddTicks(754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 737, DateTimeKind.Local).AddTicks(3691),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 878, DateTimeKind.Local).AddTicks(5210));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 729, DateTimeKind.Local).AddTicks(8222),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 871, DateTimeKind.Local).AddTicks(8424));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 739, DateTimeKind.Local).AddTicks(1183),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 881, DateTimeKind.Local).AddTicks(44));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 740, DateTimeKind.Local).AddTicks(8520),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 883, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 728, DateTimeKind.Local).AddTicks(1079),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 870, DateTimeKind.Local).AddTicks(4635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 726, DateTimeKind.Local).AddTicks(5033),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 868, DateTimeKind.Local).AddTicks(9327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 722, DateTimeKind.Local).AddTicks(1233),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 864, DateTimeKind.Local).AddTicks(2008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 734, DateTimeKind.Local).AddTicks(2962),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 875, DateTimeKind.Local).AddTicks(5467));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 732, DateTimeKind.Local).AddTicks(9774),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 874, DateTimeKind.Local).AddTicks(3538));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 717, DateTimeKind.Local).AddTicks(7200),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 859, DateTimeKind.Local).AddTicks(8294));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Quotes",
                newName: "QuoteId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Proposals",
                newName: "ProposalId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Properties",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Projects",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Invoices",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "CustomerOwnerships",
                newName: "CustomerOwnershipId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "CustomerContacts",
                newName: "CustomerContactId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Contacts",
                newName: "ContactId");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "AuditLogs",
                newName: "AuditLogId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 877, DateTimeKind.Local).AddTicks(754),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 735, DateTimeKind.Local).AddTicks(9151));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 878, DateTimeKind.Local).AddTicks(5210),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 737, DateTimeKind.Local).AddTicks(3691));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 871, DateTimeKind.Local).AddTicks(8424),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 729, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 881, DateTimeKind.Local).AddTicks(44),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 739, DateTimeKind.Local).AddTicks(1183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 883, DateTimeKind.Local).AddTicks(2979),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 740, DateTimeKind.Local).AddTicks(8520));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 870, DateTimeKind.Local).AddTicks(4635),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 728, DateTimeKind.Local).AddTicks(1079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 868, DateTimeKind.Local).AddTicks(9327),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 726, DateTimeKind.Local).AddTicks(5033));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 864, DateTimeKind.Local).AddTicks(2008),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 722, DateTimeKind.Local).AddTicks(1233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 875, DateTimeKind.Local).AddTicks(5467),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 734, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 874, DateTimeKind.Local).AddTicks(3538),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 732, DateTimeKind.Local).AddTicks(9774));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 1, 22, 26, 21, 859, DateTimeKind.Local).AddTicks(8294),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 3, 16, 48, 43, 717, DateTimeKind.Local).AddTicks(7200));
        }
    }
}
