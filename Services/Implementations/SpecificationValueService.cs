using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationValue;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Common.Enum;
namespace Ecommerce.Services.Implementations
{
    public class SpecificationValueService: ISpecificationValueService
    {
        private readonly ISpecificationValueRepository _specificationValueRepository;
        public SpecificationValueService(ISpecificationValueRepository specificationValueRepository)
        {
            _specificationValueRepository = specificationValueRepository;
        }
       public async Task<Result<SpecificationValueResponse>> AddAsync(
            CreateSpecificationValueRequest request,
            CancellationToken cancellationToken)
        {
            var specificationValue = new SpecificationValue
            {
                Value = request.Value,
                ProductId = request.ProductId,
                SpecificationAttributeId = request.SpecificationAttributeId
            };
            await _specificationValueRepository.AddAsync(specificationValue, cancellationToken);
            return Result<SpecificationValueResponse>.Success(new SpecificationValueResponse
            {
                Id = specificationValue.Id,
                ProductId = specificationValue.ProductId,
                SpecificationAttributeId = specificationValue.SpecificationAttributeId,
                Value = specificationValue.Value
            });
        }
       public async Task<Result<SpecificationValueResponse>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var specificationSpecificationValueExisting = await _specificationValueRepository
                .GetByIdAsync(id, cancellationToken);
            if(specificationSpecificationValueExisting == null)
            {
                return Result<SpecificationValueResponse>.Failure("Not Found",ErrorType.NotFound);
            }
            return Result<SpecificationValueResponse>.Success(new SpecificationValueResponse
            {
                Id = specificationSpecificationValueExisting.Id,
                ProductId = specificationSpecificationValueExisting.ProductId,
                SpecificationAttributeId = specificationSpecificationValueExisting.SpecificationAttributeId,
                Value = specificationSpecificationValueExisting.Value
            });
        }
       public async Task<Result<List<SpecificationValueResponse>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var specificationSpecificationValueExisting = await _specificationValueRepository
                            .GetAllAsync(cancellationToken);
            if (specificationSpecificationValueExisting == null)
            {
                return Result<List<SpecificationValueResponse>>.Failure("Not Found", ErrorType.NotFound);
            }
            return Result<List<SpecificationValueResponse>>.Success(specificationSpecificationValueExisting
                .Select(s => new SpecificationValueResponse
            {
                Id = s.Id,
                ProductId = s.ProductId,
                SpecificationAttributeId = s.SpecificationAttributeId,
                Value = s.Value
            }).ToList());
        }
    }
}
