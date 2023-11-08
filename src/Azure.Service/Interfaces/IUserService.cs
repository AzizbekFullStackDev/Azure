using Azure.Service.Configurations;
using Azure.Service.DTOs.User;

namespace Azure.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
        public Task<UserForResultDto> ModifyAsync(long Id, UserForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<UserForResultDto> GetByIdAsync(long Id);
        public Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
