using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeEstoque.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoDetalhada",
                table: "Produtos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SKU",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescricaoDetalhada",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Produtos");
        }
    }
}
