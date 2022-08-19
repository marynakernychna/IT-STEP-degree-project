using System;

namespace Core.Entities
{
    public class WareCart
    {
        public int Id { get; set; }
        public int WareId { get; set; }
        public Ware Ware { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public DateTimeOffset DateOfAdding { get; set; } = DateTimeOffset.UtcNow;
    }
}
