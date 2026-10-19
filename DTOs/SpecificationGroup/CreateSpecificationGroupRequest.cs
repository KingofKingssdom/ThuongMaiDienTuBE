namespace Ecommerce.DTOs.SpecificationGroup
{
    public class CreateSpecificationGroupRequest
    {
        public string SpecificationName { set; get; } = string.Empty;
        public int CategoryId { set; get; }
    }
}
