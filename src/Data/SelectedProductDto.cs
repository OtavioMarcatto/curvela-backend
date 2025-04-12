namespace curvela_backend.src.Data
{
    public class SelectedProductDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
    }

    public class SelectedProductsResponseDto(List<SelectedProductDto> products, decimal totalPrice)
    {
        public List<SelectedProductDto> Products { get; set; } = products;
        public decimal TotalPrice { get; set; } = totalPrice;
    }
}
