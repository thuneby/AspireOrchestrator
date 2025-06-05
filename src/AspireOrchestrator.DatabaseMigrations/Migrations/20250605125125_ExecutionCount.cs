using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireOrchestrator.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class ExecutionCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ExecutionCount",
                table: "EventEntity",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionCount",
                table: "EventEntity");
        }
    }
}
