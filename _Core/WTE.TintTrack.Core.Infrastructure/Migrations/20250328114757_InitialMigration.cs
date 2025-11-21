using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Core.Infrastructure.Migrations
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
                name: "AspNetRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UserCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StateOrRegion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CountryISOCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    TimeZone = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 11, 47, 55, 253, DateTimeKind.Utc).AddTicks(4019)),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 11, 47, 55, 256, DateTimeKind.Utc).AddTicks(5377)),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 316, DateTimeKind.Local).AddTicks(816)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Feature = table.Column<int>(type: "int", nullable: false),
                    PermissionLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanFeatures",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 342, DateTimeKind.Local).AddTicks(1725)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    FeatureCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 338, DateTimeKind.Local).AddTicks(8743)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PlanCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BillingCycle = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxUsers = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 350, DateTimeKind.Local).AddTicks(1133)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    LogoImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    TenantStatus = table.Column<int>(type: "int", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CountryOfHost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBillingProfiles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 360, DateTimeKind.Local).AddTicks(3348)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    BillingAddress = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false),
                    BillingProfileType = table.Column<int>(type: "int", nullable: false),
                    BillingDetailsJson = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBillingProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBillingProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 322, DateTimeKind.Local).AddTicks(7792)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "dbo",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanDiscounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 348, DateTimeKind.Local).AddTicks(214)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    PlanDiscountCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanDiscounts_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalSchema: "dbo",
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanFeatureAssociations",
                schema: "dbo",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionPlanFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanFeatureAssociations", x => new { x.SubscriptionPlanId, x.SubscriptionPlanFeatureId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanFeatureAssociations_SubscriptionPlanFeatures_SubscriptionPlanFeatureId",
                        column: x => x.SubscriptionPlanFeatureId,
                        principalSchema: "dbo",
                        principalTable: "SubscriptionPlanFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanFeatureAssociations_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalSchema: "dbo",
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSubscriptions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 354, DateTimeKind.Local).AddTicks(3184)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    SubscriptionStatus = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalSchema: "dbo",
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 334, DateTimeKind.Local).AddTicks(4393)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tokens_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTenantInvitations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 352, DateTimeKind.Local).AddTicks(1792)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InvitationStatus = table.Column<int>(type: "int", nullable: false),
                    InvitationSource = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenantInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTenantInvitations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenantInvitations_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTenants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 362, DateTimeKind.Local).AddTicks(4156)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    UserIsOwner = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTenants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "dbo",
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSubscriptionInvoices",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 356, DateTimeKind.Local).AddTicks(3544)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    InvoiceCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    LateFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TenantSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSubscriptionInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptionInvoices_TenantSubscriptions_TenantSubscriptionId",
                        column: x => x.TenantSubscriptionId,
                        principalSchema: "dbo",
                        principalTable: "TenantSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptionInvoices_UserBillingProfiles_BillingProfileId",
                        column: x => x.BillingProfileId,
                        principalSchema: "dbo",
                        principalTable: "UserBillingProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTenantRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 364, DateTimeKind.Local).AddTicks(2547)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    UserTenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenantRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTenantRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenantRoles_UserTenants_UserTenantId",
                        column: x => x.UserTenantId,
                        principalSchema: "dbo",
                        principalTable: "UserTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantSubscriptionPayments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 3, 28, 19, 47, 55, 358, DateTimeKind.Local).AddTicks(6004)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSubscriptionPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptionPayments_TenantSubscriptionInvoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "dbo",
                        principalTable: "TenantSubscriptionInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "dbo",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "dbo",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanDiscounts_SubscriptionPlanId",
                schema: "dbo",
                table: "SubscriptionPlanDiscounts",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanFeatureAssociations_SubscriptionPlanFeatureId",
                schema: "dbo",
                table: "SubscriptionPlanFeatureAssociations",
                column: "SubscriptionPlanFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptionInvoices_BillingProfileId",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                column: "BillingProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptionInvoices_TenantSubscriptionId",
                schema: "dbo",
                table: "TenantSubscriptionInvoices",
                column: "TenantSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptionPayments_InvoiceId",
                schema: "dbo",
                table: "TenantSubscriptionPayments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptions_SubscriptionPlanId",
                schema: "dbo",
                table: "TenantSubscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptions_TenantId",
                schema: "dbo",
                table: "TenantSubscriptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_TenantId",
                schema: "dbo",
                table: "Tokens",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                schema: "dbo",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBillingProfiles_UserId",
                schema: "dbo",
                table: "UserBillingProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantInvitations_TenantId",
                schema: "dbo",
                table: "UserTenantInvitations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantInvitations_UserId",
                schema: "dbo",
                table: "UserTenantInvitations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRoles_RoleId",
                schema: "dbo",
                table: "UserTenantRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenantRoles_UserTenantId",
                schema: "dbo",
                table: "UserTenantRoles",
                column: "UserTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenants_TenantId",
                schema: "dbo",
                table: "UserTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTenants_UserId",
                schema: "dbo",
                table: "UserTenants",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanDiscounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanFeatureAssociations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TenantSubscriptionPayments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTenantInvitations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTenantRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanFeatures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TenantSubscriptionInvoices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTenants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TenantSubscriptions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserBillingProfiles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "dbo");
        }
    }
}
