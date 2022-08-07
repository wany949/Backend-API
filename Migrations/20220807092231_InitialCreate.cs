using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Vision = table.Column<string>(type: "TEXT", nullable: true),
                    Weapon = table.Column<string>(type: "TEXT", nullable: true),
                    Constellation = table.Column<string>(type: "TEXT", nullable: true),
                    Birthday = table.Column<string>(type: "TEXT", nullable: true),
                    Rarity = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
