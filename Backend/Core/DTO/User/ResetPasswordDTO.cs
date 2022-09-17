namespace Core.DTO.User
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public string NewPassword { get; set; }
    }
}
