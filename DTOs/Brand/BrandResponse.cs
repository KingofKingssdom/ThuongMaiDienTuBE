using Ecommerce.DTOs.Category;

namespace Ecommerce.DTOs.Brand
{
    public class BrandResponse
    {
        public int Id { set; get; }
        public string BrandName { set; get; } = string.Empty;
        public string UrlImageBrand { set; get; } = string.Empty;
        public List<CategoryResponse> Category { set; get; } = new List<CategoryResponse>();
    }
}
