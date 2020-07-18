using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class testFriendModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Accounts_FromUserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Accounts_ToUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FromUserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ToUserId",
                table: "Friends");

            migrationBuilder.AlterColumn<int>(
                name: "ToUserId",
                table: "Friends",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FromUserId",
                table: "Friends",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ToUserId",
                table: "Friends",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FromUserId",
                table: "Friends",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FromUserId",
                table: "Friends",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ToUserId",
                table: "Friends",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Accounts_FromUserId",
                table: "Friends",
                column: "FromUserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Accounts_ToUserId",
                table: "Friends",
                column: "ToUserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
