using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories.Implementations
{
    public class SpecificationValueRepository: ISpecificationValueRepository
    {
        private readonly AppDbContext _context;
        public SpecificationValueRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SpecificationValue> AddAsync(
             SpecificationValue specificationValue,
             CancellationToken cancellationToken)
        {
            await _context.SpecificationValue.AddAsync(specificationValue, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return specificationValue;
        }
        public async Task<SpecificationValue?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationValue
                 .FirstOrDefaultAsync(s => s.Id == id);  
        }
       public async Task<List<SpecificationValue>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _context.SpecificationValue
                .ToListAsync(cancellationToken);
        }
    }
}
