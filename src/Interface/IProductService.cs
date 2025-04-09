using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using curvela_backend.src.DTOs;

namespace curvela_backend.src.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
    }
}
