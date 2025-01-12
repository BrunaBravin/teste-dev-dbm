using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteDevDbm.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTabelaProtocoloFollow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProtocolosFollow",
                columns: table => new
                {
                    IdFollow = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProtocoloId = table.Column<int>(type: "int", nullable: false),
                    DataAcao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescricaoAcao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtocolosFollow", x => x.IdFollow);
                    table.ForeignKey(
                        name: "FK_ProtocolosFollow_Protocolos_ProtocoloId",
                        column: x => x.ProtocoloId,
                        principalTable: "Protocolos",
                        principalColumn: "IdProtocolo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolosFollow_ProtocoloId",
                table: "ProtocolosFollow",
                column: "ProtocoloId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProtocolosFollow");
        }
    }
}
