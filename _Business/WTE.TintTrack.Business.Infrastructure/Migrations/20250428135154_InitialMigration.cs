using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 33, DateTimeKind.Local).AddTicks(9372)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ActionData = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 42, DateTimeKind.Local).AddTicks(4609)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AltPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    IsImported = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 37, DateTimeKind.Local).AddTicks(8929)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IndustryType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    GeneralEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MainPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false),
                    IsImported = table.Column<bool>(type: "bit", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TaxExemptionReason = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TintMaterials",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 57, DateTimeKind.Local).AddTicks(2734)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    RollLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RollWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TintServices",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 51, DateTimeKind.Local).AddTicks(4925)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ServiceType = table.Column<int>(type: "int", nullable: false),
                    EstimatedDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    AdditionalFeatures = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 50, DateTimeKind.Local).AddTicks(1880)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateOrRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryISOCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    AddressType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Contacts_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 44, DateTimeKind.Local).AddTicks(2089)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationshipType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "dbo",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAssets",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 45, DateTimeKind.Local).AddTicks(7472)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
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
                    table.PrimaryKey("PK_PropertyAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAssets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    QuantityInStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReorderLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasure = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryItem_TintMaterials_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TintMaterialPriceHistories",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 59, DateTimeKind.Local).AddTicks(7720)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    OldPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintMaterialPriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintMaterialPriceHistories_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintMaterialPriceSchedules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 61, DateTimeKind.Local).AddTicks(9389)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkupPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    CustomFormula = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: true),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintMaterialPriceSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintMaterialPriceSchedules_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintServicePriceSchedule",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarkupPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    CustomFormula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TintServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintServicePriceSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintServicePriceSchedule_TintServices_TintServiceId",
                        column: x => x.TintServiceId,
                        principalSchema: "dbo",
                        principalTable: "TintServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 54, DateTimeKind.Local).AddTicks(5778)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    LeadSource = table.Column<int>(type: "int", nullable: false),
                    ConsultationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TintType = table.Column<int>(type: "int", nullable: true),
                    SpecialRequests = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    FollowUpNeeded = table.Column<bool>(type: "bit", nullable: true),
                    SalesRepUserCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    TintServiceCodes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CustomerContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_CustomerContacts_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalSchema: "dbo",
                        principalTable: "CustomerContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintMaterialPriceOverrides",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 60, DateTimeKind.Local).AddTicks(4924)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    CustomPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintMaterialPriceScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintMaterialPriceOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintMaterialPriceOverrides_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TintMaterialPriceOverrides_TintMaterialPriceSchedules_TintMaterialPriceScheduleId",
                        column: x => x.TintMaterialPriceScheduleId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterialPriceSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintMaterialPriceTiers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 69, DateTimeKind.Local).AddTicks(1424)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    MinQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintMaterialPriceScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintMaterialPriceTiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintMaterialPriceTiers_TintMaterialPriceSchedules_TintMaterialPriceScheduleId",
                        column: x => x.TintMaterialPriceScheduleId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterialPriceSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintServicePriceOverride",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    CustomPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintTintServicePriceScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintServicePriceOverride", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintServicePriceOverride_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TintServicePriceOverride_TintServicePriceSchedule_TintTintServicePriceScheduleId",
                        column: x => x.TintTintServicePriceScheduleId,
                        principalSchema: "dbo",
                        principalTable: "TintServicePriceSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TintServicePriceTier",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    DateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TintServicePriceScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TintServicePriceTier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TintServicePriceTier_TintServicePriceSchedule_TintServicePriceScheduleId",
                        column: x => x.TintServicePriceScheduleId,
                        principalSchema: "dbo",
                        principalTable: "TintServicePriceSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 72, DateTimeKind.Local).AddTicks(9910)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaborCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdditionalFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceDocRef = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OfferDocumentStatus = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimates_CustomerContacts_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalSchema: "dbo",
                        principalTable: "CustomerContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estimates_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "dbo",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 75, DateTimeKind.Local).AddTicks(1376)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    SolutionDescription = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    ProjectTimeline = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Deliverables = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceDocRef = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OfferDocumentStatus = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_CustomerContacts_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalSchema: "dbo",
                        principalTable: "CustomerContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposals_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "dbo",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 21, 51, 54, 70, DateTimeKind.Local).AddTicks(8380)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SourceDocRef = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OfferDocumentStatus = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    InquiryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_CustomerContacts_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalSchema: "dbo",
                        principalTable: "CustomerContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "dbo",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                schema: "dbo",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_ContactId",
                schema: "dbo",
                table: "CustomerContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                schema: "dbo",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_CustomerContactId",
                schema: "dbo",
                table: "Estimates",
                column: "CustomerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_InquiryId",
                schema: "dbo",
                table: "Estimates",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerContactId",
                schema: "dbo",
                table: "Inquiries",
                column: "CustomerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ProductId",
                schema: "dbo",
                table: "InventoryItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAssets_CustomerId",
                schema: "dbo",
                table: "PropertyAssets",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_CustomerContactId",
                schema: "dbo",
                table: "Proposals",
                column: "CustomerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_InquiryId",
                schema: "dbo",
                table: "Proposals",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_CustomerContactId",
                schema: "dbo",
                table: "Quotes",
                column: "CustomerContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_InquiryId",
                schema: "dbo",
                table: "Quotes",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_TintMaterialPriceHistories_TintMaterialId",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TintMaterialPriceOverrides_CustomerId",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TintMaterialPriceOverrides_TintMaterialPriceScheduleId",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                column: "TintMaterialPriceScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TintMaterialPriceSchedules_TintMaterialId",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TintMaterialPriceTiers_TintMaterialPriceScheduleId",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                column: "TintMaterialPriceScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TintServicePriceOverride_CustomerId",
                schema: "dbo",
                table: "TintServicePriceOverride",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TintServicePriceOverride_TintTintServicePriceScheduleId",
                schema: "dbo",
                table: "TintServicePriceOverride",
                column: "TintTintServicePriceScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TintServicePriceSchedule_TintServiceId",
                schema: "dbo",
                table: "TintServicePriceSchedule",
                column: "TintServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TintServicePriceTier_TintServicePriceScheduleId",
                schema: "dbo",
                table: "TintServicePriceTier",
                column: "TintServicePriceScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Estimates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InventoryItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyAssets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Proposals",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Quotes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintMaterialPriceHistories",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintMaterialPriceOverrides",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintMaterialPriceTiers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintServicePriceOverride",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintServicePriceTier",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Inquiries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintMaterialPriceSchedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintServicePriceSchedule",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerContacts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintMaterials",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TintServices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "dbo");
        }
    }
}
