namespace Core.Entities
{
    public class Characteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int WareId { get; set; }
        public Ware Ware { get; set; }
    }
}
