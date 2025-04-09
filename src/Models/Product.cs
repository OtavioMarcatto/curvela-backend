using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace curvela_backend.src.Models
{
    [Table("product", Schema = "material")]
    [Index(nameof(Id), Name = "product_id")]
    public class Product
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string SKU { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public decimal? PromotionalPrice { get; set; }

        public string? Description { get; set; }

        public required string Category { get; set; }

        public string? Brand { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
