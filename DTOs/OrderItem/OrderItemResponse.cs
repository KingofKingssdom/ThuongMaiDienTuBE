namespace Ecommerce.DTOs.OrderItem
{
    public class OrderItemResponse
    {
        public int OrderId { set; get; }
        public int ProductVariantId { set; get; }
        public string ProductName { set; get; } = string.Empty;
        public string Storage { set; get; } = string.Empty;
        public string ColorName { set; get; } = string.Empty;
        public string UrlProductColor { set; get; } = string.Empty;
    public int Quantity {set; get; }
    public double PriceAtTime{ set; get; }
    public double TotalPrice{ set; get; }
        }
}
