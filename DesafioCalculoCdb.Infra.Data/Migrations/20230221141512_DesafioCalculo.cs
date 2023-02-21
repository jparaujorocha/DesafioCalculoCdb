using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioCalculoCdb.Infra.Data.Migrations
{
    public partial class DesafioCalculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    DataDeCriacao = table.Column<DateTime>(nullable: false),
                    DataDeFinalizacao = table.Column<DateTime>(nullable: true),
                    ValorTaxaInvestimento = table.Column<decimal>(nullable: false),
                    ValorTaxaBanco = table.Column<decimal>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoImpostos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImpostos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Impostos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    IdTipoImposto = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impostos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Impostos_TipoImpostos_IdTipoImposto",
                        column: x => x.IdTipoImposto,
                        principalTable: "TipoImpostos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImpostoInvestimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdImposto = table.Column<int>(nullable: false),
                    IdInvestimento = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpostoInvestimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImpostoInvestimentos_Impostos_IdImposto",
                        column: x => x.IdImposto,
                        principalTable: "Impostos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImpostoInvestimentos_Investimentos_IdInvestimento",
                        column: x => x.IdInvestimento,
                        principalTable: "Investimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Investimentos",
                columns: new[] { "Id", "Ativo", "DataDeCriacao", "DataDeFinalizacao", "Nome", "ValorTaxaBanco", "ValorTaxaInvestimento" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2023, 2, 21, 11, 15, 11, 196, DateTimeKind.Local).AddTicks(9707), null, "CDB", 108m, 0.9m },
                    { 2, false, new DateTime(2023, 2, 21, 11, 15, 11, 196, DateTimeKind.Local).AddTicks(9707), null, "OUTROS", 112m, 0.1m }
                });

            migrationBuilder.InsertData(
                table: "TipoImpostos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "PERCENTUAL" },
                    { 2, "VALOR FIXO" }
                });

            migrationBuilder.InsertData(
                table: "Impostos",
                columns: new[] { "Id", "Ativo", "IdTipoImposto", "Nome", "Valor" },
                values: new object[,]
                {
                    { 1, true, 1, "CDB6", 22.5m },
                    { 2, true, 1, "CDB12", 20m },
                    { 3, true, 1, "CDB24", 17.5m },
                    { 4, true, 1, "CDB24PLUS", 15m }
                });

            migrationBuilder.InsertData(
                table: "ImpostoInvestimentos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataInicio", "IdImposto", "IdInvestimento" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2023, 2, 21, 11, 15, 11, 186, DateTimeKind.Local).AddTicks(8957), 1, 1 },
                    { 2, true, null, new DateTime(2023, 2, 21, 11, 15, 11, 194, DateTimeKind.Local).AddTicks(9553), 2, 1 },
                    { 3, true, null, new DateTime(2023, 2, 21, 11, 15, 11, 194, DateTimeKind.Local).AddTicks(9553), 3, 1 },
                    { 4, true, null, new DateTime(2023, 2, 21, 11, 15, 11, 194, DateTimeKind.Local).AddTicks(9553), 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImpostoInvestimentos_IdImposto",
                table: "ImpostoInvestimentos",
                column: "IdImposto");

            migrationBuilder.CreateIndex(
                name: "IX_ImpostoInvestimentos_IdInvestimento",
                table: "ImpostoInvestimentos",
                column: "IdInvestimento");

            migrationBuilder.CreateIndex(
                name: "IX_Impostos_IdTipoImposto",
                table: "Impostos",
                column: "IdTipoImposto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpostoInvestimentos");

            migrationBuilder.DropTable(
                name: "Impostos");

            migrationBuilder.DropTable(
                name: "Investimentos");

            migrationBuilder.DropTable(
                name: "TipoImpostos");
        }
    }
}
