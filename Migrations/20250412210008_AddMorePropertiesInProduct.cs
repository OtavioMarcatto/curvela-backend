using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace curvela_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMorePropertiesInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colection",
                schema: "material",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                schema: "material",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockSize",
                schema: "material",
                table: "product",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colection",
                schema: "material",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Size",
                schema: "material",
                table: "product");

            migrationBuilder.DropColumn(
                name: "StockSize",
                schema: "material",
                table: "product");
        }
    }
}
