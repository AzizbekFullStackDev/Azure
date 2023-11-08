using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.User
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public long CreditCardNumber { get; set; }
    }
}
