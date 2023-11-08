using Azure.Api.Models;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Booking;
using Azure.Service.DTOs.Room;
using Azure.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Azure.Api.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _bookingService.GetAllAsync(@params)
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
                Data = await _bookingService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] BookingForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _bookingService.CreateAsync(dto)
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
                Data = await _bookingService.RemoveAsync(Id)
            };
            return Ok(response);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] BookingForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _bookingService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
        //Added
    }
}
