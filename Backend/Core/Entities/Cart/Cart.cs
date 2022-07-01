using System.Collections.Generic;

namespace Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public ICollection<Ware> Wares { get; set; }
    }
}
