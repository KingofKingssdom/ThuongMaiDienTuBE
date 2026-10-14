namespace Ecommerce.Common.Entities
{
    public class EntityBase
    {
        public int Id { set; get; }
        public DateTime CreateDay { set; get; } = DateTime.UtcNow;
        public DateTime DeteleDay { set; get; }

    }
}
