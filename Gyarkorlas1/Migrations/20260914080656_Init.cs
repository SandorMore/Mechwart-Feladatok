using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gyarkorlas1.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "konyvek",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cim = table.Column<string>(type: "TEXT", nullable: false),
                    szerzo = table.Column<string>(type: "TEXT", nullable: false),
                    kiadas_eve = table.Column<string>(type: "TEXT", nullable: false),
                    ar = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_konyvek", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "konyvek");
        }
    }
}
