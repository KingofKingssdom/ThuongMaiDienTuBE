using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class Product: EntityBase
    {
        public required string ProductName { set; get; }
        public string? Description { set; get; }
        public required string UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public int BrandId { set; get; }
        public Brand Brand { set; get; } = null!;
        public int CategoryId { set; get; }
        public Category Category { set; get; } = null!;
    }
}
