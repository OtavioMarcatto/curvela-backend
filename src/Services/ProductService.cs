using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curvela_backend.Data;
using curvela_backend.src.DTOs;
using curvela_backend.src.Models;
using Microsoft.EntityFrameworkCore;

namespace curvela_backend.src.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

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
                    Category = p.Category,
                    Brand = p.Brand,
                    ImageUrl = p.ImageUrl,
                    Created_at = p.Created_at,
                })
                .ToList();

            return dtoList;
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
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
                Category = product.Category,
                Brand = product.Brand,
                ImageUrl = product.ImageUrl,
                Created_at = product.Created_at,
            };
        }
    }
}
