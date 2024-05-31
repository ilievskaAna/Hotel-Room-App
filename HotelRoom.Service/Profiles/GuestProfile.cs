using HotelRoom.Service.DTOs;
using HotelRoomApp.Data.Entities;
using AutoMapper;

namespace HotelRoom.Service.Profiles
{
    public class GuestProfile : Profile
    {
        public GuestProfile()
        {
            CreateMap<Guest, GuestDTO>().ReverseMap();
        }
    }
}
