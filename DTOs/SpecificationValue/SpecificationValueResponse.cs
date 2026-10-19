namespace Ecommerce.DTOs.SpecificationValue
{
    public class SpecificationValueResponse
    {
        public int Id { set; get; }
        public string Value { set; get; } = string.Empty;
        public int ProductId { set; get; }
        public int SpecificationAttributeId { set; get; }
    }
}
