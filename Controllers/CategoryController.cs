using Ecommerce.DTOs.Category;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync(
            int id, 
            CreateCategoryRequest request, 
            CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(id, request, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPut("delete")]
        public async Task<IActionResult> DeleteAsync(
            int id, 
            CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
