namespace Ecommerce.DTOs.Category
{
    public class CategoryResponse
    {
        public int Id { set; get; }
        public string CategoryName { set; get; } = string.Empty;
        public DateTime CreatedDay { set; get; } 
    }
}
