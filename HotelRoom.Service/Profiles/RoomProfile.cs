using AutoMapper;
using HotelRoom.Service.DTOs;
using HotelRoomApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Service.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
        }
    }
}
