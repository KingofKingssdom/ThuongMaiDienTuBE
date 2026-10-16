using Ecommerce.DTOs.Role;
using Ecommerce.Extensions;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(
            CreateRoleRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _roleService.AddAsync(request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken
            )
        {
            var result = await _roleService.GetAllAsync(cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _roleService.GetByIdAsync(id, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetByNameAsync(
             string name,
             CancellationToken cancellationToken)
        {
            var result = await _roleService.GetByNameAsync(name, cancellationToken);
            return this.ToActionResult(result);
        }
        //[Authorize(Roles = "Admin")]
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateAsync(
        //    int id,
        //    UpdateRoleRequest request,
        //     CancellationToken cancellationToken)
        //{
        //    var result = await _roleService.UpdateAsync(id, request, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}
