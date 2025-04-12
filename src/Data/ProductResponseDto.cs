using System;

namespace curvela_backend.src.DTOs
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string SKU { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal? PromotionalPrice { get; set; }
        public string? Description { get; set; }
        public string? Colection { get; set; }
        public string? Size { get; set; }
        public int? StockSize { get; set; }
        public required string Category { get; set; }
        public string? Brand { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created_at { get; set; }
    }
}
