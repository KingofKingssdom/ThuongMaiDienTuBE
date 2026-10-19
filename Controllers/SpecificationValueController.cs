using Ecommerce.DTOs.SpecificationValue;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecificationValueController:ControllerBase
    {
        private readonly ISpecificationValueService _specificationValueService;
        public SpecificationValueController(ISpecificationValueService specificationValueService)
        {
            _specificationValueService = specificationValueService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateSpecificationValueRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _specificationValueService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _specificationValueService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _specificationValueService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
