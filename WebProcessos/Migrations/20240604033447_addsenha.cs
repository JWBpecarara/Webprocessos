using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProcessos.Migrations
{
    /// <inheritdoc />
    public partial class addsenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Servico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Servico");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Cliente");

        }
    }
}
