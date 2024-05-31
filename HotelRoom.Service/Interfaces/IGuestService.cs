using HotelRoom.Service.DTOs;
using HotelRoomApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Service.Interfaces
{
    public interface IGuestService
    {
        List<GuestDTO> GetAllGuests();
        GuestDTO GetGuestById(int id);
        GuestDTO AddGuest(GuestDTO guestDto);
        GuestDTO UpdateGuest(GuestDTO guestDto);
        bool DeleteGuest(int id);
    }
}
