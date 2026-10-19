namespace Ecommerce.DTOs.ProductVariant
{
    public class CreateProductVariantRequest
    {
        public string Storage { set; get; } = string.Empty;
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public int Stock { set; get; }
        public IFormFile? UrlProductColor { set; get; }
        public string ColorName { set; get; } = string.Empty;
        public int ProductId { set; get; }
    }
}
