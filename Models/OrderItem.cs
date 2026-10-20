using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class OrderItem: EntityBase
    {
        public int OrderId { set; get; }
        public Order Order { set; get; } = null!;
        public int ProductVariantId { set; get; }
        public ProductVariant ProductVariant { set; get; } = null!;
        public int Quantity { set; get; }
        public double PriceAtTime { set; get; }
        public double TotalPrice { set; get; }
    }
}
