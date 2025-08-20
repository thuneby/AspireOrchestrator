using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.Domain.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class DocumentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "ReceiptDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "ReceiptDetail");
        }
    }
}
