using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class SpecificationAttribute: EntityBase
    {
        public string LabelSpecification { set; get; } = string.Empty;
        public int SpecificationGroupId { set; get; }
        public SpecificationGroup SpecificationGroup { set; get; } = null!;
        public ICollection<SpecificationValue> SpecificationValue { set; get; }
            = new List<SpecificationValue>();
    }
}
