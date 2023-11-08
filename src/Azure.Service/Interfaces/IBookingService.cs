using Azure.Service.Configurations;
using Azure.Service.DTOs.Booking;

namespace Azure.Service.Interfaces
{
    public interface IBookingService
    {
        public Task<BookingForResultDto> CreateAsync(BookingForCreationDto dto);
        public Task<BookingForResultDto> ModifyAsync(long Id, BookingForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<BookingForResultDto> GetByIdAsync(long Id);
        public Task<IEnumerable<BookingForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
