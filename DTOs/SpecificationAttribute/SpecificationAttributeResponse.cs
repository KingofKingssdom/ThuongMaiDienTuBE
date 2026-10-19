namespace Ecommerce.DTOs.SpecificationAttribute
{
    public class SpecificationAttributeResponse
    {
        public int Id { set; get; } 
        public string LabelSpecification { set; get; } = string.Empty;
        public int SpecificationGroupId { set; get; }
    }
}
