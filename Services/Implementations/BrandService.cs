using Ecommerce.Common.Results;
using Ecommerce.DTOs.Brand;
using Ecommerce.DTOs.Category;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils;

namespace Ecommerce.Services.Implementations
{
    public class BrandService: IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ImageStorageUtil _imageStorageUtil;
        private readonly ICategoryRepository _categoryRepository;
        public BrandService(IBrandRepository brandRepository,
            ImageStorageUtil imageStorageUtil,
            ICategoryRepository categoryRepository)
        {
            _brandRepository = brandRepository;
            _imageStorageUtil = imageStorageUtil;
            _categoryRepository = categoryRepository;
        }
       public async Task<Result<BrandResponse>> AddAsync(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var imageBrand = await _imageStorageUtil.UploadImage(request.UrlImageBrand, "Brands");
            
            var brand = new Brand
            {
                BrandName = request.BrandName,
                UrlImageBrand = imageBrand,
               CategoryBrand = request.CategoryIds.Select(id => new CategoryBrand
                {
                    CategoryId = id
                }).ToList()
            };
            await _brandRepository.AddAsync(brand, cancellationToken);
            return Result<BrandResponse>.Success(new BrandResponse
            {
                Id = brand.Id,
                BrandName = brand.BrandName,
                UrlImageBrand = brand.UrlImageBrand,
            }, "Data is created successfully");
            
        }
       public async Task<Result<BrandResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var brandExisting = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if(brandExisting == null)
            {
                return Result<BrandResponse>.Failure("Not found", Common.Enum.ErrorType.NotFound);
            }
            return Result<BrandResponse>.Success(new BrandResponse {
                Id = brandExisting.Id,
            BrandName = brandExisting.BrandName,
                UrlImageBrand = brandExisting.UrlImageBrand,
                Category = brandExisting.CategoryBrand.Select(c => new CategoryResponse
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            }, "Data retrieved successfully");
        }

       public async Task<Result<List<BrandResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var brandExisting = await _brandRepository.GetAllAsync(cancellationToken);
            return Result<List<BrandResponse>>.Success(brandExisting.Select(b => new BrandResponse
            {
                Id = b.Id,
                BrandName = b.BrandName,
                UrlImageBrand = b.UrlImageBrand,
                Category = b.CategoryBrand.Select(c => new CategoryResponse
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            }).ToList());
        }
       public async Task<Result<List<BrandResponse>>> GetAllByCategoryId(
            int categoryId,
            CancellationToken cancellationToken)
        {
            var brandExisting = await _brandRepository.GetByCagtegoryIdAsync(categoryId,cancellationToken);
            return Result<List<BrandResponse>>.Success(brandExisting.Select(b => new BrandResponse
            {
                Id = b.Id,
                BrandName = b.BrandName,
                UrlImageBrand = b.UrlImageBrand,
                Category = b.CategoryBrand.Select(c => new CategoryResponse
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            }).ToList());
        }
       public async Task<Result<BrandResponse>> UpdateAsync(int id, CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var brandExisting = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if (brandExisting == null)
            {
                return Result<BrandResponse>.Failure("Not found", Common.Enum.ErrorType.NotFound);
            }
            brandExisting.BrandName = request.BrandName;
            if (request.UrlImageBrand != null && request.UrlImageBrand.Length > 0)
            {
                // Lưu lại đường dẫn ảnh cũ để chuẩn bị xóa
                var oldImagePath = brandExisting.UrlImageBrand;

                // Upload ảnh mới
                var newImageBrand = await _imageStorageUtil.UploadImage(request.UrlImageBrand, "Brands");

                // Gán ảnh mới cho entity
                brandExisting.UrlImageBrand = newImageBrand;

                // Xóa ảnh cũ trên ổ đĩa (nếu có tồn tại đường dẫn cũ)
                if (!string.IsNullOrEmpty(oldImagePath))
                {
                    _imageStorageUtil.DeleteImage(oldImagePath);
                }
            }

            await _brandRepository.UpdateAsync(cancellationToken);
            return Result<BrandResponse>.Success(new BrandResponse
            {
                Id = brandExisting.Id,
                BrandName = brandExisting.BrandName,
                UrlImageBrand = brandExisting.UrlImageBrand,
                Category = brandExisting.CategoryBrand.Select(c => new CategoryResponse
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            }, "Data updated successfully");

        }
       public async Task<Result<BrandResponse>> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var brandExisting = await _brandRepository.GetByIdAsync(id, cancellationToken);
            if (brandExisting == null)
            {
                return Result<BrandResponse>.Failure("Not found", Common.Enum.ErrorType.NotFound);
            }
            brandExisting.DeteledDay = DateTime.UtcNow;
            await _brandRepository.UpdateAsync(cancellationToken);
            return Result<BrandResponse>.Success(new BrandResponse
            {
                Id = brandExisting.Id,
                BrandName = brandExisting.BrandName,
                UrlImageBrand = brandExisting.UrlImageBrand,
                Category = brandExisting.CategoryBrand.Select(c => new CategoryResponse
                {
                    Id = c.Category.Id,
                    CategoryName = c.Category.CategoryName
                }).ToList()
            }, "Data deleted successfully");
        }
    }
}
