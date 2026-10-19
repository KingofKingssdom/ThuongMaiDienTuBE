using Ecommerce.DTOs.SpecificationAttribute;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecificationAttributeController: ControllerBase
    {
        private readonly ISpecificationAttributeService _specificationAttributeService;
        public SpecificationAttributeController(ISpecificationAttributeService specificationAttributeService)
        {
            _specificationAttributeService = specificationAttributeService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateSpecificationAttributeRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _specificationAttributeService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _specificationAttributeService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _specificationAttributeService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
