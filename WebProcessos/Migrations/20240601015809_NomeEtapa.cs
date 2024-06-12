using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProcessos.Migrations
{
    /// <inheritdoc />
    public partial class NomeEtapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Etapa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Etapa");
        }
    }
}
