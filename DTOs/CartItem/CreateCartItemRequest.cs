using Ecommerce.Models;

namespace Ecommerce.DTOs.CartItem
{
    public class CreateCartItemRequest
    {
        public int ProductVariantId { set; get; }
        public int Quantity { set; get; }
    }
}
