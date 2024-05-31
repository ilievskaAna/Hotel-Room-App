using HotelRoomApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomApp.Data.Interfaces
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms();
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room, Room newRoom);
        bool DeleteRoom(Room room);
    }
}
