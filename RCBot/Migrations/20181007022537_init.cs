using Microsoft.EntityFrameworkCore.Migrations;

namespace RCBot.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<ulong>(nullable: false),
                    inGameName = table.Column<string>(nullable: true),
                    inviteID = table.Column<string>(nullable: true),
                    forTrade = table.Column<string>(nullable: true),
                    stock = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "members");
        }
    }
}
