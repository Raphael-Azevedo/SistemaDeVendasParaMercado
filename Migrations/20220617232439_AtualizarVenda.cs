using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoMercadinho.Migrations
{
    public partial class AtualizarVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saidas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Saidas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas");

            migrationBuilder.DropIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Saidas");
        }
    }
}
