using Ecommerce.DTOs.Order;
using Ecommerce.Extensions;
using Ecommerce.Services.Implementations;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController :ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController (IOrderService orderSerivce,
            IUserService userService)
        {
            _orderService = orderSerivce;
            _userService = userService;
        }
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int num = int.Parse(UserId);
            var user = await _userService.GetByIdAsync(num);
            var result = await _orderService.AddAsync(user.Value.Id, request, cancellationToken);
            return this.ToActionResult(result);
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetAllByUserIdAsync(CancellationToken cancellationToken)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int num = int.Parse(UserId);
            var user = await _userService.GetByIdAsync(num);
            var result = await _orderService.GetAllByUserIdAsync(user.Value.Id, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}
