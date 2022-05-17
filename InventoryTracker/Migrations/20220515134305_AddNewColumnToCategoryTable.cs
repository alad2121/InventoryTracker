using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryTracker.Migrations
{
    public partial class AddNewColumnToCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfProducts",
                table: "Locations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfProducts",
                table: "Categories",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfProducts",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "NumberOfProducts",
                table: "Categories");
        }
    }
}
