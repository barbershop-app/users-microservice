using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservice.Data.SQL.Migrations
{
    public partial class Table_UserCode_Modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UsersCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersCodes_UserId",
                table: "UsersCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersCodes_Users_UserId",
                table: "UsersCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersCodes_Users_UserId",
                table: "UsersCodes");

            migrationBuilder.DropIndex(
                name: "IX_UsersCodes_UserId",
                table: "UsersCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersCodes");
        }
    }
}
