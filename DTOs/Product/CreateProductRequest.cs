namespace Ecommerce.DTOs.Product
{
    public class CreateProductRequest
    {
        public required string ProductName { set; get; }
        public string? Description { set; get; }
        public required IFormFile UrlImageProduct { set; get; }
        public bool IsFeatured { set; get; }
        public bool IsOnPromotion { set; get; }
        public int BrandId { set; get; }
        public int CategoryId { set; get; }
    }
}
