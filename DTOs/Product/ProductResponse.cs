using Ecommerce.DTOs.Brand;
using Ecommerce.DTOs.Category;
using Ecommerce.DTOs.ProductVariant;

namespace Ecommerce.DTOs.Product
{
    public class ProductResponse
    {
        public int Id { set; get; }
        public required string ProductName { set; get; }
        public string? Description { set; get; }
        public required string UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public CategoryResponse? Category { set; get; }
        public BrandResponse? Brand { set; get; }
        public List<ProductVariantResponse> ProductVariants { set; get; } 
            = new List<ProductVariantResponse>();
    }
}
