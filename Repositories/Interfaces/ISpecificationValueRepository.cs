using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ISpecificationValueRepository
    {
        Task<SpecificationValue> AddAsync(
            SpecificationValue specificationValue,
            CancellationToken cancellationToken = default);
        Task<SpecificationValue?> GetByIdAsync(int id,
            CancellationToken cancellationToken = default);
        Task<List<SpecificationValue>> GetAllAsync(
            CancellationToken cancellationToken = default);

    }
}
