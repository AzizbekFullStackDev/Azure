using Azure.Domain.Commons;
using Azure.Domain.Enums;

namespace Azure.Domain.Entities
{
    public class Booking : Auditable
    {
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }  
        public int LengthOfStay { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort Comfort { get; set; }

    }
}
