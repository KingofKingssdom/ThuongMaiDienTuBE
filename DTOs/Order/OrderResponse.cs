using Ecommerce.Common.Enum;
using Ecommerce.Models;

namespace Ecommerce.DTOs.Order
{
    public class OrderResponse
    {
        public int Id { set; get; }
        public string OrderCode { set; get; } = string.Empty;
        public int UserId { set; get; }
        public OrderStatusType OrderStatus { set; get; }
        public double SubTotal { set; get; }
        public double ShippingFee { set; get; }
        public double DiscountAmount { set; get; }
        public double GrandTotal { set; get; }
        public PaymentMethodType PaymentMethod { set; get; }
        public PaymentStatusType PaymentStatus { set; get; }
        public string ReceiverName { set; get; } = string.Empty;
        public string ReceiverPhone { set; get; } = string.Empty;
        public string ReceiverAddress { set; get; } = string.Empty;
        public string Note { set; get; } = string.Empty;
    }
}
