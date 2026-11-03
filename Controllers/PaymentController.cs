
using Ecommerce.Config;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private VNPayConfig _vnpayConfig;

        public PaymentController(
            IPaymentService paymentService,
            VNPayConfig vnpayConfig)
        {
            _paymentService = paymentService;
            _vnpayConfig = vnpayConfig;
        }

        [HttpGet("checkout/vnpay/{orderId}")]
        public async Task<IActionResult> Checkout(int orderId)
        {
            try
            {
                // Đồng bộ tên hàm đúng với IPaymentService (CreateVNPayUrlAsync)
                var url = await _paymentService.CreateVNPayUrlAsync(orderId, HttpContext);
                return Ok(new { paymentUrl = url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("result-vnpay")]
        public async Task<IActionResult> Result(CancellationToken cancellationToken)
        {
            try
            {
                var queryParams = HttpContext.Request.Query;

                // Gọi Service xử lý logic cập nhật DB và check kết quả
                var (isSuccess, orderId) = await _paymentService.ProcessVNPayCallbackAsync(queryParams, cancellationToken);

                // Chuyển hướng người dùng về trang Frontend tương ứng
                string returnUrl = _vnpayConfig.vnp_Returnurl;
                var queryString = HttpContext.Request.QueryString.Value;
                var frontendRedirectUrl = $"{returnUrl}{queryString}&orderId={orderId}";
                return Redirect(frontendRedirectUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
