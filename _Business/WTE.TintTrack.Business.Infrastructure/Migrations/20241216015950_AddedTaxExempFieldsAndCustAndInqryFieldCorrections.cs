using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTaxExempFieldsAndCustAndInqryFieldCorrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactMethod",
                schema: "dbo",
                table: "Inquiries",
                newName: "LeadSource");

            migrationBuilder.RenameColumn(
                name: "ConsultationDetails",
                schema: "dbo",
                table: "Inquiries",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "CustomerLevel",
                schema: "dbo",
                table: "Customers",
                newName: "CustomerStatus");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 19, DateTimeKind.Local).AddTicks(2960),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 180, DateTimeKind.Local).AddTicks(1188));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Quotes",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 21, DateTimeKind.Local).AddTicks(2675),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 182, DateTimeKind.Local).AddTicks(7858));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Proposals",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 11, DateTimeKind.Local).AddTicks(8901),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 170, DateTimeKind.Local).AddTicks(2256));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 23, DateTimeKind.Local).AddTicks(7219),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 185, DateTimeKind.Local).AddTicks(1713));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Projects",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AddColumn<int>(
                name: "TaxExemptionReason",
                schema: "dbo",
                table: "Projects",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 25, DateTimeKind.Local).AddTicks(7483),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 187, DateTimeKind.Local).AddTicks(5775));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Invoices",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 3, DateTimeKind.Local).AddTicks(300),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 157, DateTimeKind.Local).AddTicks(405));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Inquiries",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                schema: "dbo",
                table: "Inquiries",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 10, DateTimeKind.Local).AddTicks(254),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 167, DateTimeKind.Local).AddTicks(8423));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Customers",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AddColumn<int>(
                name: "TaxExemptionReason",
                schema: "dbo",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 8, DateTimeKind.Local).AddTicks(1656),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 164, DateTimeKind.Local).AddTicks(6815));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 16, DateTimeKind.Local).AddTicks(8662),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 177, DateTimeKind.Local).AddTicks(1061));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "CustomerContacts",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 15, DateTimeKind.Local).AddTicks(2025),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 174, DateTimeKind.Local).AddTicks(5470));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 16, 9, 59, 48, 998, DateTimeKind.Local).AddTicks(796),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 151, DateTimeKind.Local).AddTicks(286));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "AuditLogs",
                type: "nvarchar(240)",
                maxLength: 240,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(132)",
                oldMaxLength: 132)
                .Annotation("Relational:ColumnOrder", 512)
                .OldAnnotation("Relational:ColumnOrder", 511);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxExemptionReason",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Subject",
                schema: "dbo",
                table: "Inquiries");

            migrationBuilder.DropColumn(
                name: "TaxExemptionReason",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "LeadSource",
                schema: "dbo",
                table: "Inquiries",
                newName: "ContactMethod");

            migrationBuilder.RenameColumn(
                name: "Details",
                schema: "dbo",
                table: "Inquiries",
                newName: "ConsultationDetails");

            migrationBuilder.RenameColumn(
                name: "CustomerStatus",
                schema: "dbo",
                table: "Customers",
                newName: "CustomerLevel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 180, DateTimeKind.Local).AddTicks(1188),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 19, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Quotes",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 182, DateTimeKind.Local).AddTicks(7858),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 21, DateTimeKind.Local).AddTicks(2675));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Proposals",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Properties",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 170, DateTimeKind.Local).AddTicks(2256),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 11, DateTimeKind.Local).AddTicks(8901));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 185, DateTimeKind.Local).AddTicks(1713),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 23, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Projects",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 187, DateTimeKind.Local).AddTicks(5775),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 25, DateTimeKind.Local).AddTicks(7483));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Invoices",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 157, DateTimeKind.Local).AddTicks(405),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 3, DateTimeKind.Local).AddTicks(300));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Inquiries",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 167, DateTimeKind.Local).AddTicks(8423),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 10, DateTimeKind.Local).AddTicks(254));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Customers",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 164, DateTimeKind.Local).AddTicks(6815),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 8, DateTimeKind.Local).AddTicks(1656));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 177, DateTimeKind.Local).AddTicks(1061),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 16, DateTimeKind.Local).AddTicks(8662));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "CustomerContacts",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 174, DateTimeKind.Local).AddTicks(5470),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 49, 15, DateTimeKind.Local).AddTicks(2025));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "Contacts",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 19, 6, 56, 151, DateTimeKind.Local).AddTicks(286),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 12, 16, 9, 59, 48, 998, DateTimeKind.Local).AddTicks(796));

            migrationBuilder.AlterColumn<string>(
                name: "ReasonArchived",
                schema: "dbo",
                table: "AuditLogs",
                type: "nvarchar(132)",
                maxLength: 132,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(240)",
                oldMaxLength: 240,
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 511)
                .OldAnnotation("Relational:ColumnOrder", 512);
        }
    }
}
