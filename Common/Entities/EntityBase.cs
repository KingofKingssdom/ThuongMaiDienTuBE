namespace Ecommerce.Common.Entities
{
    public class EntityBase
    {
        public int Id { set; get; }
        public DateTime CreatedDay { set; get; } = DateTime.UtcNow;
        public DateTime DeteledDay { set; get; }

    }
}
