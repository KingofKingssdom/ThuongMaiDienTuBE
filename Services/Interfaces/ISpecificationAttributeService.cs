using Ecommerce.Common.Results;
using Ecommerce.DTOs.SpecificationAttribute;

namespace Ecommerce.Services.Interfaces
{
    public interface ISpecificationAttributeService
    {
        Task<Result<SpecificationAttributeResponse>> AddAsync(
            CreateSpecificationAttributeRequest request,
            CancellationToken cancellationToken);
        Task<Result<SpecificationAttributeResponse>> GetByIdAsync(
            int id, CancellationToken cancellationToken);
        Task<Result<List<SpecificationAttributeResponse>>> GetAllAsync(
            CancellationToken cancellationToken);

    }
}
