using Azure.Domain.Enums;

namespace Azure.Service.DTOs.Booking
{
    public class BookingForResultDto
    {
        public long Id { get; set; }
        public long HotelId { get; set; }
        public long UserId { get; set; }
        public int LengthOfStay { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort Comfort { get; set; }
    }
}
