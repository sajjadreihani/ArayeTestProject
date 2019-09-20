using Microsoft.EntityFrameworkCore.Migrations;

namespace ArayeTestProject.Api.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Sales");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Sales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CityId",
                table: "Sales",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Cities_CityId",
                table: "Sales",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Cities_CityId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CityId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Sales",
                maxLength: 50,
                nullable: true);
        }
    }
}
