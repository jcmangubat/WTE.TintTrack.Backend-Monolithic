using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WTE.TintTrack.Business.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferHistory_OfferRecipient_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestone_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferMilestone");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestone_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferMilestone");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestone_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferMilestone");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipient_CustomerContacts_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipient_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipient_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipient_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferRecipient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferRecipient",
                schema: "dbo",
                table: "OfferRecipient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferMilestone",
                schema: "dbo",
                table: "OfferMilestone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferHistory",
                schema: "dbo",
                table: "OfferHistory");

            migrationBuilder.RenameTable(
                name: "OfferRecipient",
                schema: "dbo",
                newName: "OfferRecipients",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OfferMilestone",
                schema: "dbo",
                newName: "OfferMilestones",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OfferHistory",
                schema: "dbo",
                newName: "OfferHistories",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipient_QuoteId",
                schema: "dbo",
                table: "OfferRecipients",
                newName: "IX_OfferRecipients_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipient_ProposalId",
                schema: "dbo",
                table: "OfferRecipients",
                newName: "IX_OfferRecipients_ProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipient_EstimateId",
                schema: "dbo",
                table: "OfferRecipients",
                newName: "IX_OfferRecipients_EstimateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipient_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipients",
                newName: "IX_OfferRecipients_CustomerContactId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestone_QuoteId",
                schema: "dbo",
                table: "OfferMilestones",
                newName: "IX_OfferMilestones_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestone_ProposalId",
                schema: "dbo",
                table: "OfferMilestones",
                newName: "IX_OfferMilestones_ProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestone_EstimateId",
                schema: "dbo",
                table: "OfferMilestones",
                newName: "IX_OfferMilestones_EstimateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferHistory_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistories",
                newName: "IX_OfferHistories_OfferRecipientId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 263, DateTimeKind.Local).AddTicks(6347),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 593, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 268, DateTimeKind.Local).AddTicks(8841),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 598, DateTimeKind.Local).AddTicks(5342));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 278, DateTimeKind.Local).AddTicks(5084),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 607, DateTimeKind.Local).AddTicks(8556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 273, DateTimeKind.Local).AddTicks(5670),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 603, DateTimeKind.Local).AddTicks(368));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 272, DateTimeKind.Local).AddTicks(1279),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 601, DateTimeKind.Local).AddTicks(6237));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 271, DateTimeKind.Local).AddTicks(2967),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 600, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 280, DateTimeKind.Local).AddTicks(267),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 609, DateTimeKind.Local).AddTicks(3619));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                schema: "dbo",
                table: "Quotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 285, DateTimeKind.Local).AddTicks(116),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 613, DateTimeKind.Local).AddTicks(9039));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                schema: "dbo",
                table: "Proposals",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 258, DateTimeKind.Local).AddTicks(3398),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 588, DateTimeKind.Local).AddTicks(4485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 266, DateTimeKind.Local).AddTicks(4901),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 596, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 282, DateTimeKind.Local).AddTicks(5277),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 611, DateTimeKind.Local).AddTicks(6277));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                schema: "dbo",
                table: "Estimates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 249, DateTimeKind.Local).AddTicks(3722),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 580, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 256, DateTimeKind.Local).AddTicks(9032),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 587, DateTimeKind.Local).AddTicks(622));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 254, DateTimeKind.Local).AddTicks(7074),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 585, DateTimeKind.Local).AddTicks(845));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 244, DateTimeKind.Local).AddTicks(1171),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 576, DateTimeKind.Local).AddTicks(7136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 262, DateTimeKind.Local).AddTicks(3519),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 592, DateTimeKind.Local).AddTicks(2898));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "OfferRecipients",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "OfferHistories",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                schema: "dbo",
                table: "OfferHistories",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedByUserCode",
                schema: "dbo",
                table: "OfferHistories",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferRecipients",
                schema: "dbo",
                table: "OfferRecipients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferMilestones",
                schema: "dbo",
                table: "OfferMilestones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferHistories",
                schema: "dbo",
                table: "OfferHistories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Contracts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _dateCreated = table.Column<DateTime>(type: "DateTime2", nullable: false, defaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 289, DateTimeKind.Local).AddTicks(9757)),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    _dateModified = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: true),
                    DateArchived = table.Column<DateTime>(type: "DateTime2", nullable: true),
                    ReasonArchived = table.Column<string>(type: "nvarchar(240)", maxLength: 240, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BillingType = table.Column<int>(type: "int", nullable: false),
                    FixedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentTerm = table.Column<int>(type: "int", nullable: false),
                    IsPaidInFull = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    IsViewed = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    SignatureType = table.Column<int>(type: "int", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false),
                    SignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignatureUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    SignedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SignatureProvider = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    SignatureEnvelopeId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EstimateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractMilestone",
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
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpectedStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMilestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractMilestone_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "dbo",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ContractId",
                schema: "dbo",
                table: "Quotes",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ContractId",
                schema: "dbo",
                table: "Proposals",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_ContractId",
                schema: "dbo",
                table: "Estimates",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMilestone_ContractId",
                schema: "dbo",
                table: "ContractMilestone",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_Contracts_ContractId",
                schema: "dbo",
                table: "Estimates",
                column: "ContractId",
                principalSchema: "dbo",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferHistories_OfferRecipients_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistories",
                column: "OfferRecipientId",
                principalSchema: "dbo",
                principalTable: "OfferRecipients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestones_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferMilestones",
                column: "EstimateId",
                principalSchema: "dbo",
                principalTable: "Estimates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestones_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferMilestones",
                column: "ProposalId",
                principalSchema: "dbo",
                principalTable: "Proposals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestones_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferMilestones",
                column: "QuoteId",
                principalSchema: "dbo",
                principalTable: "Quotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipients_CustomerContacts_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipients",
                column: "CustomerContactId",
                principalSchema: "dbo",
                principalTable: "CustomerContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipients_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferRecipients",
                column: "EstimateId",
                principalSchema: "dbo",
                principalTable: "Estimates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipients_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferRecipients",
                column: "ProposalId",
                principalSchema: "dbo",
                principalTable: "Proposals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipients_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferRecipients",
                column: "QuoteId",
                principalSchema: "dbo",
                principalTable: "Quotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Contracts_ContractId",
                schema: "dbo",
                table: "Proposals",
                column: "ContractId",
                principalSchema: "dbo",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Contracts_ContractId",
                schema: "dbo",
                table: "Quotes",
                column: "ContractId",
                principalSchema: "dbo",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_Contracts_ContractId",
                schema: "dbo",
                table: "Estimates");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferHistories_OfferRecipients_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestones_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferMilestones");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestones_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferMilestones");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferMilestones_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferMilestones");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipients_CustomerContacts_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipients_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipients_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferRecipients_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Contracts_ContractId",
                schema: "dbo",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Contracts_ContractId",
                schema: "dbo",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "ContractMilestone",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Contracts",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_ContractId",
                schema: "dbo",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ContractId",
                schema: "dbo",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Estimates_ContractId",
                schema: "dbo",
                table: "Estimates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferRecipients",
                schema: "dbo",
                table: "OfferRecipients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferMilestones",
                schema: "dbo",
                table: "OfferMilestones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferHistories",
                schema: "dbo",
                table: "OfferHistories");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "dbo",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "dbo",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "dbo",
                table: "Estimates");

            migrationBuilder.RenameTable(
                name: "OfferRecipients",
                schema: "dbo",
                newName: "OfferRecipient",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OfferMilestones",
                schema: "dbo",
                newName: "OfferMilestone",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "OfferHistories",
                schema: "dbo",
                newName: "OfferHistory",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipients_QuoteId",
                schema: "dbo",
                table: "OfferRecipient",
                newName: "IX_OfferRecipient_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipients_ProposalId",
                schema: "dbo",
                table: "OfferRecipient",
                newName: "IX_OfferRecipient_ProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipients_EstimateId",
                schema: "dbo",
                table: "OfferRecipient",
                newName: "IX_OfferRecipient_EstimateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferRecipients_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipient",
                newName: "IX_OfferRecipient_CustomerContactId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestones_QuoteId",
                schema: "dbo",
                table: "OfferMilestone",
                newName: "IX_OfferMilestone_QuoteId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestones_ProposalId",
                schema: "dbo",
                table: "OfferMilestone",
                newName: "IX_OfferMilestone_ProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferMilestones_EstimateId",
                schema: "dbo",
                table: "OfferMilestone",
                newName: "IX_OfferMilestone_EstimateId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferHistories_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistory",
                newName: "IX_OfferHistory_OfferRecipientId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintServices",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 593, DateTimeKind.Local).AddTicks(4440),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 263, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterials",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 598, DateTimeKind.Local).AddTicks(5342),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 268, DateTimeKind.Local).AddTicks(8841));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceTiers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 607, DateTimeKind.Local).AddTicks(8556),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 278, DateTimeKind.Local).AddTicks(5084));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceSchedules",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 603, DateTimeKind.Local).AddTicks(368),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 273, DateTimeKind.Local).AddTicks(5670));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceOverrides",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 601, DateTimeKind.Local).AddTicks(6237),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 272, DateTimeKind.Local).AddTicks(1279));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "TintMaterialPriceHistories",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 600, DateTimeKind.Local).AddTicks(7071),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 271, DateTimeKind.Local).AddTicks(2967));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Quotes",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 609, DateTimeKind.Local).AddTicks(3619),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 280, DateTimeKind.Local).AddTicks(267));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Proposals",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 613, DateTimeKind.Local).AddTicks(9039),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 285, DateTimeKind.Local).AddTicks(116));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "PropertyAssets",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 588, DateTimeKind.Local).AddTicks(4485),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 258, DateTimeKind.Local).AddTicks(3398));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Inquiries",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 596, DateTimeKind.Local).AddTicks(3091),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 266, DateTimeKind.Local).AddTicks(4901));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Estimates",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 611, DateTimeKind.Local).AddTicks(6277),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 282, DateTimeKind.Local).AddTicks(5277));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Customers",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 580, DateTimeKind.Local).AddTicks(6936),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 249, DateTimeKind.Local).AddTicks(3722));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "CustomerContacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 587, DateTimeKind.Local).AddTicks(622),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 256, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Contacts",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 585, DateTimeKind.Local).AddTicks(845),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 254, DateTimeKind.Local).AddTicks(7074));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "AuditLogs",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 576, DateTimeKind.Local).AddTicks(7136),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 244, DateTimeKind.Local).AddTicks(1171));

            migrationBuilder.AlterColumn<DateTime>(
                name: "_dateCreated",
                schema: "dbo",
                table: "Addresses",
                type: "DateTime2",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 28, 22, 16, 51, 592, DateTimeKind.Local).AddTicks(2898),
                oldClrType: typeof(DateTime),
                oldType: "DateTime2",
                oldDefaultValue: new DateTime(2025, 4, 28, 22, 26, 22, 262, DateTimeKind.Local).AddTicks(3519));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "OfferRecipient",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "OfferHistory",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                schema: "dbo",
                table: "OfferHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedByUserCode",
                schema: "dbo",
                table: "OfferHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferRecipient",
                schema: "dbo",
                table: "OfferRecipient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferMilestone",
                schema: "dbo",
                table: "OfferMilestone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferHistory",
                schema: "dbo",
                table: "OfferHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferHistory_OfferRecipient_OfferRecipientId",
                schema: "dbo",
                table: "OfferHistory",
                column: "OfferRecipientId",
                principalSchema: "dbo",
                principalTable: "OfferRecipient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestone_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "EstimateId",
                principalSchema: "dbo",
                principalTable: "Estimates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestone_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "ProposalId",
                principalSchema: "dbo",
                principalTable: "Proposals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferMilestone_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferMilestone",
                column: "QuoteId",
                principalSchema: "dbo",
                principalTable: "Quotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipient_CustomerContacts_CustomerContactId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "CustomerContactId",
                principalSchema: "dbo",
                principalTable: "CustomerContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipient_Estimates_EstimateId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "EstimateId",
                principalSchema: "dbo",
                principalTable: "Estimates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipient_Proposals_ProposalId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "ProposalId",
                principalSchema: "dbo",
                principalTable: "Proposals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferRecipient_Quotes_QuoteId",
                schema: "dbo",
                table: "OfferRecipient",
                column: "QuoteId",
                principalSchema: "dbo",
                principalTable: "Quotes",
                principalColumn: "Id");
        }
    }
}
