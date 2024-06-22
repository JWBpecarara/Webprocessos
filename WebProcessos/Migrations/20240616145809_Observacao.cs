using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProcessos.Migrations
{
    /// <inheritdoc />
    public partial class Observacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "OrdemServico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "OrdemServico");
        }
    }
}
