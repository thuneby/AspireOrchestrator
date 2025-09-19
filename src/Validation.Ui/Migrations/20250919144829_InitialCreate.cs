using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Validation.Ui.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    TrxDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ValDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ReconcileStatus = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Belob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    AccountReference = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostingJournal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostingDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PostingPurpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingJournal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaborAgreementNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cvr = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    PersonFullName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ReceiptType = table.Column<int>(type: "int", nullable: false),
                    PolicyNumber = table.Column<long>(type: "bigint", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionNumber = table.Column<int>(type: "int", nullable: true),
                    TotalContributionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EmployerContributionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EmployerContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContributionRateFromDate = table.Column<DateTime>(type: "Date", nullable: true),
                    NormalContribution = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NormalContributionStartDate = table.Column<DateTime>(type: "Date", nullable: true),
                    EmploymentTerminationDate = table.Column<DateTime>(type: "Date", nullable: true),
                    DeviationStartDate = table.Column<DateTime>(type: "Date", nullable: true),
                    DeviationEndDate = table.Column<DateTime>(type: "Date", nullable: true),
                    DeviationCode = table.Column<int>(type: "int", nullable: true),
                    EmployeeSalaryStartDate = table.Column<DateTime>(type: "Date", nullable: true),
                    EmployeeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TermsOfSalary = table.Column<int>(type: "int", nullable: true),
                    EmploymentRateStartDate = table.Column<DateTime>(type: "Date", nullable: true),
                    EmploymentRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "Date", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ToDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Cpr = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valid = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    ReconcileStatus = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidationErrors = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefaultTenant = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostingEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostingAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    PostingDocumentType = table.Column<int>(type: "int", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    BankTrxDate = table.Column<DateTime>(type: "Date", nullable: false),
                    BankValDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PostingMessage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostingJournalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostingEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostingEntry_PostingJournal_PostingJournalId",
                        column: x => x.PostingJournalId,
                        principalTable: "PostingJournal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostingEntry_PostingJournalId",
                table: "PostingEntry",
                column: "PostingJournalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "PostingEntry");

            migrationBuilder.DropTable(
                name: "ReceiptDetail");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "PostingJournal");
        }
    }
}
