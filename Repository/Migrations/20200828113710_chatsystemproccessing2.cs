using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class chatsystemproccessing2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HubType",
                table: "Hubs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HubType",
                table: "Hubs");
        }
    }
}
