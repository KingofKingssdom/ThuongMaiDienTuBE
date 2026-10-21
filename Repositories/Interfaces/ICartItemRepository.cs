using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddAsync(CartItem cartItem, 
            CancellationToken cancellationToken = default);
        Task<CartItem?> FindCartItemExisting(
            int productVariantId, int cartId,
            CancellationToken cancellationToken = default);
        Task<CartItem> UpdateAsync(CartItem cartItem,
            CancellationToken cancellationToken = default);
        Task<List<CartItem>> GetAllByCartIdAsync(int cartId, 
            CancellationToken cancellationToken);
        Task UpdatePriceAtTimeAsync (CancellationToken cancellationToken);
        Task<List<CartItem>> GetAllCartItemByIdsAsync(
            List<int> cartItemIds, 
            int cartId,
            CancellationToken cancellationToken = default);
        Task DeleteCartItemByIdsAsync(
            List<int> cartItemIds,
            CancellationToken cancellationToken = default);
    }
}
