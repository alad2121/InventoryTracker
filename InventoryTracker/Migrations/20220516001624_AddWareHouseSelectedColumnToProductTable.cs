using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryTracker.Migrations
{
    public partial class AddWareHouseSelectedColumnToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "selectedWareHouse",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "selectedWareHouse",
                table: "Products");
        }
    }
}
