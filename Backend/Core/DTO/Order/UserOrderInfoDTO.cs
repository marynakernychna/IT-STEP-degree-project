namespace Core.DTO.Order
{
    public class UserOrderInfoDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public int WaresCount { get; set; }
        public bool IsPicked { get; set; }
    }
}
