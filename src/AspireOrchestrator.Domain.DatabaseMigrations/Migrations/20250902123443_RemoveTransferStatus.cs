using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.Domain.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTransferStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferStatus",
                table: "ReceiptDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferStatus",
                table: "ReceiptDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
