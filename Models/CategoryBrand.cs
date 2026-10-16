namespace Ecommerce.Models
{
    public class CategoryBrand
    {
        public int CategoryId { set; get; }
        public Category Category { set; get; } = null!;
        public int BrandId { set; get; }
        public Brand Brand { set; get; } = null!;
    }
}
