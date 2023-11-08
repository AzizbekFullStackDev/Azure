using Azure.Domain.Commons;

namespace Azure.Domain.Entities
{
    public class Hotel : Auditable
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Location { get; set; }
        public string OpeningHours { get; set; }
        public int TotalRooms { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

    }
}
