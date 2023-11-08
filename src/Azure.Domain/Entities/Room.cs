using Azure.Domain.Commons;
using Azure.Domain.Enums;

namespace Azure.Domain.Entities
{
    public class Room : Auditable
    {
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Image { get; set; }
        public int NumberOfRooms { get; set; }
        public Comfort comfort { get; set; }
        public decimal Cost { get; set; }
    }
}
