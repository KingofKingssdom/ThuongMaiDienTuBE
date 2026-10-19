using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationValue;

namespace Ecommerce.Services.Interfaces
{
    public interface ISpecificationValueService
    {
        Task<Result<SpecificationValueResponse>> AddAsync(
            CreateSpecificationValueRequest request,
            CancellationToken cancellationToken);
        Task<Result<SpecificationValueResponse>> GetByIdAsync(
            int id,
            CancellationToken cancellationToken);
        Task<Result<List<SpecificationValueResponse>>> GetAllAsync(
            CancellationToken cancellationToken);
    }
}
