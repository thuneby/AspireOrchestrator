using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.Domain.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class PostingEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferStatus",
                table: "ReceiptDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "PostingEntry");

            migrationBuilder.DropTable(
                name: "PostingJournal");

            migrationBuilder.DropColumn(
                name: "TransferStatus",
                table: "ReceiptDetail");
        }
    }
}
