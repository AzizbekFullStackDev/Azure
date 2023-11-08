using Azure.Service.Configurations;
using Azure.Service.DTOs.Room;

namespace Azure.Service.Interfaces
{
    public interface IRoomService
    {
        public Task<RoomForResultDto> CreateAsync(RoomForCreationDto dto);
        public Task<RoomForResultDto> ModifyAsync(long Id, RoomForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<RoomForResultDto> GetByIdAsync(long Id);
        public Task<IEnumerable<RoomForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
