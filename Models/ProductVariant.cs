using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class ProductVariant: EntityBase
    {
        public string Storage { set; get; } = string.Empty;
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public int Stock { set; get; }
        public string UrlProductColor { set; get; } = string.Empty;
        public string ColorName { set; get; } = string.Empty;
        public int ProductId { set; get; }
        public Product Product { set; get; } = null!;
    }
}
