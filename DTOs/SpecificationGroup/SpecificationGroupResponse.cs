namespace Ecommerce.DTOs.SpecificationGroup
{
    public class SpecificationGroupResponse
    {
        public int Id { set; get; }
        public string SpecificationName { set; get; } = string.Empty;
        public int CategoryId { set; get; }
        public string CategoryName { set; get; } = string.Empty;
    }
}
