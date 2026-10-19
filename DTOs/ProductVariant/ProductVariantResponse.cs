namespace Ecommerce.DTOs.ProductVariant
{
    public class ProductVariantResponse
    {
        public int Id { set; get; }
        public string Storage { set; get; } = string.Empty;
        public double OriginPrice { set; get; }
        public double CurrentPrice { set; get; }
        public int Stock { set; get; }
        public string UrlProductColor { set; get; } = string.Empty;
        public string ColorName { set; get; } = string.Empty;
        public int ProductId { set; get; }
        public string ProductName { set; get; } = string.Empty;
        public DateTime CreatedDay { set; get; }
        public DateTime DeletedDay { set; get; }

    }
}
