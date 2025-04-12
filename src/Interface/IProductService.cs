using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using curvela_backend.src.Data;
using curvela_backend.src.DTOs;

namespace curvela_backend.src.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetProductsAsync(
            string? categoria,
            string? colecao,
            string? nome,
            int page,
            int pageSize
        );
        Task<ProductResponseDto?> GetProductByIdAsync(Guid product_id);
        Task<bool> UpdateProductSelectionAsync(Guid productId, bool isSelected);
Task<SelectedProductsResponseDto> GetSelectedProductsWithTotalAsync(List<Guid>? productIds = null);
    }
}
