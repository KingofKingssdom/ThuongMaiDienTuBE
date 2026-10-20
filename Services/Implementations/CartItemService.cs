using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.DTOs.CartItem;
using Ecommerce.DTOs.ProductVariant;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
namespace Ecommerce.Services.Implementations
{
    public class CartItemService: ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ICartRepository _cartRepository;
        public CartItemService(ICartItemRepository cartItemRepository,
            IProductVariantRepository productVariantRepository,
            ICartRepository cartRepository
            )
        {
            _cartItemRepository = cartItemRepository;
            _productVariantRepository = productVariantRepository;
            _cartRepository = cartRepository;
        }
        public async Task<Result<CartItemResponse>> AddAsync(int userId ,CreateCartItemRequest request,
            CancellationToken cancellationToken)
        {
            var cartExisting = await _cartRepository.GetByUserId(userId, cancellationToken);
            if(cartExisting == null)
            {
                return Result<CartItemResponse>.Failure("Not Found", ErrorType.NotFound);
            }
            var productVariantExisting = await _productVariantRepository
                .GetByIdAsync(request.ProductVariantId);
            if(productVariantExisting == null)
            {
                return Result<CartItemResponse>.Failure("Not Found", ErrorType.NotFound);
            }
            CartItem cartItemReturn;

            var cartItemExisting = await _cartItemRepository
                .FindCartItemExisting(productVariantExisting.Id, cartExisting.Id,
                cancellationToken);
            if(cartItemExisting != null)
            {
                cartItemExisting.PriceAtTime = productVariantExisting.CurrentPrice;
                cartItemExisting.Quantity += request.Quantity;
                cartItemExisting.TotalPrice += request.Quantity * productVariantExisting.CurrentPrice;
                await _cartItemRepository.UpdateAsync(cartItemExisting,cancellationToken);
                cartItemReturn = cartItemExisting;
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cartExisting.Id,
                    ProductVariantId = productVariantExisting.Id,
                    PriceAtTime = productVariantExisting.CurrentPrice,
                    Quantity = request.Quantity,
                    TotalPrice = productVariantExisting.CurrentPrice * request.Quantity
                };
                await _cartItemRepository.AddAsync(cartItem, cancellationToken);
                cartItemReturn = cartItem;
            }
            return Result<CartItemResponse>.Success(new CartItemResponse { 
                Id = cartItemReturn.Id,
                PriceAtTime = cartItemReturn.PriceAtTime,
                Quantity = cartItemReturn.Quantity,
                TotalPrice = cartItemReturn.TotalPrice,
                ProductVariant = new ProductVariantResponse
                {
                    Id = productVariantExisting.Id,
                    ColorName = productVariantExisting.ColorName,
                    ProductName = productVariantExisting.Product.ProductName,
                    UrlProductColor = productVariantExisting.UrlProductColor,
                    Storage = productVariantExisting.Storage
                }
            });
        }
        // Lấy toàn các sản phẩm đã thêm vào giỏ hàng của user theo giỏ thàng tương ưng với user
        // - Kiểm tra giá của sản phẩm trong giỏ hàng với giá của sản phẩm bên ngoài 
        //  nếu khác nhau, phải cập nhập giá trong giỏ hàng lại cho đúng với giá sản phẩm
        // (vì giá trong giỏ hàng là tạm thời chưa thanh toán)
        // Sử dụng thêm cờ để báo hiệu là giá đã thay đổi kể từ lần cuối thêm
        // sản phẩm vào giỏ để tránh người dùng thắc mắc dữ liệu
       public async Task<Result<List<CartItemResponse>>> GetAllByCartIdAsync(int userId,
            CancellationToken cancellationToken)
        {
            var cartExisting = await _cartRepository.GetByUserId(userId, cancellationToken);
            if (cartExisting == null)
            {
                return Result<List<CartItemResponse>>.Failure("Not Found", ErrorType.NotFound);
            }
            var cartItems =  await _cartItemRepository.GetAllByCartIdAsync(cartExisting.Id, cancellationToken);
            bool hasPriceChanged = false;
            var priceChangedTracker = new Dictionary<int, bool>();
            foreach (var item in cartItems)
            {
                bool itemPriceChanged = false;
                double latesPrice = item.ProductVariant.CurrentPrice;
                if(item.PriceAtTime != latesPrice)
                {
                    item.PriceAtTime = latesPrice;
                    item.TotalPrice = latesPrice * item.Quantity;
                    hasPriceChanged = true;
                    itemPriceChanged = true;
                }
                priceChangedTracker[item.Id] = itemPriceChanged;
            }
            if (hasPriceChanged)
            {
                await _cartItemRepository.UpdatePriceAtTimeAsync(cancellationToken);
            }
            return Result<List<CartItemResponse>>.Success(cartItems.Select(c => new CartItemResponse
            {
                Id = c.Id,
                PriceAtTime = c.PriceAtTime,
                Quantity = c.Quantity,
                TotalPrice = c.TotalPrice,
                IsPriceChanged = priceChangedTracker.ContainsKey(c.Id) && priceChangedTracker[c.Id],
                ProductVariant = new ProductVariantResponse
                {
                    Id = c.ProductVariant.Id,
                    ColorName = c.ProductVariant.ColorName,
                    ProductName = c.ProductVariant.Product.ProductName,
                    UrlProductColor = c.ProductVariant.UrlProductColor,
                    Storage = c.ProductVariant.Storage
                }
            }).ToList());
        }
    }
}
