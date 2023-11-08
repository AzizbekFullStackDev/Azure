using AutoMapper;
using Azure.Data.IRepositories;
using Azure.Domain.Entities;
using Azure.Service.Configurations;
using Azure.Service.DTOs.Asset;
using Azure.Service.DTOs.Booking;
using Azure.Service.DTOs.Message;
using Azure.Service.DTOs.Room;
using Azure.Service.Exceptions;
using Azure.Service.Extensions;
using Azure.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Azure.Service.Services
{
    public class BookingService : IBookingService
    {
        private IMapper _mapper;
        private IEmailService _emailService;
        private IRepository<Booking> _repository;
        private IRepository<Hotel> _hotelRepository;
        private IRepository<Room> _roomRepository;
        private IRepository<User> _userRepository;

        public BookingService(IRepository<Booking> repository, IMapper mapper, IRepository<Hotel> hotelRepository, IRepository<Room> roomRepository, IRepository<User> userRepository, IEmailService emailService)
        {
            _repository = repository;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<BookingForResultDto> CreateAsync(BookingForCreationDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.HotelId == dto.HotelId && e.IsDeleted == false).FirstOrDefaultAsync();
            var hotelData = await this._hotelRepository.SelectAll()
                .Include(e => e.Rooms)
                .Where(e => e.Id == dto.HotelId && e.IsDeleted == false).FirstOrDefaultAsync();
            var RoomAvailable = await this._roomRepository.SelectAll().Where(e => e.HotelId == dto.HotelId && e.NumberOfRooms == dto.NumberOfRooms && e.comfort == dto.Comfort).FirstOrDefaultAsync();
            var UserData = await this._userRepository.SelectAll().Where(e => e.Id == dto.UserId && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData != null && hotelData != null && UserData != null)
            {
                var data = this._mapper.Map<Booking>(dto);
                var result = await this._repository.InsertAsync(data);
                var Message = new MessageForCreationDto()
                {
                    Body = $"Dear {UserData.FirstName},\r\n\r\nWe are delighted to confirm your reservation at {hotelData.Name} for {result.LengthOfStay} days. Your comfort and satisfaction are our top priorities, and we are eagerly anticipating your arrival.\r\n\r\nReservation Details:\r\n\r\nHotel Name: {hotelData.Name}\r\nLength of stay: {result.LengthOfStay}\r\nRoom Type: {RoomAvailable.comfort}\r\nTotal Price: {RoomAvailable.Cost}$",
                    Subject = "\r\nSubject: Confirmation of Your Hotel Booking",
                    To = $"{UserData.Email}",
                };

                await _emailService.SendEmail(Message);
                return this._mapper.Map<BookingForResultDto>(result);
            }
            throw new AzureException(409, "This Choice is not Found, Please enter other values");
        }

        public async Task<IEnumerable<BookingForResultDto>> GetAllAsync(PaginationParams @params)
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
            return this._mapper.Map<IEnumerable<BookingForResultDto>>(data);
        }

        public async Task<BookingForResultDto> GetByIdAsync(long Id)
        {
            var data = await this._repository.SelectByIdAsync(Id);
            if (data != null)
            {
                return this._mapper.Map<BookingForResultDto>(data);
            }
            throw new AzureException(404, "NotFound");
        }

        public async Task<BookingForResultDto> ModifyAsync(long Id, BookingForUpdateDto dto)
        {
            var CheckingData = await _repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (CheckingData != null)
            {
                if (dto != null)
                {
                    CheckingData.LengthOfStay = (dto.LengthOfStay) != null ? dto.LengthOfStay : CheckingData.LengthOfStay;
                    CheckingData.NumberOfRooms = (dto.NumberOfRooms) != null ? dto.NumberOfRooms : CheckingData.NumberOfRooms;
                    CheckingData.Comfort = (dto.Comfort) != null ? dto.Comfort : CheckingData.Comfort;
                }
                CheckingData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(CheckingData);

                return this._mapper.Map<BookingForResultDto>(result);

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
            var Result = await this._repository.DeleteAsync(Id);
            return Result;
        }
    }
}
