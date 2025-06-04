using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class FKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventEntity_TenantId",
                table: "EventEntity",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEntity_Tenant_TenantId",
                table: "EventEntity",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEntity_Tenant_TenantId",
                table: "EventEntity");

            migrationBuilder.DropIndex(
                name: "IX_EventEntity_TenantId",
                table: "EventEntity");
        }
    }
}
