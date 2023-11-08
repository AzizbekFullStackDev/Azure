using Microsoft.AspNetCore.Http;

namespace Azure.Service.DTOs.Hotel
{
    public class HotelForUpdateDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Location { get; set; }
        public string OpeningHours { get; set; }
        public int TotalRooms { get; set; }
    }
}
