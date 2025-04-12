using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace curvela_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFlagSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                schema: "material",
                table: "product",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                schema: "material",
                table: "product");
        }
    }
}
