namespace Core.Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public float Estimate { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public string TargetUserId { get; set; }
        public User TargetUser { get; set; }
        public int? WareId { get; set; }
        public Ware Ware { get; set; }
    }
}
