using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AccountModelResetPasswordCodePropAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AccountFriend_AccountFriendId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Accounts_AccountId",
                table: "Friendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship");

            migrationBuilder.RenameTable(
                name: "Friendship",
                newName: "FriendShip");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_AccountId",
                table: "FriendShip",
                newName: "IX_FriendShip_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_AccountFriendId",
                table: "FriendShip",
                newName: "IX_FriendShip_AccountFriendId");

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordCode",
                table: "Accounts",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_AccountFriend_AccountFriendId",
                table: "FriendShip",
                column: "AccountFriendId",
                principalTable: "AccountFriend",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_Accounts_AccountId",
                table: "FriendShip",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_AccountFriend_AccountFriendId",
                table: "FriendShip");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_Accounts_AccountId",
                table: "FriendShip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip");

            migrationBuilder.DropColumn(
                name: "ResetPasswordCode",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "FriendShip",
                newName: "Friendship");

            migrationBuilder.RenameIndex(
                name: "IX_FriendShip_AccountId",
                table: "Friendship",
                newName: "IX_Friendship_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendShip_AccountFriendId",
                table: "Friendship",
                newName: "IX_Friendship_AccountFriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_AccountFriend_AccountFriendId",
                table: "Friendship",
                column: "AccountFriendId",
                principalTable: "AccountFriend",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Accounts_AccountId",
                table: "Friendship",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
