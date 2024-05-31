using System;
using System.Collections.Generic;
using System.Linq;
using HotelRoomApp.Data;
using HotelRoomApp.Data.Entities;
using HotelRoomApp.Data.Interfaces;

namespace HotelRoomApp.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelRoomDbContext _dataContext;

        public GuestRepository(HotelRoomDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void AddGuest(Guest guest)
        {
            _dataContext.Guests.Add(guest);
            _dataContext.SaveChanges();
        }

        public bool DeleteGuest(int id)
        {
            try
            {
                var guest = _dataContext.Guests.FirstOrDefault(x => x.Id == id);
                if (guest == null)
                {
                    return false;
                }

                _dataContext.Guests.Remove(guest);
                _dataContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Guest GetGuestById(int id)
        {
            return _dataContext.Guests.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _dataContext.Guests.ToList();
        }

        public ICollection<Guest> GetGuestsByRoomId(int roomId)
        {
            return _dataContext.Guests.Where(x => x.RoomId == roomId).ToList();
        }

        public void UpdateGuest(Guest guest)
        {
            _dataContext.Guests.Update(guest);
            _dataContext.SaveChanges();
        }

        public void UpdateGuest(Guest guest, Guest updatedGuest)
        {
            var existingGuest = _dataContext.Guests.FirstOrDefault(g => g.Id == guest.Id);

            if (existingGuest != null)
            {
                existingGuest.FirstName = updatedGuest.FirstName;
                existingGuest.LastName = updatedGuest.LastName;
                existingGuest.DOB = updatedGuest.DOB;
                existingGuest.Address = updatedGuest.Address;
                existingGuest.Nationality = updatedGuest.Nationality;
                existingGuest.CheckInDate = updatedGuest.CheckInDate;
                existingGuest.CheckOutDate = updatedGuest.CheckOutDate;
                existingGuest.RoomId = updatedGuest.RoomId;

                _dataContext.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException($"Guest with id {guest.Id} not found.");
            }
        }

        public void UpdateGuest(int id, Guest newGuest)
        {
            throw new NotImplementedException();
        }
    }
}
