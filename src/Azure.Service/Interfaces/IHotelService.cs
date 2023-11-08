using Azure.Service.Configurations;
using Azure.Service.DTOs.Hotel;

namespace Azure.Service.Interfaces
{
    public interface IHotelService
    {
        public Task<HotelForResultDto> CreateAsync(HotelForCreationDto dto);
        public Task<HotelForResultDto> ModifyAsync(long Id, HotelForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<HotelForResultDto> GetByIdAsync(long Id);
        public Task<IEnumerable<HotelForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
