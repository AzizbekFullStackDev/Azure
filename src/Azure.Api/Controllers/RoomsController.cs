using Azure.Api.Models;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Room;
using Azure.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Azure.Api.Controllers
{
    public class RoomsController : BaseController
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _roomService.GetAllAsync(@params)
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
                Data = await _roomService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] RoomForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _roomService.CreateAsync(dto)
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
                Data = await _roomService.RemoveAsync(Id)
            };
            return Ok(response);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] RoomForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _roomService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
        //Added
    }
}
