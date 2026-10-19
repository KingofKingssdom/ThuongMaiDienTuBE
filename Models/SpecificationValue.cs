using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class SpecificationValue: EntityBase
    {
        public string Value { set; get; } = string.Empty;
        public int ProductId { set; get; }
        public Product Product { set; get; } = null!;
        public int SpecificationAttributeId { set; get; }
        public SpecificationAttribute SpecificationAttribute { set; get; } = null!;
    }
}
