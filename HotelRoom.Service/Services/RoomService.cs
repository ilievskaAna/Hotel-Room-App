using AutoMapper;
using HotelRoom.Service.DTOs;
using HotelRoom.Service.Interfaces;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Service.Services
{
    public class RoomService : IRoomService
    {
        IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService (IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public RoomDTO GetRoomById(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            return _mapper.Map<RoomDTO>(room);
        }

        public List<RoomDTO> GetRooms()
        {
            var rooms = _roomRepository.GetRooms();
            return _mapper.Map<List<RoomDTO>>(rooms);
        }
        
        public RoomDTO AddRoom(RoomDTO room)
        {
            var newRoom = _mapper.Map<Room>(room);
            room.Id = 0; // Ensure the ID is set to 0 for a new room

            _roomRepository.AddRoom(newRoom);

            return _mapper.Map<RoomDTO>(room);
        }

        public bool DeleteRoom(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room != null)
            {
                return _roomRepository.DeleteRoom(room);
            }

            return false;
        }

        public RoomDTO UpdateRoom(int id, RoomDTO room)
        {
            var existingRoom = _roomRepository.GetRoomById(id);
            if (existingRoom != null)
            {
                var updatedRoom = _mapper.Map<Room>(room);
                room.Id = id; // Ensure the ID is set correctly

                _roomRepository.UpdateRoom(existingRoom, updatedRoom);

                return _mapper.Map<RoomDTO>(room);
            }

            throw new Exception("Room not found");
        }
    }
}
