using Azure.Domain.Commons;
using Azure.Domain.Enums;

namespace Azure.Domain.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public Roles Role { get; set; }
        public long CreditCardNumber { get; set; }
    }
}
