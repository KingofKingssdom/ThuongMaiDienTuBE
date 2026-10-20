using Ecommerce.DTOs.ProductVariant;
using Ecommerce.Models;

namespace Ecommerce.DTOs.CartItem
{
    public class CartItemResponse
    {
        public int Id { set; get; }
        public ProductVariantResponse? ProductVariant { set; get; } 
        public int Quantity { set; get; }
        public double PriceAtTime { set; get; }
        public double TotalPrice { set; get; }
        public bool IsPriceChanged { set; get; }
    }
}
