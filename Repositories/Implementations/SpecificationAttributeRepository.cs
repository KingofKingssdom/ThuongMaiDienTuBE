using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class SpecificationAttributeRepository : ISpecificationAttributeRepository
    {
        private readonly AppDbContext _context;
        public SpecificationAttributeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SpecificationAttribute> AddAsync(
           SpecificationAttribute specificationAttribute,
           CancellationToken cancellationToken)
        {
            await _context.SpecificationAttribute.AddAsync(specificationAttribute, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return specificationAttribute;
        }
       public async Task<SpecificationAttribute?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationAttribute
                .Include(s=> s.SpecificationValue)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
       public async Task<List<SpecificationAttribute>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationAttribute
                .ToListAsync();
        }
    }
}
