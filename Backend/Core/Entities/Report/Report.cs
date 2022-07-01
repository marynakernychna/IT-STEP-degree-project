using System;

namespace Core.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public int? WareId { get; set; }
        public Ware Ware { get; set; }
        public string TargetUserId { get; set; }
        public User TargetUser { get; set; }
    }
}
