using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.DTOs.Order;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Ecommerce.Services.Implementations
{
    public class OrderService: IOrderService
    {
        private static readonly Random _random = new Random();
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductVariantRepository productVariantRepository,
            IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productVariantRepository = productVariantRepository;
            _orderItemRepository = orderItemRepository;
        }
        private static string GenerateOrderCode()
        {
            string date = DateTime.Now.ToString("yyMMdd");
            int random = _random.Next(1000, 10000);
            return $"ORD-{date}-{random}";
        }

        /*
         Luồng xử tạo đơn hàng qua các  bước 
        Lấy giỏ hàng của User.

        Tạo đối tượng Order và danh sách OrderItem (map dữ liệu từ CartItem).

        Trừ số lượng tồn kho của ProductVariant.

        Xóa các sản phẩm đã mua khỏi giỏ hàng (CartItem).

        Gói tất cả trong một khối Transaction (using var transaction = await _context.Database.BeginTransactionAsync()) để đảm bảo tính ACID.
         */
        public async Task<Result<OrderResponse>> AddAsync(
           int userId,
           CreateOrderRequest request, 
           CancellationToken cancellationToken)
        {
            
            var cartExisting = await _cartRepository.GetByUserId(
                userId, 
                cancellationToken);
            
            if(cartExisting == null)
            {
                return Result<OrderResponse>.Failure("Cart not found", ErrorType.NotFound);
            }

            var cartItemExisting = await _cartItemRepository.GetAllCartItemByIdsAsync(
                request.SelectedCartItemIds, cartExisting.Id, cancellationToken);
            if(cartItemExisting == null)
            {
                return Result<OrderResponse>.Failure("No items selected for checkout", ErrorType.NotFound);
            }
            foreach (var item in cartItemExisting)
            {
                var variant = item.ProductVariant;
                if (variant == null)
                {
                    return Result<OrderResponse>.Failure($"Product variant not found.", ErrorType.NotFound);
                }
                if (item.Quantity > variant.Stock)
                {
                    return Result<OrderResponse>.Failure(
                        $"Sản phẩm '{variant.Product?.ProductName ?? "Sản phẩm"} ({variant.ColorName} - {variant.Storage})' chỉ còn lại {variant.Stock} sản phẩm trong kho.",
                        ErrorType.Validation);
                }
                variant.Stock -= item.Quantity;
            }
            double calculatedSubTotal = cartItemExisting.Sum(ci => ci.PriceAtTime * ci.Quantity);
            var order = new Order
            {
                UserId = userId,
                OrderCode = GenerateOrderCode(),
                SubTotal = calculatedSubTotal,
                DiscountAmount = request.DiscountAmount,
                ShippingFee = request.ShippingFee,
                GrandTotal = calculatedSubTotal - request.DiscountAmount + request.ShippingFee,
                OrderStatus = OrderStatusType.Pending,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = request.PaymentStatus,
                ReceiverName = request.ReceiverName,
                ReceiverPhone = request.ReceiverPhone,
                ReceiverAddress = request.ReceiverAddress,
                Note = request.Note,
                OrderItems = cartItemExisting.Select(c => new OrderItem
                {
                    ProductVariantId = c.ProductVariantId,
                    Quantity = c.Quantity,
                    PriceAtTime = c.PriceAtTime,
                    TotalPrice = c.PriceAtTime * c.Quantity
                }).ToList()
            };
            var cartItemIds = cartItemExisting.Select(ci => ci.Id).ToList();
            await _cartItemRepository.DeleteCartItemByIdsAsync(cartItemIds, cancellationToken);
            await _orderRepository.AddAsync(order, cancellationToken);

            return Result<OrderResponse>.Success(new OrderResponse
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                SubTotal = order.SubTotal,
                DiscountAmount = order.DiscountAmount,
                ShippingFee = order.ShippingFee,
                GrandTotal = order.GrandTotal,
                OrderStatus = order.OrderStatus,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                ReceiverName = order.ReceiverName,
                ReceiverPhone = order.ReceiverPhone,
                ReceiverAddress = order.ReceiverAddress,
                Note = order.Note,

            });
        }
    }
}
