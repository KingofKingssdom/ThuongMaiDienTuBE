using Ecommerce.Config;
using Ecommerce.Data;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using System.Net;
using System.Text;

namespace Ecommerce.Services.Implementations
{
    public class VNPayService
    {
       
            private readonly VNPayConfig _vnpayConfig;
            private readonly IPaymentRepository _paymentRepository; 

            public VNPayService(VNPayConfig VNPayConfig, IPaymentRepository paymentRepository)
            {
                _vnpayConfig = VNPayConfig;
                _paymentRepository = paymentRepository;
            }

            public async Task<string> CreatePaymentUrl(int orderId, HttpContext httpContext)
            {
                var order = await _paymentRepository.GetOrderByIdAsync(orderId, default);
                if (order == null) throw new Exception("Đơn hàng không tồn tại.");

                var parts = order.OrderCode.Split('-');
                string cleanOrderInfo = parts.Length >= 2 ? (parts[0] + parts[1]) : order.OrderCode;

                string vnp_TxnRef = order.Id.ToString();
                string vnp_IpAddr = httpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                var vnp_Params = new Dictionary<string, string>();
                DateTime vietNamTime = DateTime.UtcNow.AddHours(7);
                vnp_Params.Add("vnp_Version", "2.1.0");
                vnp_Params.Add("vnp_Command", "pay");
                vnp_Params.Add("vnp_TmnCode", _vnpayConfig.vnp_TmnCode);
                vnp_Params.Add("vnp_Amount", (order.SubTotal * 100).ToString()); // Số tiền * 100
                vnp_Params.Add("vnp_CurrCode", "VND");
                vnp_Params.Add("vnp_TxnRef", vnp_TxnRef);
                vnp_Params.Add("vnp_OrderInfo", "Thanh toan don hang: " + cleanOrderInfo);
                vnp_Params.Add("vnp_OrderType", "other");
                vnp_Params.Add("vnp_Locale", "vn");
                vnp_Params.Add("vnp_ReturnUrl", _vnpayConfig.vnp_Returnurl);
                vnp_Params.Add("vnp_IpAddr", vnp_IpAddr);
                vnp_Params.Add("vnp_CreateDate", vietNamTime.ToString("yyyyMMddHHmmss"));

                var fieldNames = vnp_Params.Keys.ToList();
                fieldNames.Sort();

                var hashData = new StringBuilder();
                var query = new StringBuilder();

                foreach (var fieldName in fieldNames)
                {
                    string fieldValue = vnp_Params[fieldName];
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        hashData.Append(fieldName).Append("=").Append(WebUtility.UrlEncode(fieldValue));
                        query.Append(WebUtility.UrlEncode(fieldName)).Append("=").Append(WebUtility.UrlEncode(fieldValue));

                        if (fieldName != fieldNames.Last())
                        {
                            hashData.Append("&");
                            query.Append("&");
                        }
                    }
                }

                string vnp_SecureHash = VNPayConfig.HmacSHA512(_vnpayConfig.vnp_HashSecret, hashData.ToString());
                string paymentUrl = $"{_vnpayConfig.vnp_PayUrl}?{query}&vnp_SecureHash={vnp_SecureHash}";

                return paymentUrl;
            }

            public bool ProcessPaymentResponse(IQueryCollection queryParams, out int orderId, out bool isSuccess)
            {
                orderId = 0;
                isSuccess = false;

                var responseCode = queryParams["vnp_ResponseCode"];
                var txnRef = queryParams["vnp_TxnRef"];

                if (int.TryParse(txnRef, out orderId))
                {
                    if (responseCode == "00")
                    {
                        isSuccess = true;
                        return true;
                    }
                    isSuccess = false;
                    return true;
                }
                return false;
            }
        }
}
