using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class Category: EntityBase
    {
        public string CategoryName { set; get; } = string.Empty;
        public ICollection<CategoryBrand> CategoryBrand { set; get; } = new List<CategoryBrand>();
    }
}
