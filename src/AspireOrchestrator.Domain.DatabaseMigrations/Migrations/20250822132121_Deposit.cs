using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.Domain.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class Deposit : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposit");
        }
    }
}
