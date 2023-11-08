using Azure.Service.DTOs.Login;
using Azure.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Azure.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> PostAsync(LoginDto dto)
        {
            var token = await this._authService.AuthenticateAsync(dto);
            return Ok(token);
        }
    }
}
