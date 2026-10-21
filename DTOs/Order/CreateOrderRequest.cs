using Ecommerce.Common.Enum;
using Ecommerce.Models;

namespace Ecommerce.DTOs.Order
{
    public class CreateOrderRequest
    {
        public double ShippingFee { set; get; }
        public double DiscountAmount { set; get; }
        public PaymentMethodType PaymentMethod { set; get; }
        public PaymentStatusType PaymentStatus { set; get; }
        public string ReceiverName { set; get; } = string.Empty;
        public string ReceiverPhone { set; get; } = string.Empty;
        public string ReceiverAddress { set; get; } = string.Empty;
        public string Note { set; get; } = string.Empty;
        public List<int> SelectedCartItemIds { set; get; } = new List<int>();
    }
}
