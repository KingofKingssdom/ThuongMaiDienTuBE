using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class Cart: EntityBase
    {
        public int UserId { set; get; }
        public User User { set; get; } = null!;
        public ICollection<CartItem> CartItem { set; get; } = new List<CartItem>();

    }
}
