using Ecommerce.DTOs.SpecificationGroup;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecificationGroupController:ControllerBase
    {
        private readonly ISpecificationGroupService _specificationGroupService;
        public SpecificationGroupController(ISpecificationGroupService specificationGroupService)
        {
            _specificationGroupService = specificationGroupService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateSpecificationGroupRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _specificationGroupService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _specificationGroupService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _specificationGroupService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
