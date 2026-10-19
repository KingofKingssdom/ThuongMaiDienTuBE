using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationGroup;

namespace Ecommerce.Services.Interfaces
{
    public interface ISpecificationGroupService
    {
        Task<Result<SpecificationGroupResponse>> AddAsync(CreateSpecificationGroupRequest request,
            CancellationToken cancellationToken);
        Task<Result<SpecificationGroupResponse>> GetByIdAsync(int id,
            CancellationToken cancellationToken);
        Task<Result<List<SpecificationGroupResponse>>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
