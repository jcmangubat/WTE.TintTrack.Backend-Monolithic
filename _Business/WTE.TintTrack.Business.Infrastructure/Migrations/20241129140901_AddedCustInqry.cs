using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustInqry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProperties",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerPropertyDetails",
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
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 37, DateTimeKind.Local).AddTicks(657));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 934, DateTimeKind.Local).AddTicks(7054),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 39, DateTimeKind.Local).AddTicks(6082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 936, DateTimeKind.Local).AddTicks(6482),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 42, DateTimeKind.Local).AddTicks(373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 938, DateTimeKind.Local).AddTicks(4630),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 44, DateTimeKind.Local).AddTicks(452));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 928, DateTimeKind.Local).AddTicks(7262),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 976, DateTimeKind.Local).AddTicks(3934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 927, DateTimeKind.Local).AddTicks(3110),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 972, DateTimeKind.Local).AddTicks(5311));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 931, DateTimeKind.Local).AddTicks(4189),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 35, DateTimeKind.Local).AddTicks(4181));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 930, DateTimeKind.Local).AddTicks(2229),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 34, DateTimeKind.Local).AddTicks(985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 917, DateTimeKind.Local).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 969, DateTimeKind.Local).AddTicks(3701));

            migrationBuilder.CreateTable(
                name: "CustomerInquiries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 921, DateTimeKind.Local).AddTicks(7836)),
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
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInquiries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInquiries_CustomerId",
                schema: "dbo",
                table: "CustomerInquiries",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 37, DateTimeKind.Local).AddTicks(657),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 933, DateTimeKind.Local).AddTicks(1344));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 39, DateTimeKind.Local).AddTicks(6082),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 934, DateTimeKind.Local).AddTicks(7054));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 42, DateTimeKind.Local).AddTicks(373),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 936, DateTimeKind.Local).AddTicks(6482));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Invoices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 44, DateTimeKind.Local).AddTicks(452),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 938, DateTimeKind.Local).AddTicks(4630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 976, DateTimeKind.Local).AddTicks(3934),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 928, DateTimeKind.Local).AddTicks(7262));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerOwnerships",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 972, DateTimeKind.Local).AddTicks(5311),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 927, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 35, DateTimeKind.Local).AddTicks(4181),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 931, DateTimeKind.Local).AddTicks(4189));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 41, 34, DateTimeKind.Local).AddTicks(985),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 930, DateTimeKind.Local).AddTicks(2229));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 969, DateTimeKind.Local).AddTicks(3701),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2024, 11, 29, 22, 8, 59, 917, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.CreateTable(
                name: "CustomerPropertyDetails",
                schema: "dbo",
                columns: table => new
                {
                    CustomerPropertyDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 983, DateTimeKind.Local).AddTicks(4038)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    CustomerPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    BuildingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrameMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSecurityFilm = table.Column<bool>(type: "bit", nullable: true),
                    WindowSizeInFeet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasDefrostLines = table.Column<bool>(type: "bit", nullable: true),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mileage = table.Column<double>(type: "float", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true),
                    Trim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSecurityGlass = table.Column<bool>(type: "bit", nullable: true),
                    HasSoundproofing = table.Column<bool>(type: "bit", nullable: true),
                    HasUVProtection = table.Column<bool>(type: "bit", nullable: true),
                    CustomGlassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomizationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoatingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GlassType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasInsulatedGlass = table.Column<bool>(type: "bit", nullable: true),
                    FilmThickness = table.Column<double>(type: "float", nullable: true),
                    FilmType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTinted = table.Column<bool>(type: "bit", nullable: true),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasSafetyFeatures = table.Column<bool>(type: "bit", nullable: true),
                    IsWeatherResistant = table.Column<bool>(type: "bit", nullable: true),
                    OutdoorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasEnergyEfficientWindows = table.Column<bool>(type: "bit", nullable: true),
                    HasPrivacyTint = table.Column<bool>(type: "bit", nullable: true),
                    HomeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    BrandingDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBacklit = table.Column<bool>(type: "bit", nullable: true),
                    SignageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFireResistant = table.Column<bool>(type: "bit", nullable: true),
                    IsSmartGlass = table.Column<bool>(type: "bit", nullable: true),
                    SpecialtyType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPropertyDetails", x => x.CustomerPropertyDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProperties",
                schema: "dbo",
                columns: table => new
                {
                    CustomerPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2024, 11, 19, 18, 41, 40, 978, DateTimeKind.Local).AddTicks(6823)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(132)", maxLength: 132, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerPropertyDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProperties", x => x.CustomerPropertyId);
                    table.ForeignKey(
                        name: "FK_CustomerProperties_CustomerPropertyDetails_CustomerPropertyDetailsId",
                        column: x => x.CustomerPropertyDetailsId,
                        principalSchema: "dbo",
                        principalTable: "CustomerPropertyDetails",
                        principalColumn: "CustomerPropertyDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProperties_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProperties_CustomerId",
                schema: "dbo",
                table: "CustomerProperties",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProperties_CustomerPropertyDetailsId",
                schema: "dbo",
                table: "CustomerProperties",
                column: "CustomerPropertyDetailsId",
                unique: true);
        }
    }
}
