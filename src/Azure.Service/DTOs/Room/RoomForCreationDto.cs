using Azure.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.Room
{
    public class RoomForCreationDto
    {
        public long HotelId { get; set; }
        public IFormFile Image { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort comfort { get; set; }

        public decimal Cost { get; set; }
    }
}
