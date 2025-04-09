using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace curvela_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDefaultValueForProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "material");

            migrationBuilder.CreateTable(
                name: "product",
                schema: "material",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PromotionalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "product_id",
                schema: "material",
                table: "product",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product",
                schema: "material");
        }
    }
}
