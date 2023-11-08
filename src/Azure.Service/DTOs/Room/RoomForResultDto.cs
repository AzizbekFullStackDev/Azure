using Azure.Domain.Enums;

namespace Azure.Service.DTOs.Room
{
    public class RoomForResultDto
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public string Image { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort comfort { get; set; }
        public decimal Cost { get; set; }
    }
}
