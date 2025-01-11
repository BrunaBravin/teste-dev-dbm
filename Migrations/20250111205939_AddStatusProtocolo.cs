using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TesteDevDbm.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusProtocolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusProtocolos",
                columns: table => new
                {
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusProtocolos", x => x.IdStatus);
                });

            migrationBuilder.InsertData(
                table: "StatusProtocolos",
                columns: new[] { "IdStatus", "NomeStatus" },
                values: new object[,]
                {
                    { 1, "Aberto" },
                    { 2, "Em Andamento" },
                    { 3, "Fechado" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusProtocolos");
        }
    }
}
