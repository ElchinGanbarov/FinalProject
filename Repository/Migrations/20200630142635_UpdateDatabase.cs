using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDetails");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AccountPrivacies");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "AccountPrivacies");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "AccountPrivacies");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "AccountPrivacies");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Accounts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeen",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImg",
                table: "Accounts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusText",
                table: "Accounts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Accounts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SocialLink",
                table: "AccountPrivacies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AccountSocialLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    AddedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    Facebook = table.Column<string>(maxLength: 100, nullable: true),
                    Twitter = table.Column<string>(maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(maxLength: 100, nullable: true),
                    Linkedin = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSocialLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSocialLinks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSocialLinks_AccountId",
                table: "AccountSocialLinks",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSocialLinks");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ProfileImg",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "StatusText",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SocialLink",
                table: "AccountPrivacies");

            migrationBuilder.AddColumn<bool>(
                name: "Facebook",
                table: "AccountPrivacies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Instagram",
                table: "AccountPrivacies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Linkedin",
                table: "AccountPrivacies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Twitter",
                table: "AccountPrivacies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Linkedin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileImg = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StatusText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountDetails_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetails_AccountId",
                table: "AccountDetails",
                column: "AccountId");
        }
    }
}
