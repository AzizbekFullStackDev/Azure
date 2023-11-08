using AutoMapper;
using Azure.Data.IRepositories;
using Azure.Domain.Entities;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Asset;
using Azure.Service.DTOs.User;
using Azure.Service.Exceptions;
using Azure.Service.Extensions;
using Azure.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Azure.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public UserService(IMapper mapper, IRepository<User> repository, IFileUploadService fileUploadService)
        {
            _mapper = mapper;
            _repository = repository;
            _fileUploadService = fileUploadService;
        }

        public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Email == dto.Email && e.PhoneNumber == e.PhoneNumber && e.IsDeleted == false).FirstOrDefaultAsync();
            if(CheckingData == null) 
            {
                var AssetDetails = new AssetForCreationDto()
                {
                    FolderPath = "User",
                    FormFile = dto.Image,
                };
                var SavedFile = await this._fileUploadService.FileUploadAsync(AssetDetails);
                var data = this._mapper.Map<User>(dto);
                data.Image = SavedFile.AssetPath;
                data.Role = Domain.Enums.Roles.User;
                var result = await this._repository.InsertAsync(data);
                return this._mapper.Map<UserForResultDto>(result);
            }
            throw new AzureException(409, "This user is Exist");
        }

        public async Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var data = await this._repository.SelectAll()
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();
            if (data == null)
            {
                throw new AzureException(404, "No data Found");
            }
            foreach (var info in data)
            {
                if (info.Image!= null)
                {
                    info.Image = $"https://localhost:7020/{info.Image.Replace('\\', '/').TrimStart('/')}";
                };
            }
            return this._mapper.Map<IEnumerable<UserForResultDto>>(data);
        }

        public async Task<UserForResultDto> GetByIdAsync(long Id)
        {
            var data = await this._repository.SelectByIdAsync(Id);
            if (data != null)
            {
                if(data.Image!= null)
                {
                    data.Image = $"https://localhost:7020/{data.Image.Replace('\\', '/').TrimStart('/')}";
                }
                return this._mapper.Map<UserForResultDto>(data);
            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<UserForResultDto> ModifyAsync(long Id, UserForUpdateDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData != null)
            {
                await _fileUploadService.DeleteFileAsync(CheckingData.Image);

                var NewImageUploadMapping = new AssetForCreationDto()
                {
                    FolderPath = "User",
                    FormFile = dto.Image,
                };

                var NewImageUpload = await this._fileUploadService.FileUploadAsync(NewImageUploadMapping);
                if(dto != null)
                {
                    CheckingData.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : CheckingData.FirstName;
                    CheckingData.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : CheckingData.LastName;
                    CheckingData.CreditCardNumber = (dto.CreditCardNumber) != null ? dto.CreditCardNumber : CheckingData.CreditCardNumber;
                    CheckingData.Image = NewImageUpload.AssetPath;
                }
                CheckingData.UpdatedAt = DateTime.UtcNow;

                var result = await this._repository.UpdateAsync(CheckingData);
                
                return this._mapper.Map<UserForResultDto>(result);

            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            var data = await this._repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if(data == null)
            {
                throw new AzureException(404, "NotFound");
            }
            var imageRemove = this._fileUploadService.DeleteFileAsync(data.Image);
            var Result = await this._repository.DeleteAsync(Id);
            return Result;
        }
    }
}
