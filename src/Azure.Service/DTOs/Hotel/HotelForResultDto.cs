using Azure.Service.DTOs.Room;

namespace Azure.Service.DTOs.Hotel
{
    public class HotelForResultDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string OpeningHours { get; set; }
        public int TotalRooms { get; set; }
        public IEnumerable<RoomForResultDto> Rooms { get; set; }
    }
}
