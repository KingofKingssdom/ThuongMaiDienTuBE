using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationAttribute;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Common.Enum;
namespace Ecommerce.Services.Implementations
{
    public class SpecificationAttributeService: ISpecificationAttributeService
    {
        private readonly ISpecificationAttributeRepository _specificationAttributeRepository;
        public SpecificationAttributeService(ISpecificationAttributeRepository specificationAttributeRepository)
        {
            _specificationAttributeRepository = specificationAttributeRepository;
        }
       public async Task<Result<SpecificationAttributeResponse>> AddAsync(
            CreateSpecificationAttributeRequest request,
            CancellationToken cancellationToken)
        {
            var specificationAttribute = new SpecificationAttribute
            {
                LabelSpecification = request.LabelSpecification,
                SpecificationGroupId = request.SpecificationGroupId
            };
            await _specificationAttributeRepository.AddAsync(specificationAttribute, cancellationToken);
            return Result<SpecificationAttributeResponse>.Success(new SpecificationAttributeResponse
            {
                Id = specificationAttribute.Id,
                LabelSpecification = specificationAttribute.LabelSpecification,
                SpecificationGroupId = specificationAttribute.SpecificationGroupId
            });
        }
       public async Task<Result<SpecificationAttributeResponse>> GetByIdAsync(
            int id, CancellationToken cancellationToken)
        {
            var specificationAttributeExisting = await _specificationAttributeRepository
                .GetByIdAsync(id, cancellationToken);
            if(specificationAttributeExisting == null)
            {
                return Result<SpecificationAttributeResponse>.Failure("Not found",ErrorType.NotFound);
            }
            return Result<SpecificationAttributeResponse>.Success(new SpecificationAttributeResponse
            {
                Id = specificationAttributeExisting.Id,
                LabelSpecification = specificationAttributeExisting.LabelSpecification,
                SpecificationGroupId = specificationAttributeExisting.SpecificationGroupId
            });
        }
       public async Task<Result<List<SpecificationAttributeResponse>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var specificationAttributeExisting = await _specificationAttributeRepository
                .GetAllAsync(cancellationToken);
            if(specificationAttributeExisting == null)
            {
                return Result<List<SpecificationAttributeResponse>>.Failure("Not found", ErrorType.NotFound);
            }
            return Result<List<SpecificationAttributeResponse>>.Success(
                specificationAttributeExisting.Select(s => new SpecificationAttributeResponse
                {
                    Id = s.Id,
                    LabelSpecification = s.LabelSpecification,
                    SpecificationGroupId = s.SpecificationGroupId
                }).ToList());
        }
    }
}
