using Ecommerce.Common.Enum;
using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationGroup;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Services.Implementations
{
    public class SpecificationGroupService: ISpecificationGroupService
    {
        private readonly ISpecificationGroupRepository _specificationGroupRepository;
        public SpecificationGroupService(ISpecificationGroupRepository specificationGroupRepository)
        {
            _specificationGroupRepository = specificationGroupRepository;
        }
       public async Task<Result<SpecificationGroupResponse>> AddAsync(CreateSpecificationGroupRequest request,
            CancellationToken cancellationToken)
        {
            var specificationGroup = new SpecificationGroup
            {
                SpecificationName = request.SpecificationName,
                CategoryId = request.CategoryId
            };
            await _specificationGroupRepository.AddAsync(specificationGroup, cancellationToken);
            return Result<SpecificationGroupResponse>.Success(new SpecificationGroupResponse
            {
                Id = specificationGroup.Id,
                SpecificationName = specificationGroup.SpecificationName,
                CategoryId = specificationGroup.CategoryId
            });
        }
       public async Task<Result<SpecificationGroupResponse>> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var specificationGroupExisting = await _specificationGroupRepository.GetById(id, cancellationToken);
            if(specificationGroupExisting == null)
            {
                return Result<SpecificationGroupResponse>.Failure("Not Found", ErrorType.NotFound);
            }
            return Result<SpecificationGroupResponse>.Success(new SpecificationGroupResponse
            {
                Id = specificationGroupExisting.Id,
                SpecificationName = specificationGroupExisting.SpecificationName,
                CategoryId = specificationGroupExisting.CategoryId,
                CategoryName = specificationGroupExisting.Category.CategoryName
            });
        }
       public async Task<Result<List<SpecificationGroupResponse>>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var specificationGroupExisting = await _specificationGroupRepository
                .GetAllAsync(cancellationToken);
            if (specificationGroupExisting == null)
            {
                return Result<List<SpecificationGroupResponse>>.Failure("Not Found", ErrorType.NotFound);
            }
            return Result<List<SpecificationGroupResponse>>.Success(specificationGroupExisting
                .Select(s => new SpecificationGroupResponse
                {
                    Id = s.Id,
                    SpecificationName = s.SpecificationName,
                    CategoryId = s.CategoryId,
                    CategoryName = s.Category.CategoryName
                }).ToList());
        }
    }

}
