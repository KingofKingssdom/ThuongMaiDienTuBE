namespace Ecommerce.DTOs.SpecificationValue
{
    public class CreateSpecificationValueRequest
    {
        public string Value { set; get; } = string.Empty;
        public int ProductId { set; get; }
        public int SpecificationAttributeId { set; get; }
    }
}
