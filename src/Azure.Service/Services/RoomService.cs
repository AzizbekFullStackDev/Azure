using AutoMapper;
using Azure.Data.IRepositories;
using Azure.Domain.Entities;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Asset;
using Azure.Service.DTOs.Hotel;
using Azure.Service.DTOs.Room;
using Azure.Service.DTOs.User;
using Azure.Service.Exceptions;
using Azure.Service.Extensions;
using Azure.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Service.Services 
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Room> _repository;
        private readonly IFileUploadService _fileUploadService;

        public RoomService(IRepository<Room> repository, IMapper mapper, IFileUploadService fileUploadService)
        {
            _mapper = mapper;
            _repository = repository;
            _fileUploadService = fileUploadService;
        }


        public async Task<RoomForResultDto> CreateAsync(RoomForCreationDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.HotelId == dto.HotelId && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData == null)
            {
                var AssetDetails = new AssetForCreationDto()
                {
                    FolderPath = "Room",
                    FormFile = dto.Image,
                };
                var SavedFile = await this._fileUploadService.FileUploadAsync(AssetDetails);
                var data = this._mapper.Map<Room>(dto);
                data.Image = SavedFile.AssetPath;
                var result = await this._repository.InsertAsync(data);
                return this._mapper.Map<RoomForResultDto>(result);
            }
            throw new AzureException(409, "This Room is Exist");
        }

        public async Task<IEnumerable<RoomForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var data = await this._repository.SelectAll()
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();
            if(data == null)
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
            return this._mapper.Map<IEnumerable<RoomForResultDto>>(data);
        }

        public async Task<RoomForResultDto> GetByIdAsync(long Id)
        {
            var data = await this._repository.SelectByIdAsync(Id);
            if (data != null)
            {
                if (data.Image != null)
                {
                    data.Image = $"https://localhost:7020/{data.Image.Replace('\\', '/').TrimStart('/')}";
                }
                return this._mapper.Map<RoomForResultDto>(data);
            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<RoomForResultDto> ModifyAsync(long Id, RoomForUpdateDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData != null)
            {
                await _fileUploadService.DeleteFileAsync(CheckingData.Image);

                var NewImageUploadMapping = new AssetForCreationDto()
                {
                    FolderPath = "Room",
                    FormFile = dto.Image,
                };

                var NewImageUpload = await this._fileUploadService.FileUploadAsync(NewImageUploadMapping);
                if (dto != null)
                {
                    CheckingData.NumberOfRooms = (dto.NumberOfRooms) !=null ? dto.NumberOfRooms : CheckingData.NumberOfRooms;
                    CheckingData.Cost = (dto.Cost) != null ? dto.Cost : CheckingData.Cost;
                    CheckingData.Image = NewImageUpload.AssetPath;
                    CheckingData.comfort = (dto.comfort) != null ? dto.comfort : CheckingData.comfort;
                }
                CheckingData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(CheckingData);

                return this._mapper.Map<RoomForResultDto>(result);

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
