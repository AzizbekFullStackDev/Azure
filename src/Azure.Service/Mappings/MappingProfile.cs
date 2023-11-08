using AutoMapper;
using Azure.Domain.Entities;
using Azure.Service.DTOs.Booking;
using Azure.Service.DTOs.Hotel;
using Azure.Service.DTOs.Room;
using Azure.Service.DTOs.User;

namespace Azure.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserForResultDto>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            CreateMap<User, UserForCreationDto>().ReverseMap();
        
            CreateMap<Hotel, HotelForUpdateDto>().ReverseMap();   
            CreateMap<Hotel, HotelForResultDto>().ReverseMap();
            CreateMap<Hotel, HotelForCreationDto>().ReverseMap();   
        
            CreateMap<Booking, BookingForResultDto>().ReverseMap();
            CreateMap<Booking, BookingForResultDto>().ReverseMap();
            CreateMap<Booking, BookingForCreationDto>().ReverseMap();

            CreateMap<Room, RoomForResultDto>().ReverseMap();
            CreateMap<Room, RoomForUpdateDto>().ReverseMap();
            CreateMap<Room, RoomForCreationDto>().ReverseMap();

        }

    }
}
