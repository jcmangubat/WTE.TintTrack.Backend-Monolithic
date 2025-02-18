using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RevisedCustProps : Migration
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
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 95, DateTimeKind.Local).AddTicks(3445),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 933, DateTimeKind.Local).AddTicks(1344));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 96, DateTimeKind.Local).AddTicks(8906),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 934, DateTimeKind.Local).AddTicks(7054));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 98, DateTimeKind.Local).AddTicks(7325),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 936, DateTimeKind.Local).AddTicks(6482));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 100, DateTimeKind.Local).AddTicks(8525),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 938, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 63, DateTimeKind.Local).AddTicks(5293),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 928, DateTimeKind.Local).AddTicks(7262));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 61, DateTimeKind.Local).AddTicks(8094),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 927, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 57, DateTimeKind.Local).AddTicks(3),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 921, DateTimeKind.Local).AddTicks(7836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 93, DateTimeKind.Local).AddTicks(1233),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 931, DateTimeKind.Local).AddTicks(4189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 91, DateTimeKind.Local).AddTicks(9404),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 930, DateTimeKind.Local).AddTicks(2229));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 51, DateTimeKind.Local).AddTicks(5315),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 917, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "dbo",
                columns: table => new
                {
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 64, DateTimeKind.Local).AddTicks(8476)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindowSizeInFeet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrameMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSecurityFilm = table.Column<bool>(type: "bit", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<double>(type: "float", nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true),
                    HasDefrostLines = table.Column<bool>(type: "bit", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSecurityGlass = table.Column<bool>(type: "bit", nullable: true),
                    HasUVProtection = table.Column<bool>(type: "bit", nullable: true),
                    HasSoundproofing = table.Column<bool>(type: "bit", nullable: true),
                    CustomGlassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomizationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoatingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasInsulatedGlass = table.Column<bool>(type: "bit", nullable: true),
                    FilmType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilmThickness = table.Column<double>(type: "float", nullable: true),
                    IsTinted = table.Column<bool>(type: "bit", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutdoorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsWeatherResistant = table.Column<bool>(type: "bit", nullable: true),
                    HasSafetyFeatures = table.Column<bool>(type: "bit", nullable: true),
                    HomeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    HasEnergyEfficientWindows = table.Column<bool>(type: "bit", nullable: true),
                    HasPrivacyTint = table.Column<bool>(type: "bit", nullable: true),
                    SignageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandingDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBacklit = table.Column<bool>(type: "bit", nullable: true),
                    SpecialtyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFireResistant = table.Column<bool>(type: "bit", nullable: true),
                    IsSmartGlass = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CustomerId",
                schema: "dbo",
                table: "Properties",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Properties",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 933, DateTimeKind.Local).AddTicks(1344),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 95, DateTimeKind.Local).AddTicks(3445));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 934, DateTimeKind.Local).AddTicks(7054),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 96, DateTimeKind.Local).AddTicks(8906));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 936, DateTimeKind.Local).AddTicks(6482),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 98, DateTimeKind.Local).AddTicks(7325));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 938, DateTimeKind.Local).AddTicks(4630),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 100, DateTimeKind.Local).AddTicks(8525));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 928, DateTimeKind.Local).AddTicks(7262),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 63, DateTimeKind.Local).AddTicks(5293));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 927, DateTimeKind.Local).AddTicks(3110),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 61, DateTimeKind.Local).AddTicks(8094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerInquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 921, DateTimeKind.Local).AddTicks(7836),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 57, DateTimeKind.Local).AddTicks(3));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 931, DateTimeKind.Local).AddTicks(4189),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 93, DateTimeKind.Local).AddTicks(1233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 930, DateTimeKind.Local).AddTicks(2229),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 91, DateTimeKind.Local).AddTicks(9404));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 917, DateTimeKind.Local).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 29, 56, 51, DateTimeKind.Local).AddTicks(5315));
        }
    }
}
