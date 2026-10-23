using Ecommerce.Common.Results;
using Ecommerce.DTOs.Product;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils;
using Ecommerce.DTOs.Category;
using Ecommerce.DTOs.Brand;
using Ecommerce.Common.Enum;
namespace Ecommerce.Services.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ImageStorageUtil _imageStorageUtil;
        public ProductService(IProductRepository productRepository,
            ImageStorageUtil imageStorageUtil)
        {
            _productRepository = productRepository;
            _imageStorageUtil = imageStorageUtil;
        }
       public async Task<Result<ProductResponse>> AddAsync(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var imageProduct = await _imageStorageUtil.UploadImage(request.UrlImageProduct,"Products");
            var product = new Product
            {
                ProductName = request.ProductName,
                UrlImageProduct = imageProduct,
                Description = request.Description,
                IsFeatured = request.IsFeatured,
                IsOnPromotion = request.IsOnPromotion,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId
            };
            await _productRepository.AddAsync(product, cancellationToken);
            return Result<ProductResponse>.Success(new ProductResponse {
                Id = product.Id,
                ProductName = product.ProductName,
                UrlImageProduct = product.UrlImageProduct,
                Description = product.Description,
                IsFeatured = product.IsFeatured,
                IsOnPromotion = product.IsOnPromotion,
                Brand = product.Brand != null ? new BrandResponse
                {
                    Id = product.Brand.Id,
                    BrandName = product.Brand.BrandName,
                    UrlImageBrand = product.Brand.UrlImageBrand
                }: null,
                Category = product.Category != null ? new CategoryResponse
                {
                    Id = product.Category.Id,
                    CategoryName = product.Category.CategoryName
                }: null

            }, "Data is created successfull");
        }
       public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var productExisting = await _productRepository.GetByIdAsync(id, cancellationToken);
            if(productExisting == null)
            {
                return Result<ProductResponse>.Failure("Not Found", ErrorType.NotFound);
            }
            return Result<ProductResponse>.Success(new ProductResponse
            {
                Id = productExisting.Id,
                ProductName = productExisting.ProductName,
                UrlImageProduct = productExisting.UrlImageProduct,
                Description = productExisting.Description,
                IsFeatured = productExisting.IsFeatured,
                IsOnPromotion = productExisting.IsOnPromotion,
                Brand = productExisting.Brand != null ? new BrandResponse
                {
                    Id = productExisting.Brand.Id,
                    BrandName = productExisting.Brand.BrandName,
                    UrlImageBrand = productExisting.Brand.UrlImageBrand
                } : null,
                Category = productExisting.Category != null ? new CategoryResponse
                {
                    Id = productExisting.Category.Id,
                    CategoryName = productExisting.Category.CategoryName
                } : null

            }, "Data is retrieved successfully");
        }
       public async Task<Result<List<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var productExisting = await _productRepository.GetAllAsync(cancellationToken);
            if(productExisting == null)
            {
                return Result<List<ProductResponse>>.Failure("Not Found", ErrorType.None);
            }
            return Result<List<ProductResponse>>.Success(productExisting.Select(p => new ProductResponse
            {
                Id = p.Id,
                ProductName = p.ProductName,
                UrlImageProduct = p.UrlImageProduct,
                Description = p.Description,
                IsFeatured = p.IsFeatured,
                IsOnPromotion = p.IsOnPromotion,
                Brand = p.Brand.CategoryBrand != null ? new BrandResponse
                {
                    Id = p.Brand.Id,
                    BrandName = p.Brand.BrandName,
                    UrlImageBrand = p.Brand.UrlImageBrand
                } : null,
                Category = p.Category.CategoryBrand != null ? new CategoryResponse
                {
                    Id = p.Category.Id,
                    CategoryName = p.Category.CategoryName
                } : null
            }).ToList());
        }
        public async Task<Result<List<ProductResponse>>> GetAllByIsOnPromotion(CancellationToken cancellationToken)
        {
            var productExisting = await _productRepository.GetAllByOnPromotion(cancellationToken);
            if (productExisting == null)
            {
                return Result<List<ProductResponse>>.Failure("Not Found", ErrorType.None);
            }
            return Result<List<ProductResponse>>.Success(productExisting.Select(p => new ProductResponse
            {
                Id = p.Id,
                ProductName = p.ProductName,
                UrlImageProduct = p.UrlImageProduct,
                Description = p.Description,
                IsFeatured = p.IsFeatured,
                IsOnPromotion = p.IsOnPromotion,
                
            }).ToList());
        }
    }

}
