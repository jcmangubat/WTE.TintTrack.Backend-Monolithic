using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkOrderLogsToPrj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 45, DateTimeKind.Local).AddTicks(6370),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 175, DateTimeKind.Local).AddTicks(6391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 52, DateTimeKind.Local).AddTicks(4386),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 186, DateTimeKind.Local).AddTicks(8953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 62, DateTimeKind.Local).AddTicks(6058),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 199, DateTimeKind.Local).AddTicks(9700));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 57, DateTimeKind.Local).AddTicks(4860),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 193, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 56, DateTimeKind.Local).AddTicks(2018),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 191, DateTimeKind.Local).AddTicks(8696));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 55, DateTimeKind.Local).AddTicks(4812),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 190, DateTimeKind.Local).AddTicks(8989));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 64, DateTimeKind.Local).AddTicks(2748),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 202, DateTimeKind.Local).AddTicks(4707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 69, DateTimeKind.Local).AddTicks(8560),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 211, DateTimeKind.Local).AddTicks(1144));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 39, DateTimeKind.Local).AddTicks(6199),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 165, DateTimeKind.Local).AddTicks(5614));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 83, DateTimeKind.Local).AddTicks(3098),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 234, DateTimeKind.Local).AddTicks(3941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 86, DateTimeKind.Local).AddTicks(6535),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 237, DateTimeKind.Local).AddTicks(8671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 49, DateTimeKind.Local).AddTicks(5708),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 181, DateTimeKind.Local).AddTicks(5134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 67, DateTimeKind.Local).AddTicks(1527),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 206, DateTimeKind.Local).AddTicks(9810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 31, DateTimeKind.Local).AddTicks(2533),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 154, DateTimeKind.Local).AddTicks(1855));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 37, DateTimeKind.Local).AddTicks(6722),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 163, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 74, DateTimeKind.Local).AddTicks(6731),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 219, DateTimeKind.Local).AddTicks(3421));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 35, DateTimeKind.Local).AddTicks(8512),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 161, DateTimeKind.Local).AddTicks(1548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 27, DateTimeKind.Local).AddTicks(2633),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 148, DateTimeKind.Local).AddTicks(1396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 43, DateTimeKind.Local).AddTicks(7296),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 171, DateTimeKind.Local).AddTicks(8664));

            migrationBuilder.CreateTable(
                name: "WorkOrder",
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkOrderStatus = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectMilestoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrder_ProjectMilestones_ProjectMilestoneId",
                        column: x => x.ProjectMilestoneId,
                        principalSchema: "dbo",
                        principalTable: "ProjectMilestones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrder_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "dbo",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderAssignment",
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
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderAssignment_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "dbo",
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderItem",
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TintServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TintMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderItem_TintMaterials_TintMaterialId",
                        column: x => x.TintMaterialId,
                        principalSchema: "dbo",
                        principalTable: "TintMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderItem_TintServices_TintServiceId",
                        column: x => x.TintServiceId,
                        principalSchema: "dbo",
                        principalTable: "TintServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderItem_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "dbo",
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderLog",
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
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderLog_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "dbo",
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderLogPhoto",
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
                    FileCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderLogPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderLogPhoto_WorkOrderLog_WorkOrderLogId",
                        column: x => x.WorkOrderLogId,
                        principalSchema: "dbo",
                        principalTable: "WorkOrderLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ProjectId",
                schema: "dbo",
                table: "WorkOrder",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_ProjectMilestoneId",
                schema: "dbo",
                table: "WorkOrder",
                column: "ProjectMilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderAssignment_WorkOrderId",
                schema: "dbo",
                table: "WorkOrderAssignment",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItem_TintMaterialId",
                schema: "dbo",
                table: "WorkOrderItem",
                column: "TintMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItem_TintServiceId",
                schema: "dbo",
                table: "WorkOrderItem",
                column: "TintServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItem_WorkOrderId",
                schema: "dbo",
                table: "WorkOrderItem",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLog_WorkOrderId",
                schema: "dbo",
                table: "WorkOrderLog",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLogPhoto_WorkOrderLogId",
                schema: "dbo",
                table: "WorkOrderLogPhoto",
                column: "WorkOrderLogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderAssignment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrderItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrderLogPhoto",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrderLog",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "WorkOrder",
                schema: "dbo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 175, DateTimeKind.Local).AddTicks(6391),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 45, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 186, DateTimeKind.Local).AddTicks(8953),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 52, DateTimeKind.Local).AddTicks(4386));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 199, DateTimeKind.Local).AddTicks(9700),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 62, DateTimeKind.Local).AddTicks(6058));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 193, DateTimeKind.Local).AddTicks(5614),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 57, DateTimeKind.Local).AddTicks(4860));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 191, DateTimeKind.Local).AddTicks(8696),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 56, DateTimeKind.Local).AddTicks(2018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 190, DateTimeKind.Local).AddTicks(8989),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 55, DateTimeKind.Local).AddTicks(4812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 202, DateTimeKind.Local).AddTicks(4707),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 64, DateTimeKind.Local).AddTicks(2748));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 211, DateTimeKind.Local).AddTicks(1144),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 69, DateTimeKind.Local).AddTicks(8560));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 165, DateTimeKind.Local).AddTicks(5614),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 39, DateTimeKind.Local).AddTicks(6199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Projects",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 234, DateTimeKind.Local).AddTicks(3941),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 83, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "ProjectMilestones",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 237, DateTimeKind.Local).AddTicks(8671),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 86, DateTimeKind.Local).AddTicks(6535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 181, DateTimeKind.Local).AddTicks(5134),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 49, DateTimeKind.Local).AddTicks(5708));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 206, DateTimeKind.Local).AddTicks(9810),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 67, DateTimeKind.Local).AddTicks(1527));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 154, DateTimeKind.Local).AddTicks(1855),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 31, DateTimeKind.Local).AddTicks(2533));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 163, DateTimeKind.Local).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 37, DateTimeKind.Local).AddTicks(6722));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contracts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 219, DateTimeKind.Local).AddTicks(3421),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 74, DateTimeKind.Local).AddTicks(6731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 161, DateTimeKind.Local).AddTicks(1548),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 35, DateTimeKind.Local).AddTicks(8512));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 148, DateTimeKind.Local).AddTicks(1396),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 27, DateTimeKind.Local).AddTicks(2633));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 23, 2, 58, 171, DateTimeKind.Local).AddTicks(8664),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 23, 22, 15, 43, DateTimeKind.Local).AddTicks(7296));
        }
    }
}
