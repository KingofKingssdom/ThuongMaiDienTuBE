namespace Ecommerce.DTOs.SpecificationAttribute
{
    public class CreateSpecificationAttributeRequest
    {
        public string LabelSpecification { set; get; } = string.Empty;
        public int SpecificationGroupId { set; get; }
    }
}
