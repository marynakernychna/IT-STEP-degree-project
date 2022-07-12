namespace Core.DTO.User
{
    public class UserProfileInfoDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
