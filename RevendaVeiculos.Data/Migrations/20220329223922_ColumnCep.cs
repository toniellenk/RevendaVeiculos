using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevendaVeiculos.Data.Migrations
{
    public partial class ColumnCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Proprietario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Proprietario");
        }
    }
}
