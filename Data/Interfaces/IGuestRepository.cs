using HotelRoomApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoomApp.Data.Interfaces
{
    public interface IGuestRepository
    {
        IEnumerable<Guest> GetGuests();
        Guest GetGuestById(int id);
        void AddGuest(Guest guest);
        void UpdateGuest(Guest guest, Guest updatedGuest);
        bool DeleteGuest(int id);
        
        ICollection<Guest> GetGuestsByRoomId(int roomId);
        void UpdateGuest(int id, Guest newGuest);
    }
}
