using Azure.Service.DTOs.Message;

namespace Azure.Service.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmail(MessageForCreationDto message);
    }
}
