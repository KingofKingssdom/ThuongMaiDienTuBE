using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class PaymentService: IPaymentService
    {
        private readonly VNPayService _vnpayService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(VNPayService vnpayService, IPaymentRepository paymentRepository)
        {
            _vnpayService = vnpayService;
            _paymentRepository = paymentRepository;
        }

        // 1. Nghiệp vụ tạo URL thanh toán VNPay
        public async Task<string> CreateVNPayUrlAsync(int orderId, HttpContext httpContext)
        {
            // Gọi VNPayService để sinh chuỗi URL chuyển hướng
            return await _vnpayService.CreatePaymentUrl(orderId, httpContext);
        }

        // 2. Nghiệp vụ xử lý kết quả trả về từ VNPay (Callback/ReturnUrl)
        public async Task<(bool isSuccess, int orderId)> ProcessVNPayCallbackAsync(IQueryCollection queryParams, CancellationToken cancellationToken)
        {
            // Gọi VNPayService để phân tích chữ ký và mã phản hồi từ VNPay
            bool isValid = _vnpayService.ProcessPaymentResponse(queryParams, out int orderId, out bool isSuccess);

            if (!isValid || orderId <= 0)
            {
                return (false, 0);
            }

            // Gọi Repository để cập nhật trạng thái đơn hàng trong Database
            // Nếu thành công (isSuccess = true) -> PaymentStatus = 1 (Đã thanh toán)
            // Nếu thất bại (isSuccess = false) -> PaymentStatus = 0 (Chưa thanh toán)
            int newPaymentStatus = isSuccess ? 1 : 0;
            await _paymentRepository.UpdateOrderPaymentStatusAsync(orderId, newPaymentStatus, cancellationToken);

            return (isSuccess, orderId);
        }
    }
}
