using System;

namespace Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoLink { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
        public string CreatorId { get; set; }
        public User Creator { get; set; }
    }
}
