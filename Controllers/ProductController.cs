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
    }
}
