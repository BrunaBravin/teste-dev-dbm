using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteDevDbm.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTabelaProtocolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    IdProtocolo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    ProtocoloStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.IdProtocolo);
                    table.ForeignKey(
                        name: "FK_Protocolos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Protocolos_StatusProtocolos_ProtocoloStatusId",
                        column: x => x.ProtocoloStatusId,
                        principalTable: "StatusProtocolos",
                        principalColumn: "IdStatus",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_ClienteId",
                table: "Protocolos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_ProtocoloStatusId",
                table: "Protocolos",
                column: "ProtocoloStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Protocolos");
        }
    }
}
