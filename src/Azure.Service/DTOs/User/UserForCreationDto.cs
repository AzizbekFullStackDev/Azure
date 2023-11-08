using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.User
{
    public class UserForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
        public long CreditCardNumber { get; set; }
    }
}
