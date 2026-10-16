using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class Brand:EntityBase
    {
        public string BrandName { set; get; } = string.Empty;
        public string UrlImageBrand { set; get; } = string.Empty;
        public ICollection<CategoryBrand> CategoryBrand = new List<CategoryBrand>(); 
    }
}
