using curvela_backend.Data;
using curvela_backend.src.Data;
using curvela_backend.src.DTOs;
using curvela_backend.src.Models;
using Microsoft.EntityFrameworkCore;

namespace curvela_backend.src.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(
            string? categoria,
            string? colecao,
            string? nome,
            int page,
            int pageSize
        )
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(categoria))
                query = query.Where(p => p.Category == categoria);

            if (!string.IsNullOrWhiteSpace(colecao))
                query = query.Where(p => p.Colection == colecao);

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(p => p.Name.Contains(nome));

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var products = await query.ToListAsync();

            var dtoList = products
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SKU = p.SKU,
                    Stock = p.Stock,
                    Price = p.Price,
                    PromotionalPrice = p.PromotionalPrice,
                    Description = p.Description,
                    Colection = p.Colection,
                    Size = p.Size,
                    StockSize = p.StockSize,
                    Category = p.Category,
                    Brand = p.Brand,
                    ImageUrl = p.ImageUrl,
                    IsSelected = p.IsSelected,
                    Created_at = p.Created_at,
                    Updated_at = p.Updated_at,
                    Deleted_at = p.Deleted_at,
                })
                .ToList();

            return dtoList;
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid product_id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == product_id);
            if (product == null)
            {
                return null;
            }

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                SKU = product.SKU,
                Stock = product.Stock,
                Price = product.Price,
                PromotionalPrice = product.PromotionalPrice,
                Description = product.Description,
                Colection = product.Colection,
                Size = product.Size,
                StockSize = product.StockSize,
                Category = product.Category,
                Brand = product.Brand,
                ImageUrl = product.ImageUrl,
                IsSelected = product.IsSelected,
                Created_at = product.Created_at,
            };
        }

        public async Task<bool> UpdateProductSelectionAsync(Guid productId, bool isSelected)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return false;
            }

            product.IsSelected = isSelected;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SelectedProductsResponseDto> GetSelectedProductsWithTotalAsync(
            List<Guid>? productIds = null
        )
        {
            IQueryable<Product> query = _context.Products;
            if (productIds != null && productIds.Count != 0)
                query = query.Where(p => productIds.Contains(p.Id));
            else
                query = query.Where(p => p.IsSelected == true);

            var selectedProducts = await query.ToListAsync();

            if (selectedProducts == null || selectedProducts.Count == 0)
            {
                return new SelectedProductsResponseDto([], 0);
            }

            var totalPrice = selectedProducts.Sum(p => p.Price);

            var selectedProductDtos = selectedProducts
                .Select(p => new SelectedProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
                })
                .ToList();

            return new SelectedProductsResponseDto(selectedProductDtos, totalPrice);
        }
    }
}
