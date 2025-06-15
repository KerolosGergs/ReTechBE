using ReTechApi.Models;

namespace ReTechBE.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; } = UserType.Customer;
    }
}
