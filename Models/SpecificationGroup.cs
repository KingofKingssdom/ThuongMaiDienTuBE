using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class SpecificationGroup:EntityBase
    {
        public string SpecificationName { set; get; } = string.Empty;
        public int CategoryId { set; get; }
        public Category Category { set; get; } = null!;
        public ICollection<SpecificationAttribute> SpecificationAttribute { set; get; } 
            = new List<SpecificationAttribute>();
    }
}
