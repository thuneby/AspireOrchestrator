using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.Transfer.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transfer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankTrxDate = table.Column<DateTime>(type: "Date", nullable: false),
                    BankValDate = table.Column<DateTime>(type: "Date", nullable: false),
                    FromDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ToDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferStatus = table.Column<int>(type: "int", nullable: false),
                    TransferCount = table.Column<int>(type: "int", nullable: false),
                    Parent = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transfer");
        }
    }
}
