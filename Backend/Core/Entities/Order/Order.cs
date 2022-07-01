using System;

namespace Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public string CourierId { get; set; }
        public User Courier { get; set; }
    }
}
