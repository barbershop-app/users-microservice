using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservice.Data.SQL.Migrations
{
    public partial class User_RemovedIsAuthenticated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuthenticated",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuthenticated",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
