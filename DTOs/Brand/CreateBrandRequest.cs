namespace Ecommerce.DTOs.Brand
{
    public class CreateBrandRequest
    {
        public string BrandName { set; get; } = string.Empty;
        public IFormFile? UrlImageBrand { set; get; }
        public List<int> CategoryIds { set; get; } = new List<int>();

    }
}
