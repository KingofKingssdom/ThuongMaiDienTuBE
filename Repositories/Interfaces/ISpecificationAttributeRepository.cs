using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ISpecificationAttributeRepository
    {
        Task<SpecificationAttribute> AddAsync(
            SpecificationAttribute specificationAttribute,
            CancellationToken cancellationToken = default);
        Task<SpecificationAttribute?> GetByIdAsync(int id,
            CancellationToken cancellationToken = default);
        Task<List<SpecificationAttribute>> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}
