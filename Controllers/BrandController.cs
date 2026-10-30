using Ecommerce.DTOs.Brand;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] CreateBrandRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _brandService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _brandService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _brandService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet("category")]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId, 
            CancellationToken cancellationToken)
        {
            var result = await _brandService.GetAllByCategoryId(categoryId, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpPatch ("update")]
        public async Task<IActionResult> UpdateAsync(
            int id, 
            CreateBrandRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _brandService.UpdateAsync(id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _brandService.DeleteAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
