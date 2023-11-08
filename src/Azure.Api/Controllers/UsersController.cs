using Azure.Api.Models;
using Azure.Service.Configurations;
using Azure.Service.DTOs.User;
using Azure.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Azure.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.GetAllAsync(@params)
            };
            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] UserForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.CreateAsync(dto)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.RemoveAsync(Id)
            };
            return Ok(response);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] UserForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
        //Added
    }
}
