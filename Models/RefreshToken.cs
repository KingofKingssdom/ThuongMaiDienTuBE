using Ecommerce.Common.Entities;

namespace Ecommerce.Models
{
    public class RefreshToken: EntityBase
    {
        public string Token { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User User { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAt { get; set; }
    }
}
