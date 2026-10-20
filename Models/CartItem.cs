using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class CartItem: EntityBase
    {
        public int CartId { set; get; }
        public Cart Cart { set; get; } = null!;
        public int ProductVariantId { set; get; }
        public ProductVariant ProductVariant { set; get; } = null!;
        public int Quantity { set; get; }
        public double PriceAtTime { set; get; }
        public double TotalPrice { set; get; }

    }
}
