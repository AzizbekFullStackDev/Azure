using Azure.Api.Models;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Hotel;
using Azure.Service.DTOs.Room;
using Azure.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Azure.Api.Controllers
{
    public class HotelsController : BaseController
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _hotelService.GetAllAsync(@params)
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
                Data = await _hotelService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] HotelForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _hotelService.CreateAsync(dto)
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
                Data = await _hotelService.RemoveAsync(Id)
            };
            return Ok(response);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] HotelForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _hotelService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
        //Added
    }
}
