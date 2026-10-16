using Ecommerce.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User: IdentityUser<int>
    {
        public string FullName { set; get; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public DateTime CreatedDay { set; get; } = DateTime.UtcNow;
        public DateTime DeteledDay { set; get; }
    }
}
