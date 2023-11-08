using Azure.Domain.Enums;

namespace Azure.Service.DTOs.Booking
{
    public class BookingForUpdateDto
    {
        public int LengthOfStay { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort Comfort { get; set; }
    }
}
