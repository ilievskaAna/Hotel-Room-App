using HotelRoom.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Service.Interfaces
{
    public interface IRoomService
    {
        List<RoomDTO> GetRooms();
        RoomDTO GetRoomById(int id);
        RoomDTO AddRoom(RoomDTO room);
        RoomDTO UpdateRoom(int id, RoomDTO room);
        bool DeleteRoom(int id);
    }
}
