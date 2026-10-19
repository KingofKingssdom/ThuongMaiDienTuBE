using Ecommerce.Models;

namespace Ecommerce.Repositories.Interfaces
{
    public interface ISpecificationGroupRepository
    {
        Task<SpecificationGroup> AddAsync(
            SpecificationGroup specificationGroup,
            CancellationToken cancellationToken = default);
        Task<SpecificationGroup?> GetById(int id,
            CancellationToken cancellationToken = default);
        Task<List<SpecificationGroup>> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}
