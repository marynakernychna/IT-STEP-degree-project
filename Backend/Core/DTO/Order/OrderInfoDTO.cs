namespace Core.DTO.Order
{
    public class OrderInfoDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ClientFullName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public int WaresCount { get; set; }
        public bool IsAcceptedByClient { get; set; }
        public bool IsAcceptedByCourier { get; set; }
    }
}
