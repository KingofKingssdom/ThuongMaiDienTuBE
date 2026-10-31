using Ecommerce.Common.Results;
using Ecommerce.DTOs.ProductVariant;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils;
using Ecommerce.Common.Enum;
namespace Ecommerce.Services.Implementations
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ImageStorageUtil _imageStorageUtil;
        public ProductVariantService(
            IProductVariantRepository productVariantRepository,
            ImageStorageUtil imageStorageUtil)
        {
            _productVariantRepository = productVariantRepository;
            _imageStorageUtil = imageStorageUtil;
        }
        public async Task<Result<ProductVariantResponse>> AddAsync(
             CreateProductVariantRequest request,
             CancellationToken cancellationToken)
        {
            var productVariantImage = await _imageStorageUtil.UploadImage(request.UrlProductColor, "productVariant");
            var productVariant = new ProductVariant
            {
                Storage = request.Storage,
                ColorName = request.ColorName,
                CurrentPrice = request.CurrentPrice,
                OriginPrice = request.OriginPrice,
                Stock = request.Stock,
                UrlProductColor = productVariantImage,
                ProductId = request.ProductId,
            };
            await _productVariantRepository.AddAsync(productVariant, cancellationToken);
            return Result<ProductVariantResponse>.Success(new ProductVariantResponse
            {
                Id = productVariant.Id,
                ColorName = productVariant.ColorName,
                Storage = productVariant.Storage,
                OriginPrice = productVariant.OriginPrice,
                CurrentPrice = productVariant.CurrentPrice,
                Stock = productVariant.Stock,
                UrlProductColor = productVariant.UrlProductColor,
                CreatedDay = productVariant.CreatedDay,
                DeletedDay = productVariant.DeteledDay,
            }, "Data is created successfully");
        }
        public async Task<Result<ProductVariantResponse>> GetByIdAsync(
             int id, CancellationToken cancellationToken)
        {
            var productExisting = await _productVariantRepository.GetByIdAsync(id, cancellationToken);
            if (productExisting == null)
            {
                return Result<ProductVariantResponse>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<ProductVariantResponse>.Success(new ProductVariantResponse
            {
                Id = productExisting.Id,
                ColorName = productExisting.ColorName,
                Storage = productExisting.Storage,
                OriginPrice = productExisting.OriginPrice,
                CurrentPrice = productExisting.CurrentPrice,
                Stock = productExisting.Stock,
                UrlProductColor = productExisting.UrlProductColor,
                CreatedDay = productExisting.CreatedDay,
                DeletedDay = productExisting.DeteledDay,
                ProductId = productExisting.Product.Id,
                ProductName = productExisting.Product.ProductName
            }, "Data is retrieved successfully");
        }
        public async Task<Result<List<ProductVariantResponse>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var productVariantExisting = await _productVariantRepository.GetAllAsync(cancellationToken);
            if (productVariantExisting == null)
            {
                return Result<List<ProductVariantResponse>>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<List<ProductVariantResponse>>.Success(productVariantExisting
                .Select(p => new ProductVariantResponse
                {
                    Id = p.Id,
                    ColorName = p.ColorName,
                    Storage = p.Storage,
                    OriginPrice = p.OriginPrice,
                    CurrentPrice = p.CurrentPrice,
                    Stock = p.Stock,
                    UrlProductColor = p.UrlProductColor,
                    CreatedDay = p.CreatedDay,
                    DeletedDay = p.DeteledDay,
                    ProductId = p.Product.Id,
                    ProductName = p.Product.ProductName
                }).ToList(), "Data is retrieved successfully");
        }
        public async Task<Result<ProductVariantResponse>> UpdateAsync(
             int id,
             CreateProductVariantRequest request,
             CancellationToken cancellationToken)
        {
            var productExisting = await _productVariantRepository.GetByIdAsync(id, cancellationToken);
            if (productExisting == null)
            {
                return Result<ProductVariantResponse>.Failure("Not found", ErrorType.NotFound);
            }
            productExisting.ColorName = request.ColorName;
            productExisting.CurrentPrice = request.CurrentPrice;
            productExisting.OriginPrice = request.OriginPrice;
            productExisting.Stock = request.Stock;
            productExisting.Storage = request.Storage;
            if (request.UrlProductColor != null && request.UrlProductColor.Length > 0)
            {
                // Lưu lại đường dẫn ảnh cũ để chuẩn bị xóa
                var oldImagePath = productExisting.UrlProductColor;

                // Upload ảnh mới
                var newImageBrand = await _imageStorageUtil.UploadImage(request.UrlProductColor, "ProductVariant");

                // Gán ảnh mới cho entity
                productExisting.UrlProductColor = newImageBrand;

                // Xóa ảnh cũ trên ổ đĩa (nếu có tồn tại đường dẫn cũ)
                if (!string.IsNullOrEmpty(oldImagePath))
                {
                    _imageStorageUtil.DeleteImage(oldImagePath);
                }
            }
            await _productVariantRepository.UpdateAsync(cancellationToken);
            return Result<ProductVariantResponse>.Success(new ProductVariantResponse
            {
                Id = productExisting.Id,
                ColorName = productExisting.ColorName,
                Storage = productExisting.Storage,
                OriginPrice = productExisting.OriginPrice,
                CurrentPrice = productExisting.CurrentPrice,
                Stock = productExisting.Stock,
                UrlProductColor = productExisting.UrlProductColor,
                CreatedDay = productExisting.CreatedDay,
                DeletedDay = productExisting.DeteledDay,
                ProductId = productExisting.Product.Id,
                ProductName = productExisting.Product.ProductName
            }, "Data is updated successfully");
        }
        public async Task<Result<ProductVariantResponse>> DeleteAsync(
             int id,
             CancellationToken cancellationToken)
        {
            var productExisting = await _productVariantRepository.GetByIdAsync(id, cancellationToken);
            if (productExisting == null)
            {
                return Result<ProductVariantResponse>.Failure("Not found", ErrorType.NotFound);
            }
            productExisting.DeteledDay = DateTime.UtcNow;
            await _productVariantRepository.UpdateAsync(cancellationToken);
            return Result<ProductVariantResponse>.Success(new ProductVariantResponse
            {
                Id = productExisting.Id,
                ColorName = productExisting.ColorName,
                Storage = productExisting.Storage,
                OriginPrice = productExisting.OriginPrice,
                CurrentPrice = productExisting.CurrentPrice,
                Stock = productExisting.Stock,
                UrlProductColor = productExisting.UrlProductColor,
                CreatedDay = productExisting.CreatedDay,
                DeletedDay = productExisting.DeteledDay,
                ProductId = productExisting.Product.Id,
                ProductName = productExisting.Product.ProductName
            }, "Data is deleted successfully");
        }
        public async Task<Result<List<ProductVariantResponse>>> GetByProductIdAsync(
             int productId,
             CancellationToken cancellationToken)
        {
            var productVariantExisting = await _productVariantRepository.GetByProductId(productId, cancellationToken);
            if (productVariantExisting == null)
            {
                return Result<List<ProductVariantResponse>>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<List<ProductVariantResponse>>.Success(productVariantExisting
                .Select(p => new ProductVariantResponse
                {
                    Id = p.Id,
                    ColorName = p.ColorName,
                    Storage = p.Storage,
                    OriginPrice = p.OriginPrice,
                    CurrentPrice = p.CurrentPrice,
                    Stock = p.Stock,
                    UrlProductColor = p.UrlProductColor,
                    CreatedDay = p.CreatedDay,
                    DeletedDay = p.DeteledDay,
                    ProductId = p.Product.Id,
                    ProductName = p.Product.ProductName
                }).ToList(), "Data is retrieved successfully");
        }
    }
}
