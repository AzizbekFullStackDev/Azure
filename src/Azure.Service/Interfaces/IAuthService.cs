using Azure.Service.DTOs.Login;

namespace Azure.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginForResultDto> AuthenticateAsync(LoginDto dto);

    }
}
