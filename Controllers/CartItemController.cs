using Ecommerce.DTOs.CartItem;
using Ecommerce.DTOs.User;
using Ecommerce.Extensions;
using Ecommerce.Models;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController: ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly IUserService _userService;
        public CartItemController(ICartItemService cartItemService,
            IUserService userService)
        {
            _cartItemService = cartItemService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateCartItemRequest request, 
            CancellationToken cancellationToken)
        {
           var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int num = int.Parse(UserId);
            var user = await _userService.GetByIdAsync(num);
            var result = await _cartItemService.AddAsync(user.Value.Id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllByCartIdAsync(CancellationToken cancellationToken)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int num = int.Parse(UserId);
            var user = await _userService.GetByIdAsync(num);
            var result = await _cartItemService.GetAllByCartIdAsync(user.Value.Id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
