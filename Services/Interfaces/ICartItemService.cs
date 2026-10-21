using Ecommerce.Common.Results;
using Ecommerce.DTOs.CartItem;

namespace Ecommerce.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<Result<CartItemResponse>> AddAsync(int userId, CreateCartItemRequest request,
            CancellationToken cancellationToken);
        Task<Result<List<CartItemResponse>>> GetAllByCartIdAsync(int userId,
            CancellationToken cancellationToken);
        
    }
}
