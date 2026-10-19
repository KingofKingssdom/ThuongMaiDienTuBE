using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class SpecificationGroupRepository: ISpecificationGroupRepository
    {
        private readonly AppDbContext _context;
        public SpecificationGroupRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<SpecificationGroup> AddAsync(
            SpecificationGroup specificationGroup,
            CancellationToken cancellationToken)
        {
            await _context.SpecificationGroup.AddAsync(specificationGroup);
            await _context.SaveChangesAsync();
            return specificationGroup;
        }
       public async Task<SpecificationGroup?> GetById(int id,
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationGroup.FirstOrDefaultAsync(s => s.Id == id);
        }
       public async Task<List<SpecificationGroup>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationGroup
                .Include(s => s.Category)
                .ToListAsync();
        }
    }
}
