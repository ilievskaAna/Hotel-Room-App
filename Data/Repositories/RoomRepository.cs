using System;
using System.Collections.Generic;
using System.Linq;
using HotelRoomApp.Data;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;

namespace HotelRoomApp.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelRoomDbContext _dataContext;

        public RoomRepository(HotelRoomDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddRoom(Room room)
        {
            _dataContext.Rooms.Add(room);
            _dataContext.SaveChanges();
        }

        public bool DeleteRoom(Room room)
        {
            try
            {
                _dataContext.Rooms.Remove(room);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Room GetRoomById(int id)
        {
            return _dataContext.Rooms.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _dataContext.Rooms.ToList();
        }

        public void UpdateRoom(Room room)
        {
            _dataContext.Rooms.Update(room);
            _dataContext.SaveChanges();
        }

        public void UpdateRoom(Room room, Room newRoom)
        {
            var existingRoom = _dataContext.Rooms.FirstOrDefault(r => r.Id == room.Id);

            if (existingRoom != null)
            {
                existingRoom.Number = newRoom.Number;
                existingRoom.Floor = newRoom.Floor;
                existingRoom.Type = newRoom.Type;

                _dataContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Room with id {room.Id} not found.");
            }
        }
    }
}
