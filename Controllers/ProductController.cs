using Ecommerce.DTOs.Product;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] CreateProductRequest request,CancellationToken cancellationToken)
        {
            var result = await _productService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _productService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("promotion")]
        public async Task<IActionResult> GetAllByIsOnPromotion(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByIsOnPromotion(cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("isfeature")]
        public async Task<IActionResult> GetAllIsFeature([FromQuery] int categoryId, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllIsFeatured(categoryId, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllByName([FromQuery] string productName, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByNameAsync(productName, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAllByCategoryAndBrand(
            [FromQuery] int categoryId,
            [FromQuery] int? brandId,
            CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByCategoryIdAndBrandId(categoryId, brandId, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(int id,
           UpdateProductRequest request, 
            CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
