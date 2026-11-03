using Ecommerce.DTOs.ProductVariant;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariantController: ControllerBase
    {
        private readonly IProductVariantService _productVariantService;
        public ProductVariantController(IProductVariantService productVariantService)
        {
            _productVariantService = productVariantService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(
          [FromForm]  CreateProductVariantRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _productVariantService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _productVariantService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _productVariantService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateAsync(
          [FromRoute]  int id, 
            CreateProductVariantRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _productVariantService.UpdateAsync(id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("delete/{id}")]        
        public async Task<IActionResult> DeleteAsync(
           [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _productVariantService.DeleteAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductIdAsync([FromRoute] int productId,
            CancellationToken cancellationToken)
        {
            var result = await _productVariantService.GetByProductIdAsync(productId, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
