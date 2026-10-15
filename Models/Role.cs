using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class Role: IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
