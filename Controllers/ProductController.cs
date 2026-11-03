using Ecommerce.DTOs.Product;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] CreateProductRequest request,CancellationToken cancellationToken)
        {
            var result = await _productService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _productService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("promotion")]
        public async Task<IActionResult> GetAllByIsOnPromotion(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByIsOnPromotion(cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("isfeature")]
        public async Task<IActionResult> GetAllIsFeature([FromQuery] int categoryId, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllIsFeatured(categoryId, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("search")]
        public async Task<IActionResult> GetAllByName([FromQuery] string productName, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByNameAsync(productName, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("filter")]
        public async Task<IActionResult> GetAllByCategoryAndBrand(
            [FromQuery] int categoryId,
            [FromQuery] int? brandId,
            CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllByCategoryIdAndBrandId(categoryId, brandId, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
          [FromForm] UpdateProductRequest request, 
            CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
