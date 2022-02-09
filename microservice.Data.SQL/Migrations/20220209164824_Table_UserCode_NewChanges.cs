using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservice.Data.SQL.Migrations
{
    public partial class Table_UserCode_NewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCodes_Users_UserId",
                table: "UsersCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersCodes",
                table: "UsersCodes");

            migrationBuilder.RenameTable(
                name: "UsersCodes",
                newName: "UserCode");

            migrationBuilder.RenameIndex(
                name: "IX_UsersCodes_UserId",
                table: "UserCode",
                newName: "IX_UserCode_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCode",
                table: "UserCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCode_Users_UserId",
                table: "UserCode",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCode_Users_UserId",
                table: "UserCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCode",
                table: "UserCode");

            migrationBuilder.RenameTable(
                name: "UserCode",
                newName: "UsersCodes");

            migrationBuilder.RenameIndex(
                name: "IX_UserCode_UserId",
                table: "UsersCodes",
                newName: "IX_UsersCodes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersCodes",
                table: "UsersCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCodes_Users_UserId",
                table: "UsersCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
