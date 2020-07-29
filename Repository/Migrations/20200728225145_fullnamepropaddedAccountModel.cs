using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class fullnamepropaddedAccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Accounts",
                maxLength: 101,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Accounts");
        }
    }
}
