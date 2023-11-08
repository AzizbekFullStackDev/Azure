using AutoMapper;
using Azure.Data.IRepositories;
using Azure.Domain.Entities;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Asset;
using Azure.Service.DTOs.Hotel;
using Azure.Service.DTOs.Room;
using Azure.Service.Exceptions;
using Azure.Service.Extensions;
using Azure.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Azure.Service.Services
{
    public class HotelService : IHotelService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Hotel> _repository;
        private readonly IFileUploadService _fileUploadService;

        public HotelService(IFileUploadService fileUploadService, IRepository<Hotel> repository, IMapper mapper)
        {
            _fileUploadService = fileUploadService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HotelForResultDto> CreateAsync(HotelForCreationDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Name == dto.Name && e.Location == dto.Location && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData == null)
            {
                var AssetDetails = new AssetForCreationDto()
                {
                    FolderPath = "Hotel",
                    FormFile = dto.Image,
                };
                var SavedFile = await this._fileUploadService.FileUploadAsync(AssetDetails);
                var data = this._mapper.Map<Hotel>(dto);
                data.Image = SavedFile.AssetPath;
                var result = await this._repository.InsertAsync(data);
                return this._mapper.Map<HotelForResultDto>(result);
            }
            throw new AzureException(409, "This user is Exist");
        }

        public async Task<IEnumerable<HotelForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var data = await this._repository.SelectAll()
                .Where(h => h.IsDeleted == false)
                .Include(h => h.Rooms)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();
            if (data == null)
            {
                throw new AzureException(404, "No data Found");
            }
            foreach (var info in data)
            {
                if (info.Image != null)
                {
                    info.Image = $"https://localhost:7020/{info.Image.Replace('\\', '/').TrimStart('/')}";
                };
            }
            return this._mapper.Map<IEnumerable<HotelForResultDto>>(data);
        }

        public async Task<HotelForResultDto> GetByIdAsync(long Id)
        {
            var data = await this._repository.SelectAll()
                .Where(h => h.IsDeleted == false)
                .Include(h => h.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data != null)
            {
                if (data.Image != null)
                {
                    data.Image = $"https://localhost:7020/{data.Image.Replace('\\', '/').TrimStart('/')}";
                }
                return this._mapper.Map<HotelForResultDto>(data);
            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<HotelForResultDto> ModifyAsync(long Id, HotelForUpdateDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData != null)
            {
                await _fileUploadService.DeleteFileAsync(CheckingData.Image);

                var NewImageUploadMapping = new AssetForCreationDto()
                {
                    FolderPath = "Hotel",
                    FormFile = dto.Image,
                };

                var NewImageUpload = await this._fileUploadService.FileUploadAsync(NewImageUploadMapping);
                if (dto != null)
                {
                    CheckingData.Name = (dto.Name) != null ? dto.Name : CheckingData.Name;
                    CheckingData.Image = NewImageUpload.AssetPath;
                    CheckingData.Location = !string.IsNullOrEmpty(dto.Location) ? dto.Location : CheckingData.Location;
                    CheckingData.OpeningHours = !string.IsNullOrEmpty(dto.OpeningHours) ? dto.OpeningHours : CheckingData.OpeningHours;
                    CheckingData.TotalRooms = (dto.TotalRooms) != null ? dto.TotalRooms : CheckingData.TotalRooms;
                }
                CheckingData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(CheckingData);

                return this._mapper.Map<HotelForResultDto>(result);

            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            var data = await this._repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (data == null)
            {
                throw new AzureException(404, "NotFound");
            }
            var imageRemove = this._fileUploadService.DeleteFileAsync(data.Image);
            var Result = await this._repository.DeleteAsync(Id);
            return Result;
        }
    }
}
