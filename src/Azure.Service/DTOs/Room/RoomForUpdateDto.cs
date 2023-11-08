using Azure.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.Room
{
    public class RoomForUpdateDto
    {
        public IFormFile Image { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort comfort { get; set; }

        public decimal Cost { get; set; }
    }
}
