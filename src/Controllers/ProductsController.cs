using System.Text.Json;
using curvela_backend.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace curvela_backend.src.Controllers
{
    [ApiController]
    [Route("curvela/[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private const string TelefoneLoja = "5516993384169";
        private readonly IProductService _productService = productService;

        // GET: api/products
        [HttpGet("products-list")]
        public async Task<IActionResult> GetProdutos(
            [FromQuery] string? categoria = null,
            [FromQuery] string? colecao = null,
            [FromQuery] string? nome = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var produtos = await _productService.GetProductsAsync(
                categoria,
                colecao,
                nome,
                page,
                pageSize
            );
            return Ok(produtos);
        }

        // GET: api/products/{id}
        [HttpGet("product/{product_id}")]
        public async Task<IActionResult> GetProduct(Guid product_id)
        {
            var product = await _productService.GetProductByIdAsync(product_id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPatch("{productId}/selected")]
        public async Task<IActionResult> UpdateSelectedStatus(
            Guid productId,
            [FromBody] bool isSelected
        )
        {
            var updated = await _productService.UpdateProductSelectionAsync(productId, isSelected);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("selected-to-buy")]
        public async Task<IActionResult> GetSelectedProductsWithTotal()
        {
            var responseDto = await _productService.GetSelectedProductsWithTotalAsync();
            return Ok(responseDto);
        }

        // GET: api/whatsapp/link
        [HttpGet("link")]
        public async Task<IActionResult> GenerateWhatsAppLink()
        {
            if (!Request.Cookies.TryGetValue("selectedProducts", out var selectedProductsJson))
                return BadRequest("Nenhum produto selecionado.");

            List<Guid> selectedProductIds;
            try
            {
                selectedProductIds =
                    JsonSerializer.Deserialize<List<Guid>>(selectedProductsJson) ?? [];
            }
            catch (Exception)
            {
                return BadRequest("Cookie de produtos selecionados está em formato inválido.");
            }

            var productsResponse = await _productService.GetSelectedProductsWithTotalAsync(
                selectedProductIds
            );

            if (productsResponse.Products == null || !productsResponse.Products.Any())
                return NotFound("Nenhum produto encontrado para os IDs informados.");

            string mensagem = "Olá, estou interessado nos seguintes produtos:\n";
            decimal total = 0;
            foreach (var product in productsResponse.Products)
            {
                mensagem +=
                    $"- {product.Name} (ID: {product.Id}) - {product.Price:C} - Tamanho: {product.Size}\n";
                total += product.Price;
            }
            mensagem += $"Total: {total:C}";

            string baseWhatsAppUrl = "https://api.whatsapp.com/send";
            string encodedMensagem = Uri.EscapeDataString(mensagem);
            string whatsAppLink = $"{baseWhatsAppUrl}?phone={TelefoneLoja}&text={encodedMensagem}";

            return Ok(new { whatsAppLink });
        }

        [HttpPost("select")]
        public IActionResult SelectProduct([FromBody] Guid productId)
        {
            List<Guid> selectedProductIds = [];
            if (Request.Cookies.TryGetValue("selectedProducts", out var cookieValue))
            {
                try
                {
                    selectedProductIds = JsonSerializer.Deserialize<List<Guid>>(cookieValue) ?? [];
                }
                catch
                {
                    throw new ArgumentException("Error to select products");
                }
            }

            if (!selectedProductIds.Contains(productId))
                selectedProductIds.Add(productId);

            var json = JsonSerializer.Serialize(selectedProductIds);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddHours(1),
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
            };

            Response.Cookies.Append("selectedProducts", json, cookieOptions);

            return Ok(new { message = "Produto selecionado com sucesso!" });
        }
    }
}
