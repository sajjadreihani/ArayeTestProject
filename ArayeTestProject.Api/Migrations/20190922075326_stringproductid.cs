using Microsoft.EntityFrameworkCore.Migrations;

namespace ArayeTestProject.Api.Migrations
{
    public partial class stringproductid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Sales",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
